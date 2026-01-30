using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

#pragma warning disable CS8603 // Possible null reference return.
namespace Recruitment.Repositories
{
    public interface IPermissionSettingRepository : IGenericRepository<PermissionSetting>
    {
        Task<List<PermissionSetting>> GetPermission(string? userid);
        Task<IActionResult> Delete();
    }

    public class PermissionSettingRepository : GenericRepository<PermissionSetting>, IPermissionSettingRepository
    {
        public PermissionSettingRepository(RecruitmentContext context) : base(context)
        {

        }

        public async Task<List<PermissionSetting>> GetPermission(string? userid)
        {
            try
            {

                if (userid != null)
                {
                    var getrole = await _context.UserRoles
                        .Join(_context.Roles,
                        ur => ur.RoleId,
                        r => r.Id,
                        (ur, r) => new { ur, r }).Where(u => u.ur.UserId != null && u.ur.UserId == userid).ToListAsync();

                    if (getrole.Count() != 0)
                    {

                        List<PermissionSetting> DB = await _context.PermissionSetting.Where(x => x.Permission == getrole.FirstOrDefault().r.Name).ToListAsync();
                        return DB;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<IActionResult> Delete()
        {
            var DB = await _context.PermissionSetting.ToListAsync();
            _context.PermissionSetting.RemoveRange(DB);
            await _context.SaveChangesAsync();
            return new JsonResult(null);
        }
    }
}
