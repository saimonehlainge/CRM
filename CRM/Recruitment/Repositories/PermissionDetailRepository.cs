using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IPermissionDetailRepository : IGenericRepository<PermissionDetail>
    { 
    
    }

    public class PermissionDetailRepository : GenericRepository<PermissionDetail>, IPermissionDetailRepository
    {
        public PermissionDetailRepository(RecruitmentContext context) : base(context) 
        {
        
        }
    }
}
