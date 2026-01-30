using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    { 
    
    }

    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
