using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class NotifyModel : PageModel
    {
        private readonly RecruitmentContext _context;

        public NotifyModel(RecruitmentContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetNotify()
        {
            try
            {
                var (userid, rolues, _, department) = User.GetUser();
                //var DB = await _context.Notification
                //    .Join(_context.CDD,
                //    n => n.CDDId,
                //    c => c.Id,
                //    (n, c) => new { n, c }).OrderByDescending(o => o.n.CreatedDate).Select(x=> new ResponseDTO.NotiResponse
                //    { 
                //        CDDId = x.n.CDDId,
                //        Id = x.n.Id,
                //        Message = x.n.Message,
                //        CreatedDate = x.n.CreatedDate == null ? null : x.n.CreatedDate.Value.ToString("dd/MM/yyyy") + " เวลา " + x.n.CreatedDate.Value.ToString("HH:mm:ss"),
                //        View = x.n.View,
                //        UserId = x.c.UserId
                //    }).ToListAsync();

                var DB = await _context.Notification.OrderByDescending(x=>x.CreatedDate).ToListAsync();

                if (rolues == "recruiter")
                {
                    DB = DB.Where(x => x.userid == userid || x.userid == null).ToList();
                }

                return new JsonResult(DB);
            }
            catch (Exception ex)
            {
                throw new Exception(ex?.InnerException?.ToString() ?? "error " + ex?.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostChangeViewNotify(int? id)
        {
            try
            {
                var DB = await _context.Notification.FirstOrDefaultAsync(x => x.Id == id);
                if (DB != null)
                {
                    DB.View = 1;
                    _context.Entry(DB).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

                return new JsonResult(DB);

                //return new JsonResult("success");
            }
            catch (Exception ex)
            {
                throw new Exception(ex?.InnerException?.ToString() ?? "error " + ex?.Message);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetDeleteNotify()
        {
            int? i = 0;
            try
            {
                var DB = await _context.Notification.ToListAsync();
                _context.Notification.RemoveRange(DB);
                await _context.SaveChangesAsync();
                i = 1;

                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                throw new Exception(ex?.InnerException?.ToString() ?? "error " + ex?.Message);
            }
        }
    }
}
