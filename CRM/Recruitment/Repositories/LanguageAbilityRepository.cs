using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface ILanguageAbilityRepository : IGenericRepository<LanguageAbility>
    { 
    
    }

    public class LanguageAbilityRepository : GenericRepository<LanguageAbility>, ILanguageAbilityRepository
    {
        public LanguageAbilityRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
