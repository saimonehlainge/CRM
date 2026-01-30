using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Models;

namespace Recruitment.Repositories
{
    public interface ISetKpiCommRepository : IGenericRepository<SetKpiComm>
    {
        Task<List<ResponseDTO.SetKpiComm>> GetSetKpi();
    }

    public class SetKpiCommRepository : GenericRepository<SetKpiComm>, ISetKpiCommRepository
    {
        public SetKpiCommRepository(RecruitmentContext context) : base(context)
        {

        }

        public async Task<List<ResponseDTO.SetKpiComm>> GetSetKpi()
        {
            try
            {
                var project = await _context.Project.ToListAsync();
                var SetKpiComm = await _context.SetKpiComm.ToListAsync();
                var DB = SetKpiComm.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.SetKpiComm
                {
                    SetKpi_Day = x.SetKpi_Day,
                    GuaranteeDay = x.GuaranteeDay,
                    Id = x.Id,
                    Kpi = x.Rank == 1 ? "ตาม Rank Salary" : x.Kpi.ToString(),
                    Comm = x.Rank == 1 ? "ตาม Rank Salary" : x.Comm.ToString(),
                    Project = x.Project == null ? null : project.FirstOrDefault(s => s.Id == x.Project).Name,
                    Status = x.Status

                }).ToList();

                return DB;
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
