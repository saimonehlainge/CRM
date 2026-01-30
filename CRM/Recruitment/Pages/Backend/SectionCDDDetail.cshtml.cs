using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;

namespace Recruitment.Pages.Backend
{
    public class SectionCDDDetailModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public SectionCDDDetailModel(IUnitOfWork unitOfWork)
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

                    var (userid, roles, _, department) = User.GetUser();
                    var PermissionSetting = await _unitOfWork.PermissionSettingRepository.GetAllAsync();
                    var Check = PermissionSetting.Where(x => x.Permission == roles && x.editteam == true).ToList();
                    if (Check.Count() == 0)
                    {
                        Check = PermissionSetting.Where(x => x.Permission == roles && x.editinfo == true).ToList();
                        if (Check.Count() == 0)
                        {
                            return new JsonResult(null);
                        }
                        else
                        {
                            DB = DB.Where(x => x.UserId == userid).ToList();
                            if (DB.Count() == 0)
                            {
                                return new JsonResult(null);
                            }
                            else
                            {
                                var Select = DB.Select(x => new
                                {
                                    Id = x.Id,
                                    Announcement = (x.Announcement == -1 || x.Announcement == null) ? "ไม่มีข้อมูล" : Announcement.FirstOrDefault(a => a.Id == x.Announcement).Name,
                                    Position = x.Position == -1 ? "ไม่มีข้อมูล" : Position.FirstOrDefault(p => p.Id == x.Position).Name,
                                    Branch = x.Branch == null ? null : x.Branch,
                                    Salary = x.Salary == null ? null : x.Salary.ToString(),
                                    WorkArea = x.WorkArea == -1 ? "ไม่มีข้อมูล" : WorkOtherProvinces.FirstOrDefault(w => w.Id == x.WorkArea).Name,
                                    NameThai = x.NameTH == null ? null : x.NameTH,
                                    NameEng = x.NameENG == null ? null : x.NameENG,
                                    Nikname = x.Nikname == null ? null : x.Nikname,
                                    Tel = x.Tel == null ? null : x.Tel,
                                    LineId = x.LineId == null ? null : x.LineId,
                                    Email = x.Email == null ? null : x.Email,
                                    Birthday = x.Birthday == null ? null : x.Birthday.Value.ToString("dd/MM/yyyy"),
                                    Religion = x.Religion == -1 ? "ไม่มีข้อมูล" : Religion.FirstOrDefault(r => r.Id == x.Religion).Name,
                                    Nationality = x.Nationality == -1 ? "ไม่มีข้อมูล" : Nationality.FirstOrDefault(n => n.Id == x.Nationality).Name,
                                    IDCard = x.IDCard == null ? null : x.IDCard,
                                    EndDateIDCard = x.EndDateIDCard == null ? null : x.EndDateIDCard.Value.ToString("dd/MM/yyyy"),
                                    MilitaryStatus = x.MilitaryStatus == -1 ? "ไม่มีข้อมูล" : MilitaryStatus.FirstOrDefault(m => m.Id == x.MilitaryStatus).Name,
                                    MarriedStatus = x.MarriedStatus == -1 ? "ไม่มีข้อมูล" : MarriedStatus.FirstOrDefault(m => m.Id == x.MarriedStatus).Name,
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
                                    HistoryCovidVaccine = x.HistoryCovidVaccine == -1 ? "ไม่มีข้อมูล" : HistoryCovid.FirstOrDefault(h => h.Id == x.HistoryCovidVaccine).Name,
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
                                    WorkOtheProvinces = x.WorkOtheProvinces == -1 ? "ไม่มีข้อมูล" : Provinces.FirstOrDefault(p => p.Id == x.WorkOtheProvinces).NameInThai,
                                    LanguageAbility = x.LanguageAbility == -1 ? "ไม่มีข้อมูล" : LanguageAbility.FirstOrDefault(l => l.Id == x.LanguageAbility).Name,
                                    Qualification = x.Qualification == -1 ? "ไม่มีข้อมูล" : Qualification.FirstOrDefault(q => q.Id == x.Qualification).Name
                                }).FirstOrDefault();

                                var response = new JsonResult(Select);
                                return response;
                            }
                        }
                    }
                    else
                    {
                        var Select = DB.Select(x => new
                        {
                            Id = x.Id,
                            Announcement = (x.Announcement == -1 || x.Announcement == null) ? "ไม่มีข้อมูล" : Announcement.FirstOrDefault(a => a.Id == x.Announcement).Name,
                            Position = (x.Position == -1 || x.Position == null) ? "ไม่มีข้อมูล" : Position.FirstOrDefault(p => p.Id == x.Position).Name,
                            Branch = x.Branch == null ? null : x.Branch,
                            Salary = x.Salary == null ? null : x.Salary.ToString(),
                            WorkArea = (x.WorkArea == -1 || x.WorkArea == null) ? "ไม่มีข้อมูล" : WorkOtherProvinces.FirstOrDefault(w => w.Id == x.WorkArea).Name,
                            NameThai = x.NameTH == null ? null : x.NameTH,
                            NameEng = x.NameENG == null ? null : x.NameENG,
                            Nikname = x.Nikname == null ? null : x.Nikname,
                            Tel = x.Tel == null ? null : x.Tel,
                            LineId = x.LineId == null ? null : x.LineId,
                            Email = x.Email == null ? null : x.Email,
                            Birthday = x.Birthday == null ? null : x.Birthday.Value.ToString("dd/MM/yyyy"),
                            Religion = (x.Religion == -1 || x.Religion == null) ? "ไม่มีข้อมูล" : Religion.FirstOrDefault(r => r.Id == x.Religion).Name,
                            Nationality = (x.Nationality == -1 || x.Nationality == null) ? "ไม่มีข้อมูล" : Nationality.FirstOrDefault(n => n.Id == x.Nationality).Name,
                            IDCard = x.IDCard == null ? null : x.IDCard,
                            EndDateIDCard = x.EndDateIDCard == null ? null : x.EndDateIDCard.Value.ToString("dd/MM/yyyy"),
                            MilitaryStatus = (x.MilitaryStatus == null || x.MilitaryStatus == -1) ? "ไม่มีข้อมูล" : MilitaryStatus.FirstOrDefault(m => m.Id == x.MilitaryStatus).Name,
                            MarriedStatus = (x.MarriedStatus == null || x.MarriedStatus == -1) ? "ไม่มีข้อมูล" : MarriedStatus.FirstOrDefault(m => m.Id == x.MarriedStatus).Name,
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
                            HistoryCovidVaccine = (x.HistoryCovidVaccine == null || x.HistoryCovidVaccine == -1) ? "ไม่มีข้อมูล" : HistoryCovid.FirstOrDefault(h => h.Id == x.HistoryCovidVaccine).Name,
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
                            WorkOtheProvinces = (x.WorkOtheProvinces == -1 || x.WorkOtheProvinces == null) ? "ไม่มีข้อมูล" : Provinces.FirstOrDefault(p => p.Id == x.WorkOtheProvinces).NameInThai,
                            LanguageAbility = (x.LanguageAbility == -1 || x.LanguageAbility == null) ? "ไม่มีข้อมูล" : LanguageAbility.FirstOrDefault(l => l.Id == x.LanguageAbility).Name,
                            Qualification = (x.Qualification == -1 || x.Qualification == null) ? "ไม่มีข้อมูล" : Qualification.FirstOrDefault(q => q.Id == x.Qualification).Name
                        }).FirstOrDefault();

                        var response = new JsonResult(Select);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
