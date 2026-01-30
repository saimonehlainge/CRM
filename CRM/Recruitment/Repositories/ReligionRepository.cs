using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IReligionRepository : IGenericRepository<Religion>
    { 
    
    }

    public class ReligionRepository : GenericRepository<Religion>, IReligionRepository
    {
        public ReligionRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
