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
    public class MilitaryStatusModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public MilitaryStatusModel(IUnitOfWork unitOfWork)
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

                var GetDB = await _unitOfWork.MilitaryStatusRepository.GetAllAsync();

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
        public async Task<IActionResult> OnPostAddData(RequestDTO.MilitaryStatusRequest request)
        {
            var i = 0;
            try
            {
                MilitaryStatus militaryStatus = new MilitaryStatus();
                militaryStatus.Name = request.Name;
                militaryStatus.Status = request.Status;
                militaryStatus.CreatedDate = DateTime.Now;
                militaryStatus.UpdatedDate = DateTime.Now;
                await _unitOfWork.MilitaryStatusRepository.AddAsync(militaryStatus);
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
        public async Task<IActionResult> OnGetMilitaryStatus(int? Id)
        {
            var db = await _unitOfWork.MilitaryStatusRepository.GetByIdAsync(Id);
            return new JsonResult(db);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEditData(RequestDTO.MilitaryStatusRequest request)
        {
            var i = 0;
            try
            {
                var militaryStatus = await _unitOfWork.MilitaryStatusRepository.GetByIdAsync(request.Id);
                if (militaryStatus is not null)
                {
                    militaryStatus.Name = request.Name;
                    militaryStatus.Status = request.Status;
                    militaryStatus.UpdatedDate = DateTime.Now;
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

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetDeleteData(int? Id)
        {
            var i = 0;
            try
            {
                var militaryStatus = await _unitOfWork.MilitaryStatusRepository.GetByIdAsync(Id);
                if (militaryStatus is not null)
                {
                    militaryStatus.DeleteAt = 1;
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
