using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface INationalityRepository : IGenericRepository<Nationality>
    { 
    
    }

    public class NationalityRepository : GenericRepository<Nationality>, INationalityRepository
    {
        public NationalityRepository(RecruitmentContext context) : base(context) 
        {
        
        }
    }
}
