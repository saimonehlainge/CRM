using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Pages.Backend;
using Recruitment.Services;
using System.Net;

//#pragma warning disable CS0618 // Type or member is obsolete
//#pragma warning disable CS8604 // Possible null reference argument.
//#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
//#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace Recruitment.Repositories
{
    public interface ICDDRepository : IGenericRepository<CDD>
    {
        //Task<IActionResult> InsertCDD(RequestDTO.DTORequest request);
        Task InsertCDD(RequestDTO.DTORequest request);

        Task<JsonResult> UpdateCDD(RequestDTO.DTORequest request);
        Task<JsonResult> ChangeStatus(Notification request);
    }

    public class CDDRepository : GenericRepository<CDD>, ICDDRepository
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly INotificationHub _notificationHub;
        private readonly SettingMail settingMail = new SettingMail();
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILoggerHelperRepository _logger;

        public CDDRepository(RecruitmentContext context, IHttpContextAccessor accessor, INotificationHub notificationHub, IWebHostEnvironment webHostEnvironment, ILoggerHelperRepository logger) : base(context)
        {
            _accessor = accessor;
            _notificationHub = notificationHub;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public async Task InsertCDD(RequestDTO.DTORequest request)
        {
            //try
            //{
            CDD cdd = new CDD();
            cdd.UserId = request.userId;
            cdd.TypeCdd = request.type_cdd;
            cdd.Company = request.Company;
            cdd.Announcement = request.Announcement;
            cdd.Position = request.Position;
            cdd.HRContact = request.HRContact?.Trim();
            cdd.Branch = request.Branch?.Trim();
            cdd.Salary = request.Salary;
            cdd.WorkArea = request.WorkArea;
            cdd.Prefix = request.Prefix;
            cdd.NameTH = request.NameTH?.Trim();
            cdd.NameENG = request.NameENG?.Trim();
            cdd.Nikname = request.Nikname?.Trim();
            cdd.Tel = request.Tel?.Trim();
            cdd.LineId = request.LineId?.Trim();
            cdd.Email = request.Email?.Trim();
            cdd.Birthday = request.Birthday;
            cdd.Religion = request.Religion;
            cdd.Nationality = request.Nationality;
            cdd.IDCard = request.IDCard?.Trim();
            cdd.EndDateIDCard = request.EndDateIDCard;
            cdd.MilitaryStatus = request.MilitaryStatus;
            cdd.MarriedStatus = request.MarriedStatus;
            cdd.IdCardAddress = request.IdCardAddress?.Trim();
            cdd.CurrentAddress = request.CurrentAddress?.Trim();
            cdd.Qualification = request.Qualification;
            cdd.StudyLocation = request.StudyLocation?.Trim();
            cdd.ComapnyName = request.ComapnyName?.Trim();
            cdd.StartEndWork = request.StartEndWork?.Trim();
            cdd.WorkPosition = request.WorkPosition?.Trim();
            cdd.WorkSalary = request.WorkSalary?.Trim();
            cdd.NoteWork = request.NoteWork?.Trim();
            cdd.LanguageAbility = request.LanguageAbility;
            cdd.LanguageAbilityOther = request.LanguageAbilityOther?.Trim();
            cdd.OtherSpecialAbility = request.OtherSpecialAbility?.Trim();
            cdd.WorkOtheProvinces = request.WorkOtheProvinces;
            cdd.HistoryCovidVaccine = request.HistoryCovidVaccine;
            cdd.WhoCanCheckName = request.WhoCanCheckName?.Trim();
            cdd.WhoCanCheckTel = request.WhoCanCheckTel?.Trim();
            cdd.WhoCanCheckAdress = request.WhoCanCheckAdress?.Trim();
            cdd.WhoCanCheckRelated = request.WhoCanCheckRelated?.Trim();
            cdd.EmergencyName = request.EmergencyName?.Trim();
            cdd.EmergencyTel = request.EmergencyTel?.Trim();
            cdd.EmergencyAdress = request.EmergencyAdress?.Trim();
            cdd.EmergencyRelated = request.EmergencyRelated?.Trim();

            if (request.DocumentCDD != null)
            {
                var subtext = request.DocumentCDD.FileName.Split('.');
                var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                using (Stream filestream = new FileStream(filePath, FileMode.Create))
                {
                    request.DocumentCDD.CopyTo(filestream);
                }
                cdd.DocumentCDD = filename;
            }

            if (request.DocumentWorkCertification != null)
            {
                var subtext = request.DocumentWorkCertification.FileName.Split('.');
                var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                using (Stream filestream = new FileStream(filePath, FileMode.Create))
                {
                    request.DocumentWorkCertification.CopyTo(filestream);
                }
                cdd.DocumentWorkCertification = filename;
            }

            if (request.DocumentBank != null)
            {
                var subtext = request.DocumentBank.FileName.Split('.');
                var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                using (Stream filestream = new FileStream(filePath, FileMode.Create))
                {
                    request.DocumentBank.CopyTo(filestream);
                }
                cdd.DocumentBank = filename;
            }

            if (request.DocumentHistroy != null)
            {
                var subtext = request.DocumentHistroy.FileName.Split('.');
                var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                using (Stream filestream = new FileStream(filePath, FileMode.Create))
                {
                    request.DocumentHistroy.CopyTo(filestream);
                }
                cdd.DocumentHistroy = filename;
            }

            if (request.DocumentJng != null)
            {
                var subtext = request.DocumentJng.FileName.Split('.');
                var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                using (Stream filestream = new FileStream(filePath, FileMode.Create))
                {
                    request.DocumentJng.CopyTo(filestream);
                }
                cdd.DocumentJng = filename;
            }

            cdd.Status = request.Status;
            cdd.CreatedDate = DateTime.Now;
            cdd.UpdatedDate = DateTime.Now;
            _context.Add(cdd);

            await _context.SaveChangesAsync();

            var notify = new Notification
            {
                Message = "มีคนสมัครงาน ชื่อคุณ " + cdd.NameTH,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CDDId = cdd.Id,
                userid = cdd.UserId,
                Type = 1
            };

            _context.Add(notify);

            await _context.SaveChangesAsync();

            if (request.Usedwork != null)
            {
                CDDCondition cDDCondition = new CDDCondition();
                cDDCondition.Usedwork = request.Usedwork;
                cDDCondition.Applicant_age = request.Applicant_age;
                cDDCondition.Applicant_pregnant = request.Applicant_pregnant;
                cDDCondition.Ever_applied = request.Ever_applied;
                cDDCondition.Studying = request.Studying;
                cDDCondition.Defect = request.Defect;
                cDDCondition.Current_degree = request.Current_degree;
                cDDCondition.Under_pressure = request.Under_pressure;
                cDDCondition.Government_work = request.Government_work;
                cDDCondition.Specific_qualifications = request.Specific_qualifications;
                cDDCondition.History_congenital_disease = request.History_congenital_disease;
                cDDCondition.Congenital_disease = request.Congenital_disease;
                cDDCondition.congenitaldisease_detail = request.congenitaldisease_detail;
                cDDCondition.Wage = request.Wage;
                cDDCondition.Social_security = request.Social_security;
                cDDCondition.Work6stop1 = request.Work6stop1;
                cDDCondition.Deduction_wages = request.Deduction_wages;
                cDDCondition.Deduction_wages50 = request.Deduction_wages50;
                cDDCondition.Numbercalls = request.Numbercalls;
                cDDCondition.Numbercalls15 = request.Numbercalls15;
                cDDCondition.Missing_work = request.Missing_work;
                cDDCondition.Cost_work2 = request.Cost_work2;
                cDDCondition.Trend12 = request.Trend12;
                cDDCondition.Trend23 = request.Trend23;
                cDDCondition.Confirm_info = request.Confirm_info;
                cDDCondition.Nationality2 = request.Nationality2;
                cDDCondition.CDDId = cdd.Id;

                cDDCondition.CreatedDate = DateTime.Now;
                cDDCondition.UpdatedDate = DateTime.Now;
                _context.Add(cDDCondition);
                await _context.SaveChangesAsync();
            }

            //var Subject = "ยื่นแบบฟอร์มการสมัครงาน";
            //var body = cdd.NameTH + " ได้ยื่นแบบฟอร์มให้กับทางเรา เรียบร้อยแล้ว";

            //var SenderTo = (await _context.EmailSetting.FirstOrDefaultAsync())!;  //mail;
            //if (SenderTo == null)
            //{
            //    var configuration = GetConfiguration();
            //    SenderTo.Email = configuration.GetSection("MailSettings:Mail").Value;
            //}

            //await settingMail.SendMail(SenderTo.Email!, cdd.Email!, Subject, body, _webHostEnvironment.WebRootPath);


            //await _context.SaveChangesAsync();

            _logger.LogMessage("บันทึกข้อมูลเรียบร้อย " + DateTime.Now);

            //    return new JsonResult(new { status = "success", messageArray = "success" });
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogMessage("เกิดข้อผิดพลาด " + "error : " + ex + " inner : " + ex.InnerException);
            //    throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            //}
        }

        public async Task<JsonResult> UpdateCDD(RequestDTO.DTORequest request)
        {
            try
            {
                var cdd = await _context.CDD.FindAsync(request.Id);
                if (cdd != null)
                {
                    cdd.TypeCdd = request.type_cdd;
                    cdd.Company = request.Company;
                    cdd.Announcement = request.Announcement;
                    cdd.Position = request.Position;
                    cdd.HRContact = request.HRContact;
                    cdd.Branch = request.Branch;
                    cdd.Salary = request.Salary;
                    cdd.WorkArea = request.WorkArea;
                    cdd.Prefix = request.Prefix;
                    cdd.NameTH = request.NameTH;
                    cdd.NameENG = request.NameENG;
                    cdd.Nikname = request.Nikname;
                    cdd.Tel = request.Tel;
                    cdd.LineId = request.LineId;
                    cdd.Email = request.Email;
                    cdd.Birthday = request.Birthday;
                    cdd.Religion = request.Religion;
                    cdd.Nationality = request.Nationality;
                    cdd.IDCard = request.IDCard;
                    cdd.EndDateIDCard = request.EndDateIDCard;
                    cdd.MilitaryStatus = request.MilitaryStatus;
                    cdd.MarriedStatus = request.MarriedStatus;
                    cdd.IdCardAddress = request.IdCardAddress;
                    cdd.CurrentAddress = request.CurrentAddress;
                    cdd.Qualification = request.Qualification;
                    cdd.StudyLocation = request.StudyLocation;
                    cdd.ComapnyName = request.ComapnyName;
                    cdd.StartEndWork = request.StartEndWork;
                    cdd.WorkPosition = request.WorkPosition;
                    cdd.WorkSalary = request.WorkSalary;
                    cdd.NoteWork = request.NoteWork;
                    cdd.LanguageAbility = request.LanguageAbility;
                    cdd.LanguageAbilityOther = request.LanguageAbilityOther;
                    cdd.OtherSpecialAbility = request.OtherSpecialAbility;
                    cdd.WorkOtheProvinces = request.WorkOtheProvinces;
                    cdd.HistoryCovidVaccine = request.HistoryCovidVaccine;
                    cdd.WhoCanCheckName = request.WhoCanCheckName;
                    cdd.WhoCanCheckTel = request.WhoCanCheckTel;
                    cdd.WhoCanCheckAdress = request.WhoCanCheckAdress;
                    cdd.WhoCanCheckRelated = request.WhoCanCheckRelated;
                    cdd.EmergencyName = request.EmergencyName;
                    cdd.EmergencyTel = request.EmergencyTel;
                    cdd.EmergencyAdress = request.EmergencyAdress;
                    cdd.EmergencyRelated = request.EmergencyRelated;

                    if (request.DocumentCDD != null)
                    {
                        var subtext = request.DocumentCDD.FileName.Split('.');
                        var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                        using (Stream filestream = new FileStream(filePath, FileMode.Create))
                        {
                            request.DocumentCDD.CopyTo(filestream);
                        }
                        cdd.DocumentCDD = filename;
                    }
                    if (request.DocumentWorkCertification != null)
                    {
                        var subtext = request.DocumentWorkCertification.FileName.Split('.');
                        var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                        using (Stream filestream = new FileStream(filePath, FileMode.Create))
                        {
                            request.DocumentWorkCertification.CopyTo(filestream);
                        }
                        cdd.DocumentWorkCertification = filename;
                    }
                    if (request.DocumentBank != null)
                    {
                        var subtext = request.DocumentBank.FileName.Split('.');
                        var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                        using (Stream filestream = new FileStream(filePath, FileMode.Create))
                        {
                            request.DocumentBank.CopyTo(filestream);
                        }
                        cdd.DocumentBank = filename;
                    }
                    if (request.DocumentHistroy != null)
                    {
                        var subtext = request.DocumentHistroy.FileName.Split('.');
                        var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                        using (Stream filestream = new FileStream(filePath, FileMode.Create))
                        {
                            request.DocumentHistroy.CopyTo(filestream);
                        }
                        cdd.DocumentHistroy = filename;
                    }
                    if (request.DocumentJng != null)
                    {
                        var subtext = request.DocumentJng.FileName.Split('.');
                        var filename = subtext[0] + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-fff") + "." + subtext[1];
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/File/", filename);
                        using (Stream filestream = new FileStream(filePath, FileMode.Create))
                        {
                            request.DocumentJng.CopyTo(filestream);
                        }
                        cdd.DocumentJng = filename;
                    }
                    if (request.Status != cdd.Status && request.Status != null) // change by sai mon
                    {
                        cdd.Status = request.Status;
                    }

                    //cdd.Status = request.Status;
                    cdd.CreatedDate = DateTime.Now;
                    cdd.UpdatedDate = DateTime.Now;
                    _context.Update(cdd);
                    await _context.SaveChangesAsync();

                    if (request.Usedwork != null)
                    {
                        var cDDCondition = await _context.CDDCondition.FirstOrDefaultAsync(x => x.CDDId == request.Id);
                        if (cDDCondition != null)
                        {
                            cDDCondition.Usedwork = request.Usedwork;
                            cDDCondition.Applicant_age = request.Applicant_age;
                            cDDCondition.Applicant_pregnant = request.Applicant_pregnant;
                            cDDCondition.Ever_applied = request.Ever_applied;
                            cDDCondition.Studying = request.Studying;
                            cDDCondition.Defect = request.Defect;
                            cDDCondition.Current_degree = request.Current_degree;
                            cDDCondition.Under_pressure = request.Under_pressure;
                            cDDCondition.Government_work = request.Government_work;
                            cDDCondition.Specific_qualifications = request.Specific_qualifications;
                            cDDCondition.History_congenital_disease = request.History_congenital_disease;
                            cDDCondition.Congenital_disease = request.Congenital_disease;
                            cDDCondition.Wage = request.Wage;
                            cDDCondition.Social_security = request.Social_security;
                            cDDCondition.Work6stop1 = request.Work6stop1;
                            cDDCondition.Deduction_wages = request.Deduction_wages;
                            cDDCondition.Deduction_wages50 = request.Deduction_wages50;
                            cDDCondition.Numbercalls = request.Numbercalls;
                            cDDCondition.Numbercalls15 = request.Numbercalls15;
                            cDDCondition.Missing_work = request.Missing_work;
                            cDDCondition.Cost_work2 = request.Cost_work2;
                            cDDCondition.Trend12 = request.Trend12;
                            cDDCondition.Trend23 = request.Trend23;
                            cDDCondition.Confirm_info = request.Confirm_info;
                            cDDCondition.Nationality2 = request.Nationality2;
                            cDDCondition.CDDId = cdd.Id;
                            cDDCondition.CreatedDate = DateTime.Now;
                            cDDCondition.UpdatedDate = DateTime.Now;
                            _context.Update(cDDCondition);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return new JsonResult(new { status = "success", messageArray = "success" });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<JsonResult> ChangeStatus(Notification request)
        {
            try
            {
                _context.Notification.Add(request);
                await _context.SaveChangesAsync();

                return new JsonResult(new { status = "success", messageArray = "success" });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
