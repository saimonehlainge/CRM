using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class PermissionModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetListPermission()
        {
            var getrole = await _unitOfWork.RoleRepository.GetAllAsync();
            var permissiondetails = await _unitOfWork.PermissionDetailRepository.GetAllAsync();
            var user = User.GetUser();
            var permission = await _unitOfWork.PermissionSettingRepository.GetAllAsync();
            //permission = permission.Where(x => x.UserId == user.userid).ToList();

            return new JsonResult(new { getrole, permissiondetails, permission });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostPermissionSubmit(RequestDTO.PermissionRequset request)
        {
            try
            {
                var GetRole = await _unitOfWork.RoleRepository.GetAllAsync();
                var GetPermissionDetail = await _unitOfWork.PermissionDetailRepository.GetAllAsync();
                List<PermissionSetting> permissionsettings = new List<PermissionSetting>();
                int loopCreate = 0;
                int loopEdit = 0;
                int loopSetting = 0;
                int loopDelete = 0;
                int loopViewTeam = 0;
                int loopViewProject = 0;
                int loopCreateCalendar = 0;
                int loopViewCalendar = 0;
                int loopOnlyInfo = 0;
                int loopViewProjectManage = 0;
                int loopEditInfo = 0;
                int loopEditTeam = 0;
                int loopReportTeam = 0;
                int loopReportInfo = 0;
                int loopReportProject = 0;
                int loopReportTargetvsAchieved = 0;



                for (var i = 0; i < request?.Permission?.Count(); i++)
                {
                    var DB = GetRole.FirstOrDefault(x => x.Name == request?.Permission[i]);
                    permissionsettings.Add(new PermissionSetting
                    {
                        Permission = DB.Name,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    });

                    if (loopCreate < request?.Create?.Count())
                    {
                        var sp = request?.Create[loopCreate].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].create = true;
                            loopCreate++;
                        }
                    }

                    if (loopEdit < request?.Edit?.Count())
                    {
                        var sp = request?.Edit[loopEdit].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].edit = true;
                            loopEdit++;
                        }
                    }

                    if (loopSetting < request?.Setting?.Count())
                    {
                        var sp = request?.Setting[loopSetting].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].setting = true;
                            loopSetting++;
                        }
                    }

                    if (loopDelete < request?.Delete?.Count())
                    {
                        var sp = request?.Delete[loopDelete].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].delete = true;
                            loopDelete++;
                        }
                    }

                    if (loopViewTeam < request?.ViewTeam?.Count())
                    {
                        var sp = request?.ViewTeam[loopViewTeam].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].viewteam = true;
                            loopViewTeam++;
                        }
                    }

                    if (loopViewProject < request?.ViewProject?.Count())
                    {
                        var sp = request?.ViewProject[loopViewProject].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].viewproject = true;
                            loopViewProject++;
                        }
                    }

                    if (loopCreateCalendar < request?.CreateCalendar?.Count())
                    {
                        var sp = request?.CreateCalendar[loopCreateCalendar].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].createcalendar = true;
                            loopCreateCalendar++;
                        }
                    }

                    if (loopViewCalendar < request?.ViewCalendar?.Count())
                    {
                        var sp = request?.ViewCalendar[loopViewCalendar].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].viewcalendar = true;
                            loopViewCalendar++;
                        }
                    }

                    if (loopOnlyInfo < request?.OnlyInfo?.Count())
                    {
                        var sp = request?.OnlyInfo[loopOnlyInfo].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].onlyinfo = true;
                            loopOnlyInfo++;
                        }
                    }

                    if (loopViewProjectManage < request?.ViewProjectManage?.Count())
                    {
                        var sp = request?.ViewProjectManage[loopViewProjectManage].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].viewprojectmanage = true;
                            loopViewProjectManage++;
                        }
                    }

                    if (loopEditInfo < request?.EditInfo?.Count())
                    {
                        var sp = request?.EditInfo[loopEditInfo].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].editinfo = true;
                            loopEditInfo++;
                        }
                    }

                    if (loopEditTeam < request?.EditTeam?.Count())
                    {
                        var sp = request?.EditTeam[loopEditTeam].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].editteam = true;
                            loopEditTeam++;
                        }
                    }

                    if (loopReportTeam < request?.ReportTeam?.Count())
                    {
                        var sp = request?.ReportTeam[loopReportTeam].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].reportteam = true;
                            loopReportTeam++;
                        }

                    }

                    if (loopReportInfo < request?.ReportInfo?.Count())
                    {
                        var sp = request?.ReportInfo[loopReportInfo].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].reportinfo = true;
                            loopReportInfo++;
                        }
                    }

                    if (loopReportProject < request?.ReportProject?.Count())
                    {
                        var sp = request?.ReportProject[loopReportProject].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].reportproject = true;
                            loopReportProject++;
                        }

                    }

                    if (loopReportTargetvsAchieved < request?.ReportTargetvsAchieved?.Count())
                    {
                        var sp = request?.ReportTargetvsAchieved[loopReportTargetvsAchieved].Split(',');
                        var Ch = GetRole.FirstOrDefault(x => x.Name == sp[1] && x.Name == DB.Name);
                        if (Ch != null)
                        {
                            permissionsettings[i].reporttargetvsachieved = true;
                            loopReportTargetvsAchieved++;

                        }
                    }
                }

                await _unitOfWork.PermissionSettingRepository.Delete();

                await _unitOfWork.PermissionSettingRepository.AddRangeAsync(permissionsettings);
                await _unitOfWork.CompleteAsync();
                return new JsonResult(null);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
