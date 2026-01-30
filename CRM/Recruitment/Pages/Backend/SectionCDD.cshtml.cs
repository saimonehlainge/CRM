using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Helpers;
using Recruitment.Repositories;

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace Recruitment.Pages.Backend
{
    public class SectionCDDModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public SectionCDDModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
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

                var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();
                var (userid, roles, _, department) = User.GetUser();
                var per = await _unitOfWork.PermissionSettingRepository.GetAllAsync();
                var Permission = per.Where(x => x.Permission == roles && x.viewproject == true).ToList();
                if (roles == "project owner")
                {
                    var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                    var GetProject = await _unitOfWork.ProjectRepository.GetAllAsync();
                    var roles_projectowner = getuser.FirstOrDefault(x => x.Id == userid);
                    var f_GetProject = GetProject.FirstOrDefault(x => x.Id.ToString() == (roles_projectowner.Project == null ? null : roles_projectowner.Project.ToString()));
                    if (f_GetProject != null)
                    {
                        //NewDB = NewDB.Where(x => x.project == f_GetProject.Name && f_GetProject.Name != null).ToList();
                        GetDB = GetDB.Where(x => x.Project == f_GetProject.Id).ToList();
                    }
                }
                else if (roles != "project owner")
                {
                    if (Permission.Count() == 0)
                    {
                        Permission = per.Where(x => x.Permission == roles && x.viewprojectmanage == true).ToList();
                        if (Permission.Count() == 0)
                        {
                            GetDB = new List<CDD>();
                        }
                        else
                        {
                            GetDB = GetDB.Where(x => x.UserId == userid).ToList();
                        }
                    }
                }

                GetDB = GetDB!.Where(x => x.DeleteAt != 1).ToList();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "name":
                            GetDB = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? GetDB!.OrderByDescending(x => x.Id).ToList() : GetDB!.OrderBy(x => x.Id).ToList();
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    GetDB = GetDB!.Where(x => x is { NameTH: not null } && (x.NameTH.Contains(searchValue))).ToList();
                }

                recordsTotal = GetDB!.Count();
                GetDB = GetDB!.Skip(skip).Take(pageSize).ToList();

                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = GetDB };
                return new JsonResult(jsonData);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
