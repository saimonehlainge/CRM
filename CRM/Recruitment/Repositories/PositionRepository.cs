using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Models;

namespace Recruitment.Repositories
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
        Task RemoveRangeAsync(IEnumerable<Position> getPosition);
        Task<List<ResponseDTO.PositionResponse>> CoustomPosition(string? department);
    }

    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(RecruitmentContext context) : base(context)
        { 
        
        }

        public async Task RemoveRangeAsync(IEnumerable<Position> getPosition)
        {
            try
            {
                _context.Position.RemoveRange(getPosition);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<List<ResponseDTO.PositionResponse>> CoustomPosition(string? department)
        {
            var db = await _context.Position.Where(x=>x.DeleteAt != 1).ToListAsync();
          
            if (department == "3")
            {
                db = db.Where(x => x.TypeForm == 3 || x.TypeForm == 4).ToList();
            }
            else
            {
                db = db.Where(x => x.TypeForm == 1 || x.TypeForm == 2).ToList();
            }

            var group = db.GroupBy(x => x.Name).ToList();

            List<ResponseDTO.PositionResponse> aa = new List<ResponseDTO.PositionResponse>();
            foreach (var item in group)
            {
                var f = db.FirstOrDefault(x => x.Name == item.Key);
                aa.Add(new ResponseDTO.PositionResponse
                { 
                   Status = f.Status,
                   Id = f.Id,
                   Name = f.Name,
                   TypeForm = f.TypeForm == null ? null : f.TypeForm.ToString(),
                   DeleteAt = f.DeleteAt
                });
            }

            return aa;
        }
    }
}
