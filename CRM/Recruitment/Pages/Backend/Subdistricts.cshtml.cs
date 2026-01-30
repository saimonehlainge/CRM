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
    public class SubdistrictsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubdistrictsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int? Id)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTable(int? Id)
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

                var GetDB = await _unitOfWork.SubdistrictsRepository.GetAllAsync();

                GetDB = GetDB.Where(x => x.Status == 1 && x.DistrictId == Id).ToList();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "name":
                            GetDB = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? GetDB.OrderByDescending(x => x.NameInThai).ToList() : GetDB.OrderBy(x => x.NameInThai).ToList();
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    GetDB = GetDB.Where(x => x is { NameInThai: not null } && (x.NameInThai.Contains(searchValue))).ToList();
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
        public async Task<IActionResult> OnPostAddData(RequestDTO.SubDistrictsRequest request)
        {
            var i = 0;
            try
            {
                Subdistricts subdistricts = new Subdistricts();
                subdistricts.Code = request.Code;
                subdistricts.NameInThai = request.NameInThai;
                subdistricts.NameInEnglish = request.NameInEnglish;
                subdistricts.Status = request.Status;
                subdistricts.Latitude = request.Latitude;
                subdistricts.Longitude = request.Longitude;
                subdistricts.DistrictId = request.DistrictId;
                subdistricts.ZipCode = request.ZipCode;
                subdistricts.CreatedDate = DateTime.Now;
                subdistricts.UpdatedDate = DateTime.Now;
                await _unitOfWork.SubdistrictsRepository.AddAsync(subdistricts);
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
        public async Task<IActionResult> OnGetDistrict(int? Id)
        {
            var db = await _unitOfWork.DistrictRepository.GetByIdAsync(Id);
            return new JsonResult(db);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEditData(RequestDTO.SubDistrictsRequest request)
        {
            var i = 0;
            try
            {
                var subdistricts = await _unitOfWork.SubdistrictsRepository.GetByIdAsync(request.Id);
                if (subdistricts is not null)
                {
                    subdistricts.Code = request.Code;
                    subdistricts.NameInThai = request.NameInThai;
                    subdistricts.NameInEnglish = request.NameInEnglish;
                    subdistricts.Status = request.Status;
                    subdistricts.Latitude = request.Latitude;
                    subdistricts.Longitude = request.Longitude;
                    subdistricts.DistrictId = request.DistrictId;
                    subdistricts.ZipCode = request.ZipCode;
                    subdistricts.UpdatedDate = DateTime.Now;
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
                var districts = await _unitOfWork.SubdistrictsRepository.GetByIdAsync(Id);
                if (districts is not null)
                {
                    districts.DeleteAt = 1;
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
