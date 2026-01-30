using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Models;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace Recruitment.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
       
    }

    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(RecruitmentContext context) : base(context) 
        { 
        
        }
    }
}
