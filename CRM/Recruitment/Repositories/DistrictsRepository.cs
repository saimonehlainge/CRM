using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IDistrictsRepository : IGenericRepository<Districts>
    { 
    
    }

    public class DistrictsRepository : GenericRepository<Districts>, IDistrictsRepository
    {
        public DistrictsRepository(RecruitmentContext context) : base(context)
        { 
        
        }
    }
}
