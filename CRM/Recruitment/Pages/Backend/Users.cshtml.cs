using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Areas.Identity.GeneralProperty;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class UsersModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<RecruitmentUser> _userManager;

        public UsersModel(IUnitOfWork unitOfWork, UserManager<RecruitmentUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTable(string? name)
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

                var GetDB = await _unitOfWork.UsersRepository.GetUserResponse();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "firstname":
                            GetDB = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? GetDB.OrderByDescending(x => x.Firstname).ToList() : GetDB.OrderBy(x => x.Firstname).ToList();
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    GetDB = GetDB.Where(x => x is { Firstname: not null } && (x.Firstname.Contains(searchValue))).ToList();
                }

                if (name != null)
                {
                    GetDB = GetDB.Where(x => x.Firstname.Contains(name)).ToList();
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
        public async Task<IActionResult> OnGetUser(string? Id)
        {
            var db = await _unitOfWork.UsersRepository.GetAllAsync();

            //var yes = (int)Enum.Parse(IsActive.ใช้งาน.GetType(), IsActive.ใช้งาน.ToString());

            var user = db.Select(x=> new RequestDTO.UserRequests
            { 
              Status = x.IsActive,
              Address = x.Address,
              DepartmentId = x.DepartmentId,
              Firstname = x.Firstname,
              Id = x.Id,
              Lastname = x.Lastname,
              ProjectId = x.Project,
              RawPassword = x.RawPassword,
              RoleId = null,
              Targetdate = x.Targetdate == null ? null : x.Targetdate.Value.ToString("yyyy-MM-dd"),
              TargetKpi = x.TargetKpi,
              Todate = x.Todate == null ? null : x.Todate.Value.ToString("yyyy-MM-dd"),
              UserName = x.UserName,
              StatusTarget = x.StatusTarget
            }).FirstOrDefault(x => x.Id == Id);

            var bb = await _unitOfWork.UserRoleRepository.GetIndentityUserRole();
            var role = bb.FirstOrDefault(x => x.UserId == Id);

            return new JsonResult(new { user, role });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAddData(RequestDTO.UserRequest request)
        {
            var i = 0;
            try
            {
                RecruitmentUser User = new RecruitmentUser();
                var pwdhas = new PasswordHasher<RecruitmentUser>();
                User.CreatedDate = DateTime.Now;
                User.UpdatedDate = DateTime.Now;
                User.Firstname = request.Firstname;
                User.Lastname = request.Lastname;
                User.Address = request.Address;
                User.UserName = request.UserName;
                User.Email = request.UserName;
                User.NormalizedUserName = request.UserName.ToUpper();
                User.NormalizedEmail = request.UserName.ToLower();
                User.PasswordHash = pwdhas.HashPassword(User, request.RawPassword);
                //User.RawPassword = request.RawPassword;
                User.DepartmentId = request.DepartmentId;
                User.Project = request.ProjectId;

                User.TargetKpi = request.TargetKpi;
                User.Targetdate = request.Targetdate;
                User.Todate = request.Todate;
                User.StatusTarget = request.StatusTarget;

                if (request.RoleId != null && request.RoleId != "-1")
                { 
                    var result = await _userManager.CreateAsync(User, request.RawPassword);
                    if (result.Succeeded)
                    {
                        var GetRole = await _unitOfWork.RoleRepository.GetbyId(request.RoleId);
                        _ = await _userManager.AddToRoleAsync(User, GetRole.Name);
                        i = 1;
                    }
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
        public async Task<IActionResult> OnGetUsers(int? Id)
        {
            var db = await _unitOfWork.UsersRepository.GetByIdAsync(Id);
            return new JsonResult(db);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEditData(RequestDTO.UserRequest request)
        {
            var i = 0;
            try
            {
                var User = await _unitOfWork.UsersRepository.GetByIdAsync(request.Id);
                if (User is not null)
                {
                    var pwdhas = new PasswordHasher<RecruitmentUser>();
                    User.UpdatedDate = DateTime.Now;
                    User.Firstname = request.Firstname;
                    User.Lastname = request.Lastname;
                    User.Address = request.Address;
                    User.UserName = request.UserName;

                    User.NormalizedUserName = request.UserName.ToUpper();
                    User.NormalizedEmail = request.UserName.ToLower();
                    if (request.RawPassword != null && request.RawPassword != "")
                    {
                        //User.RawPassword = request.RawPassword;
                        User.PasswordHash = pwdhas.HashPassword(User, request.RawPassword);
                    }
                    User.DepartmentId = request.DepartmentId;
                    User.Project = request.ProjectId;

                    User.TargetKpi = request.TargetKpi;
                    User.Targetdate = request.Targetdate;
                    User.Todate = request.Todate;
                    User.StatusTarget = request.StatusTarget;

                    var result = await _userManager.UpdateAsync(User);
                    if (result.Succeeded)
                    {
                        var roleuser = await _unitOfWork.UsersRepository.FindRolueUser(User.Id);
                        if (roleuser != null)
                        {
                            await _userManager.RemoveFromRoleAsync(User, roleuser.Name);
                        }

                        var GetRole = await _unitOfWork.RoleRepository.GetbyId(request.RoleId);
                        _ = await _userManager.AddToRoleAsync(User, GetRole.Name);
                        i = 1;
                    }
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
        public async Task<IActionResult> OnGetDeleteData(string? Id)
        {
            var i = 0;
            try
            {
                var user = await _unitOfWork.UsersRepository.GetByIdAsync(Id);
                if (user is not null)
                {
                    user.DeleteAt = 1;
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
        public async Task<IActionResult> OnGetPermission()
        {
            var DB = await _unitOfWork.RoleRepository.GetAllAsync();
            return new JsonResult(DB);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetDepartment()
        {
            var DB = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return new JsonResult(DB);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetProject()
        {
            var DB = await _unitOfWork.ProjectRepository.GetAllAsync();
            return new JsonResult(DB);
        }
    }
}
