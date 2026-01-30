using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IPrefixRepositoy : IGenericRepository<Prefix>
    { 
    
    }

    public class PrefixRepositoy : GenericRepository<Prefix>, IPrefixRepositoy
    {
        public PrefixRepositoy(RecruitmentContext context) : base(context)
        { 
        
        }
    }
}
