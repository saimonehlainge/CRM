using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IEmailSettingRepository : IGenericRepository<EmailSetting>
    { 
        
    }

    public class EmailSettingRepository : GenericRepository<EmailSetting>, IEmailSettingRepository
    {
        public EmailSettingRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
