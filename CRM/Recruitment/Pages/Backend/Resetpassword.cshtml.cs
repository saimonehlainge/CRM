using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Repositories;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
namespace Recruitment.Pages.Backend
{
    public class ResetpasswordModel : PageModel
    {
        IUnitOfWork _unitOfWork;

        public ResetpasswordModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(string? Id)
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostResetPassword(string? Id, string? token, string? confrim_pass)
        {
            var result = await _unitOfWork.UsersRepository.ResetPassword(Id, token, confrim_pass);
            return new JsonResult(1);
        }
    }
}
