using Recruitment.Data;
using Recruitment.Repositories;
using System.Security.Claims;

namespace Recruitment.Helpers
{
    public static class UserLoginActive
    {

        public static (string? userid, string? roles, string? allRole, string? department) GetUser(this ClaimsPrincipal cl)
        {
            if (cl.Identity.IsAuthenticated)
            {
                var userid = cl.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var roles = cl.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault();
                var allRole = string.Join(",", roles);
                var department = cl.Claims.FirstOrDefault(x => x.Type == "DepartmentId")?.Value;
                return (userid, roles, allRole, department);
            }
            else
            {
                return (null, null, null,null);
            }
        }
    }
}
