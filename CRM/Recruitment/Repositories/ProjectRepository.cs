using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    { 
    
    }

    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
