using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Recruitment.Areas.Identity.Data;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;
using System.Globalization;

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
namespace Recruitment.Pages.Backend
{
    public class FlowSummaryModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlowSummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTable(string? firstname, string? lastname, int? project, int? status, string? startdate, string? enddate, string? month, string? year)
        {
            try
            {
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
                var (userid, roles, _, department) = User.GetUser();
                if (roles != "project owner")
                { 
                    var per = await _unitOfWork.PermissionSettingRepository.GetAllAsync();
                    var Permission = per.Where(x => x.Permission == roles && x.viewproject == true).ToList();
                    if (Permission.Count() == 0)
                    {
                        Permission = per.Where(x => x.Permission == roles && x.viewprojectmanage == true).ToList();
                        if (Permission.Count() == 0)
                        {
                            GetDB = new List<CDD>(); ///null;
                        }
                        else
                        {
                            GetDB = GetDB.Where(x => x.UserId == userid && x.DeleteAt != 1).ToList();
                        }
                    }
                }
                else if (roles == "project owner")
                {
                    var DBuser = await _unitOfWork.UsersRepository.GetAllAsync();
                    var aa = DBuser.FirstOrDefault(x => x.Id == userid);
                    GetDB = GetDB.Where(x => x.Project == aa.Project).ToList();
                }

                if (firstname != null && firstname != "")
                {
                    GetDB = GetDB.Where(x =>
                    x.NameTH.Contains(firstname) ||
                    x.NameENG.Contains(firstname)
                    ).ToList();
                }

                if (lastname != null && lastname != "")
                {
                    GetDB = GetDB.Where(x =>
                        x.NameTH.Contains(lastname) ||
                        x.NameENG.Contains(lastname)
                        ).ToList();
                }

                if (project != null && project != -1)
                {
                    GetDB = GetDB.Where(x => x.Project == project).ToList();
                }

                if (status != null && status != -1)
                {
                    GetDB = GetDB.Where(x => x.Status == status).ToList();
                }

                if (startdate != null && startdate != "" && enddate != null && enddate != "")
                {
                    var S = Convert.ToDateTime(startdate, new CultureInfo("en-US"));
                    var E = Convert.ToDateTime(enddate, new CultureInfo("en-US"));
                    GetDB = GetDB.Where(x => x.CreatedDate != null && x.CreatedDate.Value.Date >= S.Date && x.CreatedDate.Value.Date <= E.Date).ToList();
                }

                if (month != null && month != "-1")
                {
                    GetDB = GetDB.Where(x => x.BillingCycle.Contains(month)).ToList();
                }

                if (year != null && year != "")
                {
                    GetDB = GetDB.Where(x => x.BillingCycle.Contains(year)).ToList();
                }

                var StatsuRecruit = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
                var GetProject = await _unitOfWork.ProjectRepository.GetAllAsync();
                var NewDB = GetDB.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.SummaryResponse
                {
                    id = x.Id,
                    name = x.NameTH,
                    nickname = x.Nikname,
                    tel = x.Tel,
                    project = GetProject.FirstOrDefault(a => a.Id == x.Project) == null ? null : GetProject.FirstOrDefault(a => a.Id == x.Project).Name,
                    kpi = x.KPI,
                    status = StatsuRecruit.FirstOrDefault(a => a.Id == x.Status) == null ? null : StatsuRecruit.FirstOrDefault(a => a.Id == x.Status).Name,
                    Date = x.CreatedDate
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
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        public async Task<IActionResult> OnGetReport_FlowSummary(string? firstname, string? lastname, int? project, int? status, string? start, string to, string? month, string? year)
        {
            var FileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Export/" + DateTime.Now.Ticks + ".xlsx");
            Stream stream = new MemoryStream();
            using (var package = new ExcelPackage(new FileInfo(FileName)))
            {
                ExcelWorksheet Sheet = package.Workbook.Worksheets.Add("Performance Report - RC");

                #region ตาราง
                Sheet.Cells["A1"].Value = "#";
                Sheet.Cells["B1"].Value = "CDD Name";
                Sheet.Cells["C1"].Value = "NickName";
                Sheet.Cells["D1"].Value = "Tel";
                Sheet.Cells["E1"].Value = "Project";
                Sheet.Cells["F1"].Value = "Kpi";
                Sheet.Cells["G1"].Value = "Status";
                #endregion

                var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();
                var NewDB = GetDB.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.ReportResponseExport
                {
                    id = x.Id,
                    userId = x.UserId,
                    name = x.NameTH,
                    nickname = x.Nikname,
                    tel = x.Tel,
                    project = x.Project,
                    kpi = x.KPI,
                    status = x.Status,
                    Date = x.CreatedDate,
                    startdate = x.StatusStart,
                    enddate = x.StatusEnd
                }).ToList();

                if (firstname != null)
                {
                    NewDB = NewDB.Where(x => x.name.Contains(firstname)).ToList();
                }

                if (lastname != null)
                {
                    NewDB = NewDB.Where(x => x.name.Contains(lastname)).ToList();
                }

                if (project != null)
                {
                    NewDB = NewDB.Where(x => x.project == project).ToList();
                }

                if (status != null)
                {
                    NewDB = NewDB.Where(x => x.status == status).ToList();
                }

                if (start != null && start != "")
                {
                    var CDate = Convert.ToDateTime(start);
                    NewDB = NewDB.Where(x => x.startdate!.Value.Date == CDate.Date).ToList();
                }

                if (to != null && to != "")
                {
                    var CDate = Convert.ToDateTime(to);
                    NewDB = NewDB.Where(x => x.enddate!.Value.Date == CDate.Date).ToList();
                }

                if (month != null)
                {
                    NewDB = NewDB.Where(x => x.startdate != null && x.startdate!.Value.Month == Convert.ToInt32(month)).ToList();
                }

                if (year != null)
                {
                    NewDB = NewDB.Where(x => x.startdate != null && x.startdate!.Value.Year == Convert.ToInt32(year)).ToList();
                }

                var i = 1;
                var index = 2;
                var DBproject = await _unitOfWork.ProjectRepository.GetAllAsync();
                var RecruitStatus = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
                foreach (var item in NewDB)
                {
                    Sheet.Cells[string.Format("A{0}", index)].Value = i;
                    Sheet.Cells[string.Format("B{0}", index)].Value = item.name == null ? null : item.name;
                    Sheet.Cells[string.Format("C{0}", index)].Value = item.nickname == null ? null : item.nickname;
                    Sheet.Cells[string.Format("D{0}", index)].Value = item.tel == null ? null : item.tel;
                    Sheet.Cells[string.Format("E{0}", index)].Value = DBproject.FirstOrDefault(x => x.Id == item.project) == null ? null : DBproject.FirstOrDefault(x => x.Id == item.project).Name;
                    Sheet.Cells[string.Format("F{0}", index)].Value = item.kpi == null ? null : item.kpi;
                    Sheet.Cells[string.Format("G{0}", index)].Value = item.status == null ? null : RecruitStatus.FirstOrDefault(x => x.Id == item.status).Name;
                    i++;
                    index++;
                }

                Sheet.Cells["A:G"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                Sheet.Cells["A:G"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                Sheet.Cells["A:G"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A:G"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A:G"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A:G"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                Sheet.Cells["A:G"].AutoFitColumns();
                package.SaveAs(stream);
                stream.Position = 0;
            }
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CertificationsReport-" + FileName + "");
        }
    }
}
