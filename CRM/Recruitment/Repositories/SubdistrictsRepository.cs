using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface ISubdistrictsRepository : IGenericRepository<Subdistricts>
    { 
    
    }

    public class SubdistrictsRepository : GenericRepository<Subdistricts>, ISubdistrictsRepository
    {
        public SubdistrictsRepository(RecruitmentContext context) : base(context)
        { 
        
        }
    }
}
