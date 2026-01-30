using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    { 
    
    }

    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
