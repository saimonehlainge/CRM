using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Models;

namespace Recruitment.Repositories
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {

    }

    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(RecruitmentContext context) : base(context)
        { 
        
        }
    }
}
