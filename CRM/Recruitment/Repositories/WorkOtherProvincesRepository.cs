using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IWorkOtherProvincesRepository : IGenericRepository<WorkOtherProvinces>
    {

    }

    public class WorkOtherProvincesRepository : GenericRepository<WorkOtherProvinces>, IWorkOtherProvincesRepository
    {
        public WorkOtherProvincesRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
