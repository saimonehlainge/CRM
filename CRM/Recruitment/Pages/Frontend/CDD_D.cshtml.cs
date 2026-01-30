using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
namespace Recruitment.Pages.Frontend
{
    public class CDD_DModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerHelperRepository _logger;

        public CDD_DModel(IUnitOfWork unitOfWork, ILoggerHelperRepository logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostFormD(RequestDTO.DTORequest request)
        {
            try
            { 
                if (request.userId == null)
                {
                    var user = User.GetUser();
                    request.userId = user.userid;
                }
                request.type_cdd = 4;

                await _unitOfWork.CDDRepository.InsertCDD(request);

                _logger.LogMessage("บันทึกข้อมูลเรียบร้อย " + DateTime.Now);
                return new JsonResult(new { status = "success", messageArray = "success" }); ;
            }
            catch (Exception ex)
            {
                _logger.LogMessage("เกิดข้อผิดพลาด " + "error : " + ex + " inner : " + ex.InnerException);
                throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetAnnouncement()
        {
            var db = await _unitOfWork.AnnouncementRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetPosition()
        {
            var db = await _unitOfWork.PositionRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.TypeForm == 4 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetProvince()
        {
            var db = await _unitOfWork.ProvincesRepository.GetAllProvinces();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetPrefix()
        {
            var db = await _unitOfWork.PrefixRepositoy.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetReligion()
        {
            var db = await _unitOfWork.ReligionRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetNationality()
        {
            var db = await _unitOfWork.NationalityRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetMilitaryStatus()
        {
            var db = await _unitOfWork.MilitaryStatusRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetMarriedStatus()
        {
            var db = await _unitOfWork.MarriedStatusRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetQualification()
        {
            var db = await _unitOfWork.QualificationRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetLanguageAbility()
        {
            var db = await _unitOfWork.LanguageAbilityRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetComapny()
        {
            var db = await _unitOfWork.CompanyRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetHistoryCovid()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.HistoryCovidRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }
    }
}
