using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using System.ComponentModel;


#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
namespace Recruitment.Repositories
{
    public interface IProvinceRepository : IGenericRepository<Provinces>
    {
        Task<List<Provinces>> GetAllProvinces();
    }

    public class ProvinceRepository : GenericRepository<Provinces>, IProvinceRepository
    {
        public ProvinceRepository(RecruitmentContext context) : base(context)
        { 
            
        }

        public async Task<List<Provinces>> GetAllProvinces()
        {
            try
            {
                var DB = await _context.Provinces.Where(x=>x.Status == 1).ToListAsync();
                return DB;
                //return await (from Provinces in _context.Provinces where Provinces.Status == 1
                //        select Provinces)
                //    .Include(d => d.Districts)
                //    .ThenInclude(s => s.Subdistricts).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }         
        }
    }
}
