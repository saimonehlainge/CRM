using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IAnnouncementRepository : IGenericRepository<Announcement>
    { 
    
    }

    public class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(RecruitmentContext context) : base(context)
        { 
        
        }
    }
}
