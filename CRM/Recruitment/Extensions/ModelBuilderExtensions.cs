using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;

namespace Recruitment.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            string pwd = "abcd1234";// || "Abcd1234@";
            var pwdhasher = new PasswordHasher<RecruitmentUser>();

            #region Seed Role
            List<Role> roles = new List<Role>() {
                new Role()
                {
                    Name = "admin",
                    NormalizedName = "admin".ToUpper(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Role()
                {
                    Name = "md",
                    NormalizedName = "md".ToUpper(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Role()
                {
                    Name = "manager",
                    NormalizedName = "manager".ToUpper(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Role()
                {
                    Name = "sup",
                    NormalizedName = "sup".ToUpper(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Role()
                {
                    Name = "project owner",
                    NormalizedName = "project owner".ToUpper(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Role()
                {
                    Name = "recruiter",
                    NormalizedName = "recruiter".ToUpper(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            builder.Entity<Role>().HasData(roles);
            #endregion

            #region Seed user
            var admin = new RecruitmentUser
            {
                UserName = "admin@recruitment.com"
                ,
                Email = "admin@recruitment.com",
                EmailConfirmed = false,
                Firstname = "admin",
                Lastname = "admin",
            };
            admin.NormalizedUserName = admin.UserName.ToUpper();
            admin.NormalizedEmail = admin.Email.ToUpper();
            //admin.RawPassword = pwd;
            admin.PasswordHash = pwdhasher.HashPassword(admin, pwd);
            admin.CreatedDate = DateTime.Now;
            admin.UpdatedDate = DateTime.Now;

            //var user = new RecruitmentUser
            //{
            //    UserName = "user@recruitment.com",
            //    Email = "user@recruitment.com",
            //    EmailConfirmed = false,
            //    Firstname = "user",
            //    Lastname = "user",
            //};
            //user.NormalizedUserName = user.UserName.ToUpper();
            //user.NormalizedEmail = user.Email.ToUpper();
            ////user.RawPassword = pwd;
            //user.PasswordHash = pwdhasher.HashPassword(user, pwd);
            //user.CreatedDate = DateTime.Now;
            //user.UpdatedDate = DateTime.Now;

            List<RecruitmentUser> listuser = new List<RecruitmentUser>()
            {
                admin,
                //user
            };

            builder.Entity<RecruitmentUser>().HasData(listuser);
            #endregion

            #region Seed UserRole
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = listuser[0].Id,
                RoleId = roles.FirstOrDefault(x => x.Name == "admin")!.Id
            });

            //userRoles.Add(new IdentityUserRole<string>
            //{
            //    UserId = listuser[1].Id,
            //    RoleId = roles.FirstOrDefault(x => x.Name == "user")!.Id
            //});

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            #endregion

            #region Seed Announcement
            List<Announcement> announcement = new List<Announcement>() {
                new Announcement()
                {
                    Id = 1,
                    Name = "Facebook",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 2,
                    Name = "Line",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 3,
                    Name = "JobThaiweb",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 4,
                    Name = "Jobfinfin",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 5,
                    Name = "Facebook Group",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 6,
                    Name = "โทรศัพท์",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 7,
                    Name = "Line @",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 8,
                    Name = "Facebook Page",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 9,
                    Name = "Friend get Friend",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 10,
                    Name = "80 List",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 11,
                    Name = "JobBKK",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 12,
                    Name = "JobPub",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 13,
                    Name = "JobThai",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Announcement()
                {
                    Id = 14,
                    Name = "อื่นๆ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            builder.Entity<Announcement>().HasData(announcement);
            #endregion

            #region Seed Position
            List<Position> position = new List<Position>() {
                new Position()
                {
                    Id = 1,
                    Name = "TSR",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 2,
                    Name = "TSR Collecter",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 3,
                    Name = "TSR Executive",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 4,
                    Name = "HR Recruit",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 5,
                    Name = "Leader",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 6,
                    Name = "Ac",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 7,
                    Name = "TVD-อาหารเสริม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 8,
                    Name = "Call Center-AIA",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 9,
                    Name = "Call Service-AIA",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 10,
                    Name = "Reinstatement",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 11,
                    Name = "Direct Sales",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 12,
                    Name = "UBO Leader",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 13,
                    Name = "Supervisor",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 14,
                    Name = "Truck Driver",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 15,
                    Name = "Sales Executive",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id = 16,
                    Name = "Administrative",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Position()
                {
                    Id= 17,
                    Name = "Accounting",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            builder.Entity<Position>().HasData(position);
            #endregion

            #region Seed Prefix
            List<Prefix> prefix = new List<Prefix>() {
                new Prefix()
                {
                    Id = 1,
                    Name = "นาย",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Prefix()
                {
                    Id = 2,
                    Name = "นางสาว",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Prefix()
                {
                    Id = 3,
                    Name = "นาง",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Prefix()
                {
                    Id = 4,
                    Name = "ไม่ระบุ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
            };
            builder.Entity<Prefix>().HasData(prefix);
            #endregion

            #region Seed Religion
            List<Religion> religion = new List<Religion>() {
                new Religion()
                {
                    Id = 1,
                    Name = "ศาสนาพุทธ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Religion()
                {
                    Id = 2,
                    Name = "ศาสนาคริสต์",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Religion()
                {
                    Id = 3,
                    Name = "ศาสนาอิสลาม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Religion()
                {
                    Id = 4,
                    Name = "อิ่นๆ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
            };
            builder.Entity<Religion>().HasData(religion);
            #endregion

            #region Seed Nationality
            List<Nationality> nationality = new List<Nationality>() {
                new Nationality()
                {
                    Id = 1,
                    Name = "ไทย",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Nationality()
                {
                    Id = 2,
                    Name = "อิ่นๆ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
            };
            builder.Entity<Nationality>().HasData(nationality);
            #endregion

            #region Seed MilitaryStatus
            List<MilitaryStatus> militaryStatus = new List<MilitaryStatus>() {
                new MilitaryStatus()
                {
                    Id = 1,
                    Name = "ยังไม่เกณฑ์ทหาร",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new MilitaryStatus()
                {
                    Id = 2,
                    Name = "เกณฑ์ทหารแล้ว",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
            };
            builder.Entity<MilitaryStatus>().HasData(militaryStatus);
            #endregion

            #region Seed MarriedStatus
            List<MarriedStatus> marriedStatus = new List<MarriedStatus>() {
                new MarriedStatus()
                {
                    Id = 1,
                    Name = "โสด",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new MarriedStatus()
                {
                    Id = 2,
                    Name = "สมรส",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new MarriedStatus()
                {
                    Id = 3,
                    Name = "หย่าร้าง",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new MarriedStatus()
                {
                    Id = 4,
                    Name = "หม้าย",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new MarriedStatus()
                {
                    Id = 5,
                    Name = "แยกกัน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
            };
            builder.Entity<MarriedStatus>().HasData(marriedStatus);
            #endregion

            #region Seed Qualification
            List<Qualification> qualification = new List<Qualification>() {
                new Qualification()
                {
                    Id = 1,
                    Name = "มัธยมต้น",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Qualification()
                {
                    Id = 2,
                    Name = "มัธยมปลาย",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Qualification()
                {
                    Id = 3,
                    Name = "ป.ตรี",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Qualification()
                {
                    Id = 4,
                    Name = "ป.โท",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Qualification()
                {
                    Id = 5,
                    Name = "ป.เอก",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
            };
            builder.Entity<Qualification>().HasData(qualification);
            #endregion

            #region Seed LanguageAbility
            List<LanguageAbility> languageAbility = new List<LanguageAbility>() {
                new LanguageAbility()
                {
                    Id = 1,
                    Name = "ภาษาอังกฤษ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            builder.Entity<LanguageAbility>().HasData(languageAbility);
            #endregion

            #region Seed HistoryCovid
            List<HistoryCovid> historyCovid = new List<HistoryCovid>() {
                new HistoryCovid()
                {
                    Id = 1,
                    Name = "1 เข็ม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new HistoryCovid()
                {
                    Id = 2,
                    Name = "2 เข็ม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new HistoryCovid()
                {
                    Id = 3,
                    Name = "3 เข็ม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new HistoryCovid()
                {
                    Id = 4,
                    Name = "ยังไม่ได้รับวัคซีน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new HistoryCovid()
                {
                    Id = 5,
                    Name = "อื่นๆ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            builder.Entity<HistoryCovid>().HasData(historyCovid);
            #endregion

            #region Seed Project
            List<Project> project = new List<Project>() {
                new Project()
                {
                    Id = 1,
                    Name = "Project",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 2,
                    Name = "AIA",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 3,
                    Name = "AZAY",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 4,
                    Name = "TCIB",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 5,
                    Name = "TVDxAIA",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 6,
                    Name = "TVD",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 7,
                    Name = "Cressi",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 8,
                    Name = "BSP",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 9,
                    Name = "BSP(HR)",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 10,
                    Name = "BSP(MGR)",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 11,
                    Name = "R89 RC",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 12,
                    Name = "AC1",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 13,
                    Name = "Muvmii",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 14,
                    Name = "Lazada",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 15,
                    Name = "Sale Marketing",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 16,
                    Name = "AWC",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Project()
                {
                    Id = 17,
                    Name = "Next Job",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            builder.Entity<Project>().HasData(project);
            #endregion

            #region Seed RecuritStatus
            List<RecruitStatus> recruitstatus = new List<RecruitStatus>() {
                new RecruitStatus()
                {
                    Id = 1,
                    Name = "ไม่สนใจ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 2,
                    Name = "ไม่ผ่านคุณสมบัติ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 3,
                    Name = "ไม่ตอบ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 4,
                    Name = "นัดสัมภาษณ์แล้ว",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 5,
                    Name = "เพื่อติดตาม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 6,
                    Name = "รอสัมภาษณ์_ลูกค้า",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 7,
                    Name = "ไม่ไปตามนัด_ลูกค้า",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 8,
                    Name = "ไม่ผ่านสัมภาษณ์_ลูกค้า",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 9,
                    Name = "ไม่ผ่านสัมภาษณ์_ลูกค้า2",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 10,
                    Name = "รอผลสัมภาษณ์_ลูกค้า",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 11,
                    Name = "รอเริ่มงาน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 12,
                    Name = "ผู้สมัคร_นัดแล้วไม่มา",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 13,
                    Name = "ผู้สมัคร_ยกเลิก (แจ้งยกเลิก)",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 14,
                    Name = "ผู้สมัคร_สอบใบอนุญาตไม่ผ่าน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 15,
                    Name = "ผู้สมัคร_ไปเริ่มงานแล้วไม่ชอบ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 16,
                    Name = "ผู้สมัคร_ไม่ผ่านเทรนงาน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 17,
                    Name = "ผู้สมัคร_ไม่ผ่านการสอบ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 18,
                    Name = "ไม่สนใจ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 19,
                    Name = "ลูกค้า_ผิดเงื่อนไข",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 20,
                    Name = "ไม่ไปเริ่มงาน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 21,
                    Name = "ลาออกก่อนวางบิล",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 22,
                    Name = "เริ่มงานแล้วรอวางบิล",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 23,
                    Name = "ติดตามให้พ้นการันตี",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 24,
                    Name = "KPI_0.5ครั้งที่#1",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 25,
                    Name = "KPI_0.5ครั้งที่#2",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 26,
                    Name = "KPI_1.0",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 27,
                    Name = "KPI_ตามตำแหน่งงาน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 28,
                    Name = "ค่าคอมเท่านั้น",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 29,
                    Name = "ค่าคอมคนสอนน้องใหม่",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 30,
                    Name = "ค่าคอมช่วยเหลือน้องใหม่",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 31,
                    Name = "Replace",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new RecruitStatus()
                {
                    Id = 32,
                    Name = "ครบการันตี",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            builder.Entity<RecruitStatus>().HasData(recruitstatus);
            #endregion

            #region Seed WorkOtherProvince
            List<WorkOtherProvinces> workOtherProvinces = new List<WorkOtherProvinces>() {
                new WorkOtherProvinces()
                {
                    Id = 1,
                    Name = "ได้",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new WorkOtherProvinces()
                {
                    Id = 2,
                    Name = "ไม่ได้",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };
            builder.Entity<WorkOtherProvinces>().HasData(workOtherProvinces);
            #endregion

            #region Seed PermissionMenu
            List<PermissionDetail> permissionMenus = new List<PermissionDetail>() {
                new PermissionDetail()
                {
                    Id = 1,
                    Name = "สร้างข้อมูล",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 2,
                    Name = "แก้ไขข้อมูล Masterdate List",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 3,
                    Name = "ตั้งค่าระบบ",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 4,
                    Name = "ลบข้อมูล",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 5,
                    Name = "เห็นข้อมูลของทีม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 6,
                    Name = "เห็นข้อมูล Project ทั้งหมด",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 7,
                    Name = "สร้างข้อมูลในปฎิทิน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 8,
                    Name = "เห็นข้อมูลในปฎิทิน",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 9,
                    Name = "เห็นเฉพาะข้อมูลตนเอง",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 10,
                    Name = "เห็นเฉพาะข้อมูล Project ที่ตัวเองดูแล",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 11,
                    Name = "แก้ไขเฉพาะข้อมูลตนเอง",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 12,
                    Name = "แก้ไขข้อมูลของทีม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 13,
                    Name = "ดึงรายงานข้อมูลของทีม",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 14,
                    Name = "ดึงรายงานข้อมูลตนเอง",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 15,
                    Name = "ดึงรายงานข้อมูลของ Project",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
                new PermissionDetail()
                {
                    Id = 16,
                    Name = "ดึงรายงานข้อมูล Target Vs Achieved",
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                },
            };
            builder.Entity<PermissionDetail>().HasData(permissionMenus);
            #endregion
        }
    }
}
