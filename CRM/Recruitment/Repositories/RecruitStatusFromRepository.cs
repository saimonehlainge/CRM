using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface IRecruitStatusFromRepository : IGenericRepository<RecruitStatusFrom>
    {
        Task RemoveAll(int? id);
    }

    public class RecruitStatusFromRepository : GenericRepository<RecruitStatusFrom>, IRecruitStatusFromRepository
    {
        public RecruitStatusFromRepository(RecruitmentContext context) : base(context)
        { 
        
        }

        public async Task RemoveAll(int? id)
        {
            var get = await _context.RecruitStatusFrom
                            .Where(x => x.recruitStatusId == id)
                            .ToListAsync();

            if (get.Any())
            {
                _context.RecruitStatusFrom.RemoveRange(get);
                await _context.SaveChangesAsync(); // อย่าลืมบันทึก
            }
        }
    }
}
