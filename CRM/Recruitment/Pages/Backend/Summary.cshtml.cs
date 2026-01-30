using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Recruitment.Areas.Identity.Data;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;
using Recruitment.Services;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Calendar = Recruitment.Areas.Identity.Data.Calendar;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class SummaryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationHub _notificationHub;

        public SummaryModel(IUnitOfWork unitOfWork, INotificationHub notificationHub)
        {
            _unitOfWork = unitOfWork;
            _notificationHub = notificationHub;
        }

        public async Task OnGet()
        {


            // กรอง Claims ที่จะเก็บไว้ (ไม่เอา Department เดิม)




            // var newClaims = currentClaims..ToList();

            // เพิ่ม Claim ใหม่
            //newClaims.Add(); // 

            //new
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTable(string? firstname, string? lastname, int? project, int? status, string? startdate, string? enddate, string? month, string? year, int? Company, int? Position)
        {
            //try
            //{
            string? draw = Request.Form["draw"];
            string? start = Request.Form["start"];
            string? length = Request.Form["length"];
            string? sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string? sortColumnDirection = Request.Form["order[0][dir]"];
            string? searchValue = Request.Form["search[value]"];
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int? recordsTotal = 0;

            var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();
            var StatsuRecruit = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
            var GetProject = await _unitOfWork.ProjectRepository.GetAllAsync();
            var (userid, roles, _, department) = User.GetUser();
            var getuser = await _unitOfWork.UsersRepository.GetAllAsync();

            if (roles == "project owner")
            {

                var roles_projectowner = getuser.FirstOrDefault(x => x.Id == userid);
                var f_GetProject = GetProject.FirstOrDefault(x => x.Id.ToString() == (roles_projectowner.Project == null ? null : roles_projectowner.Project.ToString()));
                if (f_GetProject != null)
                {
                    GetDB = GetDB.Where(x => x.Project == f_GetProject.Id).ToList();
                }
            }
            else if (roles == "recruiter")
            {
                var per = await _unitOfWork.PermissionSettingRepository.GetAllAsync();
                var Permission = per.Where(x => x.Permission == roles && x.viewproject == true).ToList();
                if (Permission.Count() == 0)
                {

                    Permission = per.Where(x => x.Permission == roles && x.viewprojectmanage == true || x.viewprojectmanage == true).ToList();
                    if (Permission.Count() == 0)
                    {
                        GetDB = new List<CDD>();
                    }
                    else
                    {
                        GetDB = GetDB.Where(x => x.UserId == userid && x.DeleteAt != 1).ToList();
                    }
                }
            }
            else if (roles != "project owner")
            {
                var per = await _unitOfWork.PermissionSettingRepository.GetAllAsync();
                var Permission = per.Where(x => x.Permission == roles && x.viewproject == true).ToList();
                if (Permission.Count() == 0)
                {
                    Permission = per.Where(x => x.Permission == roles && x.viewprojectmanage == true || x.viewprojectmanage == true).ToList();
                    if (Permission.Count() == 0)
                    {
                        GetDB = new List<CDD>(); ///null;
                    }
                    else
                    {
                        //GetDB = GetDB.Where(x => x.UserId == userid && x.DeleteAt != 1).ToList();
                    }
                }
            }

            var fixDate = DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null);
            GetDB = GetDB.Where(x => x.CreatedDate != null && x.CreatedDate.Value.Date >= fixDate.Date).ToList();

            List<ResponseDTO.SummaryDataResponse> NewGetDB = new List<ResponseDTO.SummaryDataResponse>();
            NewGetDB = GetDB.Select(x => new ResponseDTO.SummaryDataResponse
            {
                DateStatus = x.Datestatus,
                Announcement = x.Announcement == null ? null : x.Announcement,
                Salary = x.Salary == null ? null : x.Salary,
                StartEndWork = x.StartEndWork == null ? null : x.StartEndWork,
                Status = x.Status == null ? null : x.Status,
                StatusEnd = x.StatusEnd == null ? null : x.StatusEnd,
                StatusStart = x.StatusStart == null ? null : x.StatusStart,
                StudyLocation = x.StudyLocation == null ? null : x.StudyLocation,
                MarriedStatus = x.MarriedStatus == null ? null : x.MarriedStatus,
                MilitaryStatus = x.MilitaryStatus == null ? null : x.MilitaryStatus,
                NoteStatus = x.NoteStatus == null ? null : x.NoteStatus,
                OtherSpecialAbility = x.NoteStatus == null ? null : x.NoteStatus,
                WorkSalary = x.WorkSalary == null ? null : x.WorkSalary,
                BillingCycle = x.BillingCycle == null ? null : x.BillingCycle,
                Birthday = x.Birthday == null ? null : x.Birthday,
                Branch = x.Branch == null ? null : x.Branch,
                Cause = x.Cause == null ? null : x.Cause,
                ComapnyName = x.ComapnyName == null ? null : x.ComapnyName,
                Company = x.Company == null ? null : x.Company,
                CreatedDate = x.CreatedDate == null ? null : x.CreatedDate,
                CurrentAddress = x.CurrentAddress == null ? null : x.CurrentAddress,
                DeleteAt = x.DeleteAt == null ? null : x.DeleteAt,
                DocumentBank = x.DocumentBank == null ? null : x.DocumentBank,
                DocumentCDD = x.DocumentCDD == null ? null : x.DocumentCDD,
                DocumentHistroy = x.DocumentHistroy == null ? null : x.DocumentHistroy,
                DocumentJng = x.DocumentJng == null ? null : x.DocumentJng,
                DocumentWorkCertification = x.DocumentWorkCertification == null ? null : x.DocumentWorkCertification,
                Email = x.Email == null ? null : x.Email,
                EmergencyAdress = x.EmergencyAdress == null ? null : x.EmergencyAdress,
                EmergencyName = x.EmergencyName == null ? null : x.EmergencyName,
                EmergencyRelated = x.EmergencyRelated == null ? null : x.EmergencyRelated,
                EmergencyTel = x.EmergencyTel == null ? null : x.EmergencyTel,
                EndDateIDCard = x.EndDateIDCard == null ? null : x.EndDateIDCard,
                HistoryCovidVaccine = x.HistoryCovidVaccine == null ? null : x.HistoryCovidVaccine,
                HRContact = x.HRContact == null ? null : x.HRContact,
                Id = x.Id,
                IDCard = x.IDCard == null ? null : x.IDCard,
                IdCardAddress = x.IDCard == null ? null : x.IDCard,
                KPI = x.KPI == null ? null : x.KPI,
                LanguageAbility = x.LanguageAbility == null ? null : x.LanguageAbility,
                LanguageAbilityOther = x.LanguageAbilityOther == null ? null : x.LanguageAbilityOther,
                LineId = x.LineId == null ? null : x.LineId,
                NameENG = x.Nikname == null ? null : x.Nikname,
                NameTH = x.NameTH == null ? null : x.NameTH,
                Nationality = x.Nationality == null ? null : x.Nationality,
                Nikname = x.Nikname == null ? null : x.Nikname,
                NoteWork = x.NoteWork == null ? null : x.NoteWork,
                Position = x.Position == null ? null : x.Position,
                Prefix = x.Prefix == null ? null : x.Prefix,
                Project = x.Project == null ? null : x.Project,
                Qualification = x.Qualification == null ? null : x.Qualification,
                Religion = x.Religion == null ? null : x.Religion,
                Tel = x.Tel == null ? null : x.Tel,
                TypeCdd = x.TypeCdd == null ? null : x.TypeCdd,
                UpdatedDate = x.UpdatedDate == null ? null : x.UpdatedDate,
                UserId = x.UserId == null ? null : getuser.FirstOrDefault(a => (a.Id != null && a.Id == x.UserId))?.Firstname,
                WhoCanCheckAdress = x.WhoCanCheckAdress == null ? null : x.WhoCanCheckAdress,
                WhoCanCheckName = x.WhoCanCheckName == null ? null : x.WhoCanCheckName,
                WhoCanCheckRelated = x.WhoCanCheckRelated == null ? null : x.WhoCanCheckRelated,
                WhoCanCheckTel = x.WhoCanCheckTel == null ? null : x.WhoCanCheckTel,
                WorkArea = x.WorkArea == null ? null : x.WorkArea,
                WorkOtheProvinces = x.WorkOtheProvinces == null ? null : x.WorkOtheProvinces,
                WorkPosition = x.WorkPosition == null ? null : x.WorkPosition
            }).ToList();

            if (firstname != null && firstname != "")
            {
                NewGetDB = NewGetDB.Where(x =>
                x.NameTH != null && x.NameTH.Contains(firstname) ||
                x.NameENG != null && x.NameENG.Contains(firstname)
                ).ToList();
            }

            if (lastname != null && lastname != "")
            {
                NewGetDB = NewGetDB.Where(x =>
                    x.NameTH != null && x.NameTH.Contains(lastname) ||
                    x.NameENG != null && x.NameENG.Contains(lastname)
                    ).ToList();
            }

            if (project != null && project != -1)
            {
                NewGetDB = NewGetDB.Where(x => x.Project == project).ToList();
            }

            if (status != null && status != -1)
            {
                if (status == 24 || status == 25 || status == 27 || status == 28 || status == 30 || status == 37 || status == 38)
                {
                    NewGetDB = NewGetDB.Where(x => x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38).ToList();
                }
                else
                {
                    NewGetDB = NewGetDB.Where(x => x.Status == status).ToList();
                }
            }

            if (startdate != null && startdate != "" && enddate != null && enddate != "")
            {
                var S = Convert.ToDateTime(startdate, new CultureInfo("en-US"));
                var E = Convert.ToDateTime(enddate, new CultureInfo("en-US"));
                //NewGetDB = NewGetDB.Where(x => x.CreatedDate != null && x.CreatedDate.Value.Date >= S.Date && x.CreatedDate.Value.Date <= E.Date).ToList();
                NewGetDB = NewGetDB.Where(x => x.DateStatus != null && x.DateStatus.Value.Date >= S.Date && x.DateStatus.Value.Date <= E.Date).ToList(); //change by sai mon

            }

            if (month != null && month != "-1")
            {
                NewGetDB = NewGetDB.Where(x => x.BillingCycle.Contains(month)).ToList();
            }

            if (year != null && year != "")
            {
                NewGetDB = NewGetDB.Where(x => x.BillingCycle.Contains(year)).ToList();
            }

            if (Company != null && Company != -1)
            {
                NewGetDB = NewGetDB.Where(x => x.Company == Company).ToList();
            }

            if (Position != null && Position != -1)
            {
                NewGetDB = NewGetDB.Where(x => x.Position == Position).ToList();
            }

            var NewDB = NewGetDB.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.SummaryResponse
            {
                id = x.Id,
                recruit = x.UserId == null ? null : x.UserId,
                name = x.NameTH == null ? null : x.NameTH,
                nickname = x.Nikname == null ? null : x.Nikname,
                tel = x.Tel == null ? null : x.Tel,
                project = GetProject.FirstOrDefault(a => a.Id == x.Project) == null ? null : GetProject.FirstOrDefault(a => a.Id == x.Project).Name,
                kpi = x.KPI == null ? null : x.KPI,
                status = StatsuRecruit.FirstOrDefault(a => a.Id == x.Status) == null ? null : StatsuRecruit.FirstOrDefault(a => a.Id == x.Status).Name,
                Date = x.CreatedDate == null ? null : x.CreatedDate,
                typeform = x.TypeCdd == null ? null : x.TypeCdd
            }).ToList();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                switch (sortColumn)
                {
                    case "name":
                        NewDB = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? NewDB.OrderByDescending(x => x.name).ToList() : NewDB.OrderBy(x => x.name).ToList();
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                NewDB = NewDB.Where(x => x is { name: not null } && (x.name.Contains(searchValue))).ToList();
            }

            recordsTotal = NewDB.Count();
            NewDB = NewDB.Skip(skip).Take(pageSize).ToList();

            var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = NewDB };
            return new JsonResult(jsonData);
            //}
            //catch (Exception ex)
            //{
            //    return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            //}
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetStatus()
        {
            var db = await _unitOfWork.RecruitStatusRepository.StatusRecruit();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).OrderBy(x => x.Id).ToList();

            var (userid, roles, _, department) = User.GetUser();
            var user = await _unitOfWork.UsersRepository.GetAllAsync();
            var ss = user.FirstOrDefault(x => x.Id.ToLower() == userid.ToLower());
            List<ResponseDTO.RecruitStatusResponse> a = new List<ResponseDTO.RecruitStatusResponse>();
            if (ss.DepartmentId == 3)
            {
                var Gr = db.Where(x => x.typeform == 3 || x.typeform == 4).GroupBy(b => b.Name).ToList();
                foreach (var item in Gr)
                {
                    var ewe = db.FirstOrDefault(x => x.Name == item.Key);
                    a.Add(new ResponseDTO.RecruitStatusResponse
                    {
                        Status = ewe.Status,
                        project = ewe.project,
                        CreatedDate = ewe.CreatedDate,
                        DeleteAt = ewe.DeleteAt,
                        Id = ewe.Id,
                        Name = ewe.Name,
                        typeform = ewe.typeform,
                        UpdatedDate = ewe.UpdatedDate
                    });
                }
            }
            else
            {
                var Gr = db.Where(x => x.typeform == 1 || x.typeform == 2).GroupBy(b => b.Name).ToList();
                foreach (var item in Gr)
                {
                    var ewe = db.FirstOrDefault(x => x.Name == item.Key);
                    a.Add(new ResponseDTO.RecruitStatusResponse
                    {
                        Status = ewe.Status,
                        project = ewe.project,
                        CreatedDate = ewe.CreatedDate,
                        DeleteAt = ewe.DeleteAt,
                        Id = ewe.Id,
                        Name = ewe.Name,
                        typeform = ewe.typeform,
                        UpdatedDate = ewe.UpdatedDate
                    });
                }
            }
            return new JsonResult(a);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetProject()
        {
            var db = await _unitOfWork.ProjectRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetCompany()
        {
            var db = await _unitOfWork.CompanyRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetPosition()
        {
            var (userid, roles, _, department) = User.GetUser();
            var db = await _unitOfWork.PositionRepository.CoustomPosition(department);
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        public async Task<IActionResult> OnGetExportExcel(int? Project, string? StartDate, string? EndDate)
        {
            var FileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Export/" + DateTime.Now.Ticks + ".xlsx");
            Stream stream = new MemoryStream();
            using (var package = new ExcelPackage(new FileInfo(FileName)))
            {
                ExcelWorksheet Sheet = package.Workbook.Worksheets.Add("Summary");

                #region Column Table
                Sheet.Cells["A1"].Value = "#";
                Sheet.Cells["B1"].Value = "CDD Name";
                Sheet.Cells["C1"].Value = "Nickname";
                Sheet.Cells["D1"].Value = "Tel";
                Sheet.Cells["E1"].Value = "Project";
                Sheet.Cells["F1"].Value = "KPI";
                Sheet.Cells["G1"].Value = "Status";
                #endregion

                int? index = 2;
                var i = 1;
                var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();
                var NewDB = GetDB.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.ReportResponseExport
                {
                    id = x.Id,
                    name = x.NameTH,
                    nickname = x.Nikname,
                    tel = x.Tel,
                    project = null,
                    kpi = null,
                    status = x.Status,
                    Date = x.CreatedDate
                }).ToList();

                if (Project != null)
                {
                    NewDB = NewDB.Where(x => x.project == Project).ToList();
                }

                if (StartDate != null)
                {
                    NewDB = NewDB.Where(x => x.Date >= Convert.ToDateTime(StartDate).Date).ToList();
                }

                if (EndDate != null)
                {
                    NewDB = NewDB.Where(x => x.Date <= Convert.ToDateTime(EndDate)).ToList();
                }

                var project = await _unitOfWork.ProjectRepository.GetAllAsync();

                foreach (var item in NewDB)
                {
                    Sheet.Cells[string.Format("A{0}", index)].Value = (i = i + 1);
                    Sheet.Cells[string.Format("B{0}", index)].Value = item.name;
                    Sheet.Cells[string.Format("C{0}", index)].Value = item.nickname;
                    Sheet.Cells[string.Format("D{0}", index)].Value = item.tel;
                    Sheet.Cells[string.Format("E{0}", index)].Value = project.FirstOrDefault(x => x.Id == item.project) == null ? null : project.FirstOrDefault(x => x.Id == item.project).Name;
                    Sheet.Cells[string.Format("F{0}", index)].Value = "";
                    Sheet.Cells[string.Format("G{0}", index)].Value = item.status;
                    index++;
                }

                Sheet.Cells["A:AZ"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                Sheet.Cells["A:AZ"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A:AZ"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A:AZ"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A:AZ"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A:AZ"].AutoFitColumns();
                package.SaveAs(stream);
                stream.Position = 0;
            }
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CertificationsReport-" + FileName + "");
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetDeleteData(int? Id)
        {
            var i = 0;
            try
            {
                var CDD = await _unitOfWork.CDDRepository.GetByIdAsync(Id);
                if (CDD is not null)
                {
                    CDD.DeleteAt = 1;
                    await _unitOfWork.CompleteAsync();
                    i = 1;
                }
                else
                {
                    i = 2;
                }
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetCDD(int? Id)
        {
            try
            {
                var DB = await _unitOfWork.CDDRepository.GetByIdAsync(Id);
                if (DB == null)
                {
                    return BadRequest("ไม่มีข้อมูล");
                }

                var oldstatus = await _unitOfWork.RecruitStatusRepository.GetByIdAsync(DB.Status);
                return new JsonResult(new { db = DB, oldstatus = oldstatus });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetCalculateDay(int? Id, string? Date)
        {
            try
            {
                var DB = await _unitOfWork.CDDRepository.GetByIdAsync(Id);
                if (DB == null)
                {
                    return BadRequest("ไม่มีข้อมูล");
                }

                var setkpi = await _unitOfWork.SetKpiCommRepository.GetAllAsync();
                var DBFind0 = setkpi.FirstOrDefault(x => x.Project == DB.Project);
                double day = DBFind0 == null ? 0 : Convert.ToDouble(DBFind0.SetKpi_Day);
                var CollectionDay = "";
                double day2 = DBFind0 == null ? 0 : Convert.ToDouble(DBFind0.GuaranteeDay);
                var Guaranteed_ExpirationDate = "";
                if (Date == "Invalid Date" || Date == "1/1/1970")
                {
                    if (DB.StatusStart != null)
                    {
                        CollectionDay = DB.StatusStart!.Value.AddDays(day).ToString("dd/MM/yyyy");
                        Guaranteed_ExpirationDate = DB.StatusStart!.Value.AddDays(day2).ToString("dd/MM/yyyy");
                    }
                }
                else
                {
                    CollectionDay = Convert.ToDateTime(Date).AddDays(day).ToString("dd/MM/yyyy");
                    Guaranteed_ExpirationDate = Convert.ToDateTime(Date).AddDays(day2).ToString("dd/MM/yyyy");
                }
                return new JsonResult(new { Collection = CollectionDay, Guaranteed = Guaranteed_ExpirationDate });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostChangeCDD(RequestDTO.ChangeCDDRequset requset)
        {
            var i = 0;
            try
            {
                var DB = await _unitOfWork.CDDRepository.GetByIdAsync(requset.Id);
                if (DB == null)
                {
                    return BadRequest("ไม่มีข้อมูล");
                }
                else
                {
                    var Status = await _unitOfWork.RecruitStatusRepository.GetAllAsync();

                    //requset.datestatus = DateTime.Now; // add by sai mon

                    if (requset.userid != null)
                    {
                        DB.UserId = requset.userid == "-1" ? null : requset.userid;
                    }

                    if (requset.Project != null)
                    {
                        DB.Project = requset.Project;
                    }

                    if (requset.Status != null)
                    {
                        if (requset.Status == 24 || requset.Status == 25 || requset.Status == 26 || requset.Status == 27 || requset.Status == 28 || requset.Status == 30 || requset.Status == 37 || requset.Status == 38)
                        {
                            var kpi = "";
                            if (requset.KPI != null)
                            {
                                DB.KPI = requset.KPI;
                            }
                            else
                            {
                                if (requset.Status == 24)
                                {
                                    kpi = "0.5/1";
                                }
                                else if (requset.Status == 25)
                                {
                                    kpi = "0.5/2";
                                }
                                else if (requset.Status == 27)
                                {
                                    kpi = "1";
                                }
                                else if (requset.Status == 28)
                                {
                                    kpi = "1.5";
                                }
                                else if (requset.Status == 30)
                                {
                                    kpi = "2";
                                }
                                else if (requset.Status == 37)
                                {
                                    kpi = "2.5";
                                }
                                else if (requset.Status == 38)
                                {
                                    kpi = "3";
                                }
                                DB.KPI = kpi;
                            }
                            DB.Status = requset.Status;
                        }
                        else
                        {
                            DB.Status = requset.Status;
                            DB.KPI = "";
                        }
                    }

                    if (requset.Cause != null)
                    {
                        DB.Cause = requset.Cause;
                    }

                    if (requset.NoteStatus != null)
                    {
                        DB.NoteStatus = requset.NoteStatus;
                    }

                    if (requset.StatusStart != null)
                    {
                        DB.StatusStart = requset.StatusStart;
                    }

                    if (requset.StatusEnd != null)
                    {
                        DB.StatusEnd = requset.StatusEnd;
                    }

                    DB.WorkSalary = requset.WorkSalary;

                    DB.UpdatedDate = DateTime.Now;
                    DB.Datestatus = requset.datestatus;
                    _unitOfWork.CDDRepository.Update(DB);
                    await _unitOfWork.CompleteAsync();

                    if (requset.Status != null)
                    {
                        var Get = Status.FirstOrDefault(x => x.Id == DB.Status);
                        var Set = Status.FirstOrDefault(x => x.Id == requset.Status);
                        var CDDDetail = new Summary();
                        CDDDetail.CDDId = requset.Id;
                        CDDDetail.Detail = "คุณได้เปลื่ยนสถานะ" + (Get == null ? null : Get.Name) + " เป็น " + (Set == null ? null : Set.Name) + "เรียบร้อยแล้ว";
                        CDDDetail.UpdatedDate = DateTime.Now;
                        CDDDetail.CreatedDate = DateTime.Now;
                        await _unitOfWork.SummaryRepositoy.AddAsync(CDDDetail);
                        await _unitOfWork.CompleteAsync();

                        var notify = new Notification();
                        notify.Message = CDDDetail.Detail;
                        notify.CreatedDate = DateTime.Now;
                        notify.UpdatedDate = DateTime.Now;
                        notify.CDDId = requset.Id;
                        notify.Type = 2;
                        await _unitOfWork.CDDRepository.ChangeStatus(notify);

                        Calendar calendar = new Calendar();
                        calendar.Userid = DB.UserId;
                        calendar.Detail = "เก็บเงิน คุณ " + DB.NameTH + "ได้แล้ว";
                        calendar.StartDate = requset.datekpiSuccess;
                        calendar.EndDate = requset.datekpiSuccess;
                        calendar.CreatedDate = DateTime.Now;
                        calendar.UpdatedDate = DateTime.Now;
                        await _unitOfWork.CalendarRepository.AddAsync(calendar);
                        await _unitOfWork.CompleteAsync();

                    }
                    i = 1;
                }
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostCommentCDD(RequestDTO.CommentCDDRequset requset)
        {
            var i = 0;
            try
            {
                var DB = await _unitOfWork.CDDRepository.GetByIdAsync(requset.CDDId);
                if (DB == null)
                {
                    return BadRequest("ไม่มีข้อมูล");
                }
                else
                {
                    var GetCommentCDD = await _unitOfWork.CommentCDDRepository.GetAllAsync();

                    GetCommentCDD = GetCommentCDD.Where(x => x.CDDId == requset.CDDId).ToList();


                    if (GetCommentCDD.Count() == 0)
                    {
                        CommentCDD commentCDD = new CommentCDD();
                        commentCDD.CDDId = requset.CDDId;
                        commentCDD.Highlights = requset.Highlights;
                        commentCDD.Observations = requset.Observations;
                        commentCDD.Score = requset.Score;
                        commentCDD.CreatedDate = DateTime.Now;
                        commentCDD.UpdatedDate = DateTime.Now;

                        await _unitOfWork.CommentCDDRepository.AddAsync(commentCDD);
                    }
                    else
                    {
                        var First = GetCommentCDD.FirstOrDefault(x => x.CDDId == requset.CDDId);
                        First.Observations = requset.Observations;
                        First.Score = requset.Score;
                        First.UpdatedDate = DateTime.Now;
                        _unitOfWork.CommentCDDRepository.Update(First);
                    }

                    await _unitOfWork.CompleteAsync();
                    i = 1;
                }
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetCommentCDD(int? Id)
        {
            try
            {
                var DB = await _unitOfWork.CommentCDDRepository.GetAllAsync();
                if (DB.Count() == 0)
                {
                    return BadRequest("ไม่มีข้อมูล");
                }

                var Frist = DB.FirstOrDefault(x => x.CDDId == Id);

                return new JsonResult(new { db = Frist });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetUser(string? userId)
        {
            try
            {
                var user = await _unitOfWork.UsersRepository.GetAllAsync();
                var fuser = user.Where(x => x.DeleteAt != 1).FirstOrDefault(x => x.Id.ToLower() == userId.ToLower());
                return new JsonResult(new { db = fuser });
            }
            catch (Exception ex)
            {
                return BadRequest("error : " + ex.Message + "inner :" + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetRecruitment()
        {
            try
            {
                var user = await _unitOfWork.UsersRepository.GetRecruit();
                return new JsonResult(user);
            }
            catch (Exception ex)
            {
                return BadRequest("error : " + ex.Message + "inner :" + ex.InnerException);
            }
        }
    }
}
