using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IQualificationRepository : IGenericRepository<Qualification>
    { 
    
    }

    public class QualificationRepository : GenericRepository<Qualification>, IQualificationRepository
    {
        public QualificationRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
