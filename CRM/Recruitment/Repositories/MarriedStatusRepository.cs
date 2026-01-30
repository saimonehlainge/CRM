using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IMarriedStatusRepository : IGenericRepository<MarriedStatus>
    { 
    
    }

    public class MarriedStatusRepository : GenericRepository<MarriedStatus>, IMarriedStatusRepository
    {
        public MarriedStatusRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
