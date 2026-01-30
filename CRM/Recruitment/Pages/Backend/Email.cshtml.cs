using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Asn1.Ocsp;
using Recruitment.Areas.Identity.Data;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable CS8604 // Possible null reference argument.
namespace Recruitment.Pages.Backend
{
    public class EmailModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetMail()
        { 
            var db = await _unitOfWork.EmailSettingRepository.GetAllAsync();
            return new JsonResult(db);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostMailSetting(RequestDTO.EmailSettingRequest request)
        {
            var i = 0;
            try
            {
                var EmailSetting = await _unitOfWork.EmailSettingRepository.GetByIdAsync(request.Id);
                if (EmailSetting is not null)
                { 
                    EmailSetting.Email = request.Email;
                    EmailSetting.UpdatedDate = DateTime.Now;
                }
                else
                {
                    EmailSetting Email = new EmailSetting();
                    Email.Email = request.Email;
                    Email.CreatedDate = DateTime.Now;
                    Email.UpdatedDate = DateTime.Now;
                    await _unitOfWork.EmailSettingRepository.AddAsync(Email);
                }
                await _unitOfWork.CompleteAsync();
                i = 1;
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
