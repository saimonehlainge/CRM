using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace Recruitment.Pages.Backend
{
    public class TeamUserModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamUserModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int? Id)
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTableTeam()
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

                var GetDB = await _unitOfWork.PermissionRepository.GetAllAsync();

                var user = await _unitOfWork.UsersRepository.GetAllAsync();
                var department = await _unitOfWork.DepartmentRepository.GetAllAsync();

                var WhereDB = GetDB.Where(x => x.DeleteAt != 1).Select(x=> new ResponseDTO.datateam
                { 
                    Id = x.Id,
                    name = (x.BossId != null ? user.FirstOrDefault(u => u.Id == x.BossId).Firstname : user.FirstOrDefault(u=>u.Id == x.UserId).Firstname),   //user.FirstOrDefault(u=>u.Id == x.userId).Firstname
                    department = (x.DepartmentId == null ? null : department.FirstOrDefault(d=>d.Id == x.DepartmentId).Name)
                }).ToList();
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "name":
                            WhereDB = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? WhereDB.OrderByDescending(x => x.name).ToList() : WhereDB.OrderBy(x => x.name).ToList();
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    WhereDB = WhereDB.Where(x => x is { name: not null } && (x.name.Contains(searchValue))).ToList();
                }

                recordsTotal = WhereDB.Count();
                WhereDB = WhereDB.Skip(skip).Take(pageSize).ToList();

                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = WhereDB };
                return new JsonResult(jsonData);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetUser()
        {
            var DB = await _unitOfWork.UsersRepository.GetAllAsync();
            return new JsonResult(DB);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetDepartment()
        {
            var DB = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return new JsonResult(DB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostUserTeamSubmit(RequestDTO.UserTeamRequset request)
        {
            var i = 0;
            try
            {
                Permission permission = new Permission();
                if (request.teamposition == 1)
                {
                    permission.BossId = request.userId;
                }
                else
                {
                    permission.UserId = request.userId;
                }
                permission.DepartmentId = request.departmentId;
                permission.TeamId = request.teamId;
                permission.CreatedDate = DateTime.Now;
                permission.UpdatedDate = DateTime.Now;

                await _unitOfWork.PermissionRepository.AddAsync(permission);
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
        public async Task<IActionResult> OnGetDeleteData(int? Id)
        {
            var i = 0;
            try
            {
                var Permission = await _unitOfWork.PermissionRepository.GetByIdAsync(Id)!;
                if (Permission is not null)
                {
                    await _unitOfWork.PermissionRepository.RemoveAsync(Permission);
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
