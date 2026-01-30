using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Models;

namespace Recruitment.Repositories
{
    public interface IRecruitStatusRepository : IGenericRepository<RecruitStatus>
    {
        Task<List<ResponseDTO.RecruitStatusResponse>> StatusRecruit();
    }

    public class RecruitStatusRepository : GenericRepository<RecruitStatus>, IRecruitStatusRepository
    {
        public RecruitStatusRepository(RecruitmentContext context) : base(context) 
        { 
        
        }

        public async Task<List<ResponseDTO.RecruitStatusResponse>> StatusRecruit()
        {
            var db = await _context.RecruitStatus
                .Join(_context.RecruitStatusFrom,
                r => r.Id,
                rf => rf.recruitStatusId,
                (r, rf) => new { r, rf }
            ).ToListAsync();

            List<ResponseDTO.RecruitStatusResponse> aa = new List<ResponseDTO.RecruitStatusResponse>();   
            foreach (var item in db)
            {
                var f = db.FirstOrDefault(x => x.r.Id == item.r.Id);
                aa.Add(new ResponseDTO.RecruitStatusResponse
                {
                    Status = item.r.Status,
                    CreatedDate = item.r.CreatedDate,
                    DeleteAt = item.r.DeleteAt,
                    Id = item.r.Id,
                    Name = item.r.Name,
                    UpdatedDate = item.r.UpdatedDate,
                    typeform = item.rf.typeform
                });
            }

            return aa;
        }
    }
}
