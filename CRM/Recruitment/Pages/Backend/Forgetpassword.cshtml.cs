using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Repositories;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
namespace Recruitment.Pages.Backend
{
    public class ForgetpasswordModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ForgetpasswordModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostResetPassword(string? Email)
        {
            await _unitOfWork.UsersRepository.SendMailResetPassword(Email);
            return new JsonResult(0);
        }
    }
}
