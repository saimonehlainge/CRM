using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable CS8604 // Possible null reference argument.
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class WorkOtherProvincesModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkOtherProvincesModel(IUnitOfWork unitOfWork)
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

                var GetDB = await _unitOfWork.WorkOtherProvincesRepository.GetAllAsync();

                GetDB = GetDB.Where(x => x.DeleteAt != 1).ToList();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "name":
                            GetDB = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? GetDB.OrderByDescending(x => x.Name).ToList() : GetDB.OrderBy(x => x.Name).ToList();
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    GetDB = GetDB.Where(x => x is { Name: not null } && (x.Name.Contains(searchValue))).ToList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAddData(RequestDTO.WorkOtherProvincesRequest request)
        {
            var i = 0;
            try
            {
                WorkOtherProvinces workOtherProvinces = new WorkOtherProvinces();
                workOtherProvinces.Name = request.Name;
                workOtherProvinces.Status = request.Status;
                workOtherProvinces.CreatedDate = DateTime.Now;
                workOtherProvinces.UpdatedDate = DateTime.Now;
                await _unitOfWork.WorkOtherProvincesRepository.AddAsync(workOtherProvinces);
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
        public async Task<IActionResult> OnGetWorkOtherProvin(int? Id)
        {
            var db = await _unitOfWork.WorkOtherProvincesRepository.GetByIdAsync(Id);
            return new JsonResult(db);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEditData(RequestDTO.WorkOtherProvincesRequest request)
        {
            var i = 0;
            try
            {
                var workOtherProvinces = await _unitOfWork.WorkOtherProvincesRepository.GetByIdAsync(request.Id);
                if (workOtherProvinces is not null)
                {
                    workOtherProvinces.Name = request.Name;
                    workOtherProvinces.Status = request.Status;
                    workOtherProvinces.UpdatedDate = DateTime.Now;
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
                var workOtherProvinces = await _unitOfWork.WorkOtherProvincesRepository.GetByIdAsync(Id);
                if (workOtherProvinces is not null)
                {
                    workOtherProvinces.DeleteAt = 1;
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
