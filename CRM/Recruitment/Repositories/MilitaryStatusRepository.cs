using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IMilitaryStatusRepository : IGenericRepository<MilitaryStatus>
    { 
    
    }

    public class MilitaryStatusRepository : GenericRepository<MilitaryStatus>, IMilitaryStatusRepository
    {
        public MilitaryStatusRepository(RecruitmentContext context) : base(context) 
        {
        
        }
    }
}
