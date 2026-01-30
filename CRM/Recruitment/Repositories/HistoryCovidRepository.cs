using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IHistoryCovidRepository : IGenericRepository<HistoryCovid>
    { 
    
    }

    public class HistoryCovidRepository : GenericRepository<HistoryCovid>, IHistoryCovidRepository
    {
        public HistoryCovidRepository(RecruitmentContext context) : base(context)
        { 
        
        }
    }
}
