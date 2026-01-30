using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Models;
using Recruitment.Repositories;
using System.Xml.Linq;

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
namespace Recruitment.Pages.Backend
{
    public class SetKpiCommModel : PageModel
    {
        public readonly IUnitOfWork _unitOfWork;

        public SetKpiCommModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTable()
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

                var GetDB = await _unitOfWork.SetKpiCommRepository.GetSetKpi();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "name":
                            GetDB = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? GetDB.OrderByDescending(x => x.Project).ToList() : GetDB.OrderBy(x => x.Project).ToList();
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    GetDB = GetDB.Where(x => x is { Project: not null } && (x.Project.Contains(searchValue))).ToList();
                }
                recordsTotal = GetDB.Count();
                GetDB = GetDB.Skip(skip).Take(pageSize).ToList();

                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = GetDB };
                return new JsonResult(jsonData);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetProject()
        {
            var DB = await _unitOfWork.ProjectRepository.GetAllAsync();
            DB = DB.Where(x => x.DeleteAt != 1).ToList();
            return new JsonResult(DB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAddData(RequestDTO.SetKpiComm request)
        {
            var i = 0;
            try
            {
                SetKpiComm setKpiComm = new SetKpiComm();
                setKpiComm.Project = request.Project;
                setKpiComm.SetKpi_Day = request.SetKpi_Day;
                setKpiComm.GuaranteeDay = request.GuaranteeDay;
                setKpiComm.Comm = request.Comm;
                setKpiComm.Kpi = request.Kpi;
                setKpiComm.Rank = request.Rank;
                setKpiComm.Status = request.Status;
                setKpiComm.CreatedDate = DateTime.Now;
                setKpiComm.UpdatedDate = DateTime.Now;
                await _unitOfWork.SetKpiCommRepository.AddAsync(setKpiComm);
                await _unitOfWork.CompleteAsync();
                i = 1;
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetSetKpiComm(int? Id)
        {
            var db = await _unitOfWork.SetKpiCommRepository.GetByIdAsync(Id!);
            return new JsonResult(db);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEditData(RequestDTO.SetKpiComm request)
        {
            var i = 0;
            try
            {
                var setKpiComm = await _unitOfWork.SetKpiCommRepository.GetByIdAsync(request.Id!);
                if (setKpiComm is not null)
                {
                    setKpiComm.Project = request.Project;
                    setKpiComm.SetKpi_Day = request.SetKpi_Day;
                    setKpiComm.GuaranteeDay = request.GuaranteeDay;
                    setKpiComm.Comm = request.Comm;
                    setKpiComm.Kpi = request.Kpi;
                    setKpiComm.Rank = request.Rank;
                    setKpiComm.Status = request.Status;
                    setKpiComm.UpdatedDate = DateTime.Now;
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
        public async Task<IActionResult> OnGetDeleteData(int? Id)
        {
            var i = 0;
            try
            {
                var setKpiComm = await _unitOfWork.SetKpiCommRepository.GetByIdAsync(Id!);
                if (setKpiComm is not null)
                {
                    setKpiComm.DeleteAt = 1;
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
    }
}
