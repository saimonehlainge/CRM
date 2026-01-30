using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
namespace Recruitment.Pages.Frontend
{
    public class CDD_AModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerHelperRepository _logger;

        public CDD_AModel(IUnitOfWork unitOfWork, ILoggerHelperRepository logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void OnGet(string? UserId)
        {
            ModelState.Clear();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostFormA(RequestDTO.DTORequest request)
        {
            _logger.LogMessage("กำลังทำรายการ ลงทะเบียน " + DateTime.Now);
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (request.userId == null)
                {
                    var user = User.GetUser();
                    request.userId = user.userid;
                }

                request.type_cdd = 1;

                await _unitOfWork.CDDRepository.InsertCDD(request);

                _logger.LogMessage("บันทึกข้อมูลเรียบร้อย " + DateTime.Now);
                return new JsonResult(new { status = "success", messageArray = "success" });
            }
            catch (Exception ex)
            {
                _logger.LogMessage("เกิดข้อผิดพลาด " + "error : " + ex + " inner : " + ex.InnerException);

                return BadRequest(new { success = false, message = "ส่งอีเมลไม่สำเร็จ กรุณาตรวจสอบข้อมูลอีกครั้ง" });

                //throw new Exception("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetAnnouncement()
        {           
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.AnnouncementRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetPosition()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.PositionRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.TypeForm == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetProvince()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.ProvincesRepository.GetAllProvinces();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetPrefix()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.PrefixRepositoy.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetReligion()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.ReligionRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetNationality()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.NationalityRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetMilitaryStatus()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.MilitaryStatusRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetMarriedStatus()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.MarriedStatusRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetQualification()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.QualificationRepository.GetAllAsync();
            db = db.Where(x => x.Status == 1 && x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetLanguageAbility()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var db = await _unitOfWork.LanguageAbilityRepository.GetAllAsync();
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
