using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Recruitment.Data;
using Recruitment.Models;

namespace Recruitment.Repositories
{
    public interface IUserRoleRepository : IGenericRepository<IdentityRole>
    {
        Task<List<ResponseDTO.IdentityUserRoleResponse>> GetIndentityUserRole();
        Task<List<ResponseDTO.GroupUserResponse>> GetUserRole();
    }

    public class UserRoleRepository : GenericRepository<IdentityRole>, IUserRoleRepository
    {
        public UserRoleRepository(RecruitmentContext context) : base(context)
        { 
            
        }

        public async Task<List<ResponseDTO.IdentityUserRoleResponse>> GetIndentityUserRole()
        {
            try
            {
                var db = await _context.UserRoles.Select(x=> new ResponseDTO.IdentityUserRoleResponse
                { 
                    RoleId = x.RoleId,
                    UserId = x.UserId
                }).ToListAsync();
                return db; 
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<List<ResponseDTO.GroupUserResponse>> GetUserRole()
        {
            try
            {
                var db = await _context.UserRoles.Join(_context.Users,
                    ur => ur.UserId,
                    u => u.Id,
                    (ur, u) => new { ur, u })
                    .Join(_context.Roles,
                    j => j.ur.RoleId,
                    r => r.Id,
                    (j, r) => new ResponseDTO.GroupUserResponse
                    {
                        Address = j.u.Address,
                        CreatedDate = j.u.CreatedDate,
                        DeleteAt = j.u.DeleteAt,
                        DepartmentId = j.u.DepartmentId,
                        Firstname = j.u.Firstname,
                        IsActive = j.u.IsActive,
                        Lastname = j.u.Lastname,
                        Project = j.u.Project,
                        RawPassword = j.u.RawPassword,
                        UpdatedDate = j.u.UpdatedDate,
                        RoleId = r.Id,
                        UserId = j.u.Id
                    }).ToListAsync();
                return db;
            }
            catch (Exception ex)
            {
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
