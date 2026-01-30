using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetbyId(string? Id);
    }

    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(RecruitmentContext context) : base(context)
        {

        }

        public async Task<Role> GetbyId(string? Id)
        {
            try
            {
                var DB = await _context.Roles.FirstOrDefaultAsync(x => x.Id == Id);
                return DB!;
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
