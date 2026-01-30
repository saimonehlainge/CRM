using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;
using System.Globalization;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class CDDDetailModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CDDDetailModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int? Id)
        {

        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetCDD(int? Id)
        {
            try
            {
                var DB = await _unitOfWork.CDDRepository.GetAllAsync();
                DB = DB.Where(x => x.Id == Id).ToList();
                if (DB.Count() == 0)
                {
                    return BadRequest("ไม่มีข้อมูล");
                }
                else
                {
                    var (userid, roles, _, department) = User.GetUser();
                    var PermissionSetting = await _unitOfWork.PermissionSettingRepository.GetAllAsync();
                    var Check = PermissionSetting.Where(x => x.Permission == roles && x.editteam == true).ToList();
                    if (Check.Count() == 0)
                    {
                        var Announcement = await _unitOfWork.AnnouncementRepository.GetAllAsync();
                        var Position = await _unitOfWork.PositionRepository.GetAllAsync();
                        var WorkOtherProvinces = await _unitOfWork.WorkOtherProvincesRepository.GetAllAsync();
                        var Religion = await _unitOfWork.ReligionRepository.GetAllAsync();
                        var Nationality = await _unitOfWork.NationalityRepository.GetAllAsync();
                        var MilitaryStatus = await _unitOfWork.MilitaryStatusRepository.GetAllAsync();
                        var MarriedStatus = await _unitOfWork.MarriedStatusRepository.GetAllAsync();
                        var Qualification = await _unitOfWork.QualificationRepository.GetAllAsync();
                        var LanguageAbility = await _unitOfWork.LanguageAbilityRepository.GetAllAsync();
                        var Provinces = await _unitOfWork.ProvincesRepository.GetAllAsync();
                        var HistoryCovid = await _unitOfWork.HistoryCovidRepository.GetAllAsync();
                        var Prefix = await _unitOfWork.PrefixRepositoy.GetAllAsync();
                        var Select = DB.Select(x => new ResponseDTO.CDDREsponse
                        {
                            TypeCdd = x.TypeCdd,
                            Id = x.Id,
                            Announcement = x.Announcement == -1 ? null : x.Announcement,
                            Position = x.Position == -1 ? null : x.Position,
                            Branch = x.Branch == null ? null : x.Branch,
                            Salary = x.Salary == null ? null : x.Salary.ToString(),
                            WorkArea = x.WorkArea == -1 ? null : x.WorkArea,
                            Prefix = x.Prefix == -1 ? null : x.Prefix,
                            NameThai = x.NameTH == null ? null : x.NameTH,
                            NameEng = x.NameENG == null ? null : x.NameENG,
                            Nikname = x.Nikname == null ? null : x.Nikname,
                            Tel = x.Tel == null ? null : x.Tel,
                            LineId = x.LineId == null ? null : x.LineId,
                            Email = x.Email == null ? null : x.Email,
                            Birthday = x.Birthday == null ? null : x.Birthday.Value.ToString("yyyy-MM-dd"), //ToString("dd/MM/yyyy")
                            Religion = x.Religion == -1 ? null : x.Religion,
                            Nationality = x.Nationality == -1 ? null : x.Nationality,
                            IDCard = x.IDCard == null ? null : x.IDCard,
                            EndDateIDCard = x.EndDateIDCard == null ? null : x.EndDateIDCard.Value.ToString("yyyy-MM-dd"), //.ToString("dd/MM/yyyy")
                            MilitaryStatus = x.MilitaryStatus == -1 ? null : x.MilitaryStatus,
                            MarriedStatus = x.MarriedStatus == -1 ? null : x.MarriedStatus,
                            IdCardAddress = x.IdCardAddress == null ? null : x.IdCardAddress,
                            CurrentAddress = x.CurrentAddress == null ? null : x.CurrentAddress,
                            StudyLocation = x.StudyLocation == null ? null : x.StudyLocation,
                            ComapnyName = x.ComapnyName == null ? null : x.ComapnyName,
                            StartEndWork = x.StartEndWork == null ? null : x.StartEndWork,
                            WorkPosition = x.WorkPosition == null ? null : x.WorkPosition,
                            WorkSalary = x.WorkSalary == null ? null : x.WorkSalary,
                            NoteWork = x.NoteWork == null ? null : x.NoteWork,
                            LanguageAbilityOther = x.LanguageAbilityOther == null ? null : x.LanguageAbilityOther,
                            OtherSpecialAbility = x.OtherSpecialAbility == null ? null : x.OtherSpecialAbility,
                            HistoryCovidVaccine = x.HistoryCovidVaccine == null ? null : x.HistoryCovidVaccine,
                            WhoCanCheckName = x.WhoCanCheckName == null ? null : x.WhoCanCheckName,
                            WhoCanCheckTel = x.WhoCanCheckTel == null ? null : x.WhoCanCheckTel,
                            WhoCanCheckAdress = x.WhoCanCheckAdress == null ? null : x.WhoCanCheckAdress,
                            WhoCanCheckRelated = x.WhoCanCheckRelated == null ? null : x.WhoCanCheckRelated,
                            EmergencyName = x.EmergencyName == null ? null : x.EmergencyName,
                            EmergencyTel = x.EmergencyTel == null ? null : x.EmergencyTel,
                            EmergencyAdress = x.EmergencyAdress == null ? null : x.EmergencyAdress,
                            EmergencyRelated = x.EmergencyRelated == null ? null : x.EmergencyRelated,
                            DocumentCDD = x.DocumentCDD == null ? null : x.DocumentCDD,
                            DocumentWorkCertification = x.DocumentWorkCertification == null ? null : x.DocumentWorkCertification,
                            WorkOtheProvinces = x.WorkOtheProvinces == null ? null : x.WorkOtheProvinces,
                            LanguageAbility = x.LanguageAbility == null ? null : x.LanguageAbility,
                            Qualification = x.Qualification == null ? null : x.Qualification
                        }).FirstOrDefault();

                        var response = new JsonResult(new
                        {
                            Select,
                            Announcement,
                            Position,
                            WorkOtherProvinces,
                            Religion,
                            Nationality,
                            MilitaryStatus,
                            MarriedStatus,
                            Qualification,
                            LanguageAbility,
                            Provinces,
                            HistoryCovid,
                            Prefix
                        });
                        return response;

                    }
                    else
                    {

                        var Announcement = await _unitOfWork.AnnouncementRepository.GetAllAsync();
                        var Position = await _unitOfWork.PositionRepository.GetAllAsync();
                        var WorkOtherProvinces = await _unitOfWork.WorkOtherProvincesRepository.GetAllAsync();
                        var Religion = await _unitOfWork.ReligionRepository.GetAllAsync();
                        var Nationality = await _unitOfWork.NationalityRepository.GetAllAsync();
                        var MilitaryStatus = await _unitOfWork.MilitaryStatusRepository.GetAllAsync();
                        var MarriedStatus = await _unitOfWork.MarriedStatusRepository.GetAllAsync();
                        var Qualification = await _unitOfWork.QualificationRepository.GetAllAsync();
                        var LanguageAbility = await _unitOfWork.LanguageAbilityRepository.GetAllAsync();
                        var Provinces = await _unitOfWork.ProvincesRepository.GetAllAsync();
                        var HistoryCovid = await _unitOfWork.HistoryCovidRepository.GetAllAsync();
                        var Prefix = await _unitOfWork.PrefixRepositoy.GetAllAsync();
                        var Select = DB.Select(x => new ResponseDTO.CDDREsponse
                        {
                            TypeCdd = x.TypeCdd,
                            Id = x.Id,
                            Announcement = x.Announcement == -1 ? null : x.Announcement,
                            Position = x.Position == -1 ? null : x.Position,
                            Branch = x.Branch == null ? null : x.Branch,
                            Salary = x.Salary == null ? null : x.Salary.ToString(),
                            WorkArea = x.WorkArea == -1 ? null : x.WorkArea,
                            Prefix = x.Prefix == -1 ? null : x.Prefix,
                            NameThai = x.NameTH == null ? null : x.NameTH,
                            NameEng = x.NameENG == null ? null : x.NameENG,
                            Nikname = x.Nikname == null ? null : x.Nikname,
                            Tel = x.Tel == null ? null : x.Tel,
                            LineId = x.LineId == null ? null : x.LineId,
                            Email = x.Email == null ? null : x.Email,
                            Birthday = x.Birthday == null ? null : x.Birthday.Value.ToString("yyyy-MM-dd"),
                            Religion = x.Religion == -1 ? null : x.Religion,
                            Nationality = x.Nationality == -1 ? null : x.Nationality,
                            IDCard = x.IDCard == null ? null : x.IDCard,
                            EndDateIDCard = x.EndDateIDCard == null ? null : x.EndDateIDCard.Value.ToString("yyyy-MM-dd"),
                            MilitaryStatus = x.MilitaryStatus == -1 ? null : x.MilitaryStatus,
                            MarriedStatus = x.MarriedStatus == -1 ? null : x.MarriedStatus,
                            IdCardAddress = x.IdCardAddress == null ? null : x.IdCardAddress,
                            CurrentAddress = x.CurrentAddress == null ? null : x.CurrentAddress,
                            StudyLocation = x.StudyLocation == null ? null : x.StudyLocation,
                            ComapnyName = x.ComapnyName == null ? null : x.ComapnyName,
                            StartEndWork = x.StartEndWork == null ? null : x.StartEndWork,
                            WorkPosition = x.WorkPosition == null ? null : x.WorkPosition,
                            WorkSalary = x.WorkSalary == null ? null : x.WorkSalary,
                            NoteWork = x.NoteWork == null ? null : x.NoteWork,
                            LanguageAbilityOther = x.LanguageAbilityOther == null ? null : x.LanguageAbilityOther,
                            OtherSpecialAbility = x.OtherSpecialAbility == null ? null : x.OtherSpecialAbility,
                            HistoryCovidVaccine = x.HistoryCovidVaccine == null ? null : x.HistoryCovidVaccine,
                            WhoCanCheckName = x.WhoCanCheckName == null ? null : x.WhoCanCheckName,
                            WhoCanCheckTel = x.WhoCanCheckTel == null ? null : x.WhoCanCheckTel,
                            WhoCanCheckAdress = x.WhoCanCheckAdress == null ? null : x.WhoCanCheckAdress,
                            WhoCanCheckRelated = x.WhoCanCheckRelated == null ? null : x.WhoCanCheckRelated,
                            EmergencyName = x.EmergencyName == null ? null : x.EmergencyName,
                            EmergencyTel = x.EmergencyTel == null ? null : x.EmergencyTel,
                            EmergencyAdress = x.EmergencyAdress == null ? null : x.EmergencyAdress,
                            EmergencyRelated = x.EmergencyRelated == null ? null : x.EmergencyRelated,
                            DocumentCDD = x.DocumentCDD == null ? null : x.DocumentCDD,
                            DocumentWorkCertification = x.DocumentWorkCertification == null ? null : x.DocumentWorkCertification,
                            WorkOtheProvinces = x.WorkOtheProvinces == null ? null : x.WorkOtheProvinces,
                            LanguageAbility = x.LanguageAbility == null ? null : x.LanguageAbility,
                            Qualification = x.Qualification == null ? null : x.Qualification
                        }).FirstOrDefault();

                        var response = new JsonResult(new
                        {
                            Select,
                            Announcement,
                            Position,
                            WorkOtherProvinces,
                            Religion,
                            Nationality,
                            MilitaryStatus,
                            MarriedStatus,
                            Qualification,
                            LanguageAbility,
                            Provinces,
                            HistoryCovid,
                            Prefix
                        });
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEditCDD(RequestDTO.DTORequest request)
        {
            return await _unitOfWork.CDDRepository.UpdateCDD(request);
        }
    }
}
