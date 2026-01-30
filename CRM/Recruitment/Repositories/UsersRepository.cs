using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Pages.Backend;
using System.Collections.Generic;

#pragma warning disable EF1001 // Internal EF Core API usage.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
namespace Recruitment.Repositories
{
    public interface IUsersRepository : IGenericRepository<RecruitmentUser>
    {
        Task<List<ResponseDTO.UserResponse>> GetUserResponse();
        Task<IActionResult> SendMailResetPassword(string? Email);
        Task<IActionResult> ResetPassword(string? Id, string? token, string? confrim_pass);
        Task<RecruitmentUser> FindEmail(string? Email);
        Task<RecruitmentUser> GetUser(string? userid);
        Task<Role> FindRolueUser(string? userid);
        Task<List<ResponseDTO.GetRoleUser>> GetRoleUser();
        Task<List<ResponseDTO.RecruitResponse>> GetRecruit();
    }

    public class UsersRepository : GenericRepository<RecruitmentUser>, IUsersRepository
    {
        private readonly SettingMail settingMail = new SettingMail();
        private readonly UserManager<RecruitmentUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersRepository(RecruitmentContext context, UserManager<RecruitmentUser> userManager, IWebHostEnvironment webHostEnvironment) : base(context)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public async Task<Role> FindRolueUser(string? userid)
        {
            try
            {
                if (userid != null)
                { 
                    var aa = await _context.UserRoles.ToListAsync();
                    var DB = aa.FirstOrDefault(x => x.UserId.Contains(userid));
                    if (DB != null)
                    {
                        var Role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == DB.RoleId);
                        return Role;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<List<ResponseDTO.GetRoleUser>> GetRoleUser()
        {
            try
            {
                var DB = await _context.UserRoles.Join(_context.Roles,
                    ur => ur.RoleId,
                    u => u.Id,
                    (ur, r) => new ResponseDTO.GetRoleUser
                    {
                        userid = ur.UserId,
                        role = r.Name
                    }).ToListAsync();

                return DB;
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<RecruitmentUser> GetUser(string? userid)
        {
            try
            {
                var DB = await _context.Users.FirstOrDefaultAsync(x => x.Id.Contains(userid));
                return DB;
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<RecruitmentUser> FindEmail(string? Email)
        {
            try
            {
                var DB = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Contains(Email));
                return DB;
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<List<ResponseDTO.UserResponse>> GetUserResponse()
        {
            try
            {
                var DB = await (from u in _context.Users
                                join ur in _context.UserRoles on u.Id equals ur.UserId into urs
                                from o in urs.DefaultIfEmpty()
                                join r in _context.Roles on o.RoleId equals r.Id into rs
                                from o2 in rs.DefaultIfEmpty()
                                where u.DeleteAt != 1
                                select new ResponseDTO.UserResponse
                                {
                                    Id = u.Id,
                                    Firstname = u.Firstname,
                                    Lastname = u.Lastname,
                                    Permission = o2.Name,
                                    DeleteAt = u.DeleteAt,
                                    Target = u.TargetKpi,
                                    Targetdate = u.Targetdate,
                                    Todate = u.Todate,
                                    Project = u.Project
                                }).ToListAsync();

                return DB;
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<IActionResult> SendMailResetPassword(string? Email)
        {
            var CheckMail = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Contains(Email));
            if (CheckMail != null)
            {
                var configuration = GetConfiguration();
                var Subject = "ยื่นขอเปลื่ยนรหัสผ่าน";

                var Find = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Contains(Email));
                var code = await _userManager.GeneratePasswordResetTokenAsync(Find);

                var SenderTo = (await _context.EmailSetting.FirstOrDefaultAsync())!;  //mail;
                if (SenderTo == null)
                {

                    SenderTo.Email = configuration.GetSection("MailSettings:Mail").Value;
                }

                var callbackUrl = configuration.GetSection("LinkWeb:Frontend").Value + "Backend/Resetpassword/?Id=" + CheckMail.Id;

                var param = new Dictionary<string, string>
                {
                    { "token",code },
                    { "email",SenderTo.Email! }

                };

                var callback = QueryHelpers.AddQueryString(callbackUrl!, param!);


                var html = "";
                html += "<p>สวัสดีค่ะ/ครับ.</p><br>";
                html += "<p>เราได้ส่งข้อมูลตามคำขอเพื่อช่วยให้คุณสามารถเข้าถึงบัญชีผู็ใช้ของ ระบบ CRM ได้ </p><br>";
                html += "<p>โปรดรีบดำเนินการ! ลิงก์นี้จะหมดอายุภายใน 24 ชั่วโมง</p><br>";
                html += "<p>ชื่อผู็ใช้ : </p><br>";

                html += "<a href=\"" + callback + "\">คลิกที่นี้เพื่อ ตั้งรหัสผ่านใหม่ </a><br>";

                html += "<p>ไม่ได้เป็นผู้ร้องขอใช่ไหม?</p><br>";
                html += "<p>ไม่ต้องกังวลค่ะ/ครับ ข้อมูลของคุณยังคงปลอดภัย หากคุณไม่ได้ดำเนินการใดๆ สามารถเพิกเฉยต่ออีเมลฉบับนี้ได้</p><br>";
                html += "<p>ขอบคุณที่ไว้วางใจเราเสมอค่ะ/ครับ</p><br>";
                html += "<p>ทีมงาน : </p><br>";

                html += "<p>หากต้องการความช่วยเหลือเพิ่มเติม กรุณาติดต่อผู้ดูแลระบบที่ : </p><br>";
                html += "<p>เรียนรู้เพิ่มเติม : </p><br>";

                await settingMail.SendMail(SenderTo.Email, Email, Subject, html, _webHostEnvironment.WebRootPath); //body
            }
            else
            {
                throw new Exception("ไม่มี Email นี้อยู่ในระบบ กรุณาลองใหม่อีกครั้ง");
            }
            return new JsonResult(0);
        }

        public async Task<IActionResult> ResetPassword(string? Id, string? token, string? confrim_pass)
        {
            var i = 0;
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, confrim_pass);
                    if (result.Succeeded)
                    {
                        var haspassword = new PasswordHasher<RecruitmentUser>();
                        var edit = new RecruitmentUser
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            EmailConfirmed = false,
                            Firstname = user.Firstname,
                            Lastname = user.Lastname
                        };
                        user.RawPassword = confrim_pass;
                        user.PasswordHash = haspassword.HashPassword(edit, confrim_pass);
                        _context.Entry(user).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        i = 1;
                    }
                }
                else
                {
                    throw new Exception("ไม่มี user นี้อยู่ในระบบ กรุณาลองใหม่อีกครั้ง");
                }
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }

        }

        public async Task<List<ResponseDTO.RecruitResponse>> GetRecruit()
        {
            List<ResponseDTO.RecruitResponse> UserResponse = new List<ResponseDTO.RecruitResponse>();

            UserResponse = await _context.Users
                .Join(_context.UserRoles,
                u => u.Id,
                ur => ur.UserId,
                (u, ur) => new { u, ur })
                .Join(_context.Roles,
                n=>n.ur.RoleId,
                r=>r.Id,
                (n,r) => new ResponseDTO.RecruitResponse { 
                    Id = n.u.Id,
                    Firstname = n.u.Firstname,
                    Lastname = n.u.Lastname,
                    role = r.Name,
                })
                .Where(x=>x.role == "recruiter")
                .ToListAsync();


            return UserResponse;
        }
    }
}
