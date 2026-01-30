using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public class ResponseDTO
    {
        public class NotiResponse
        {
            public int Id { get; set; }
            public string? Message { get; set; }
            public int? View { get; set; }
            public int? CDDId { get; set; }
            public string? UserId { get; set; }
            public string? CreatedDate { get; set; }
            public DateTimeOffset? UpdatedDate { get; set; }
        }

        public class SetKpiComm
        {
            public int Id { get; set; }
            public string? Project { get; set; }
            public int? SetKpi_Day { get; set; }
            public int? GuaranteeDay { get; set; }
            public string? Comm { get; set; }
            public string? Kpi { get; set; }
            public int? Status { get; set; }
        }

        public class CalculateCollectionDays
        {
            public int? Id { get; set; }
            public string? Project { get; set; }
            public int? CollectionPeriod { get; set; }
            public int? Status { get; set; }
        }

        public class ThinkingTimeDay
        {
            public int Id { get; set; }
            public int? Project { get; set; }
            public DateTime? StartDate { get; set; }
            public int? Thinkingtimedays { get; set; }
            public DateTime? CollectionDay { get; set; }
            public int? Status { get; set; }
            public int? DeleteAt { get; set; }

        }

        public class ProjectResponse
        {
            public int Id { get; set; }
            public string? Project { get; set; }
            public int? Thinkingtimedays { get; set; }
            public int? Status { get; set; }
        }

        public class GetRoleUser
        {
            public string? userid { get; set; }
            public string? role { get; set; }
        }

        public class Calendar
        {
            public string? Userid { get; set; }
            public string? Detail { get; set; }
            public string? Description { get; set; }
            public DateTimeOffset? StartDate { get; set; }
            public DateTimeOffset? EndDate { get; set; }
            public string? Color { get; set; }
            public int? Status { get; set; }
        }

        public class datateam
        {
            public int? Id { get; set; }
            public string? name { get; set; }
            public string? department { get; set; }
        }

        public class GroupUserResponse
        {
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
            public string? Address { get; set; }
            public string? RawPassword { get; set; }
            public int? DepartmentId { get; set; }
            public int? Project { get; set; }
            public IsActive? IsActive { get; set; }
            public DateTimeOffset? CreatedDate { get; set; }
            public DateTimeOffset? UpdatedDate { get; set; }
            public int? DeleteAt { get; set; }
            public string? RoleId { get; set; }
            public string? UserId { get; set; }
        }

        public class UserResponse
        {
            public string? Id { get; set; }
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
            public string? Department { get; set; }
            public string? Permission { get; set; }
            public int? Target { get; set; }
            public DateTime? Targetdate { get; set; }
            public DateTime? Todate { get; set; }
            public int? Project { get; set; }
            public int? DeleteAt { get; set; }
        }

        public class RecruitResponse
        {
            public string? Id { get; set; }
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
            public string? role { get; set; }
        }

        public class IdentityUserRoleResponse
        {
            public string? UserId { get; set; }
            public string? RoleId { get; set; }
        }

        public class RecruitStatusResponse
        {
            public int Id { get; set; }
            public string? project { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
            public DateTimeOffset? CreatedDate { get; set; }
            public DateTimeOffset? UpdatedDate { get; set; }
            public int? DeleteAt { get; set; }
            public int? typeform { get; set; }
        }

        public class ReportResponse
        {
            public int? index { get; set; }
            public string? name { get; set; }
            public string? status { get; set; }
            public string? total_lead { get; set; }
            public string? not_interested { get; set; }
            public string? screen_noshow { get; set; }
            public string? screen_notpass { get; set; }
            public string? client_noshow { get; set; }
            public string? client_notpass { get; set; }
            public string? notjoin { get; set; }
            public string? dropout { get; set; }
            public string? success_kpi { get; set; }
            public string? notpass_guarantee { get; set; }


            public string? project1 { get; set; }
            public string? project2 { get; set; }
            public string? project3 { get; set; }
            public string? project4 { get; set; }
            public string? project5 { get; set; }
            public string? project6 { get; set; }
            public string? project7 { get; set; }
            public string? project8 { get; set; }
            public string? project9 { get; set; }
            public string? project10 { get; set; }
            public string? project11 { get; set; }
            public string? project12 { get; set; }
            public string? project13 { get; set; }
            public string? project14 { get; set; }
            public string? project15 { get; set; }
            public string? project16 { get; set; }
            public string? project17 { get; set; }
            public string? project18 { get; set; }
            public string? project19 { get; set; }
            public string? project20 { get; set; }
            public string? project21 { get; set; }
            public string? total_project { get; set; }

            public List<string>? project { get; set; }
            public List<List<string>>? CountData { get; set; }
            public List<string>? recruit { get; set; }
        }

        public class ReportResponseExport
        {
            public int? id { get; set; }
            public string? userId { get; set; }
            public string? name { get; set; }
            public string? nickname { get; set; }
            public string? tel { get; set; }
            public int? project { get; set; }
            public string? kpi { get; set; }
            public int? status { get; set; }
            public DateTime? startdate { get; set; }
            public DateTime? enddate { get; set; }
            public DateTimeOffset? Date { get; set; }
            public DateTimeOffset? Update { get; set; }
        }

        public class SummaryResponse
        {
            public int? typeform { get; set; }
            public int? id { get; set; }
            public string recruit { get; set; }
            public string? name { get; set; }
            public string? nickname { get; set; }
            public string? tel { get; set; }
            public string? project { get; set; }
            public string? kpi { get; set; }
            public string? status { get; set; }
            public DateTimeOffset? Date { get; set; }
        }

        public class CDDREsponse
        {
            public int? TypeCdd { get; set; }
            public int? Id { get; set; }
            public int? Announcement { get; set; }
            public int? Company { get; set; }
            public int? Position { get; set; }
            public string? Branch { get; set; }
            public string? Salary { get; set; }
            public int? WorkArea { get; set; }
            public int? Prefix { get; set; }
            public string? NameThai { get; set; }
            public string? NameEng { get; set; }
            public string? Nikname { get; set; }
            public string? Tel { get; set; }
            public string? LineId { get; set; }
            public string? Email { get; set; }
            public string? Birthday { get; set; }
            public int? Religion { get; set; }
            public int? Nationality { get; set; }
            public string? IDCard { get; set; }
            public string? EndDateIDCard { get; set; }
            public int? MilitaryStatus { get; set; }
            public int? MarriedStatus { get; set; }
            public string? IdCardAddress { get; set; }
            public string? CurrentAddress { get; set; }
            public int? Qualification { get; set; }
            public string? StudyLocation { get; set; }
            public string? ComapnyName { get; set; }
            public string? StartEndWork { get; set; }
            public string? WorkPosition { get; set; }
            public string? WorkSalary { get; set; }
            public string? NoteWork { get; set; }
            public int? LanguageAbility { get; set; }
            public string? LanguageAbilityOther { get; set; }
            public string? OtherSpecialAbility { get; set; }
            public int? WorkOtheProvinces { get; set; }
            public int? HistoryCovidVaccine { get; set; }
            public string? WhoCanCheckName { get; set; }
            public string? WhoCanCheckTel { get; set; }
            public string? WhoCanCheckAdress { get; set; }
            public string? WhoCanCheckRelated { get; set; }
            public string? EmergencyName { get; set; }
            public string? EmergencyTel { get; set; }
            public string? EmergencyAdress { get; set; }
            public string? EmergencyRelated { get; set; }
            public string? DocumentCDD { get; set; }
            public string? DocumentWorkCertification { get; set; }
        }

        public class PositionResponse
        {
            public int? index { get; set; }
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? TypeForm { get; set; }
            public int? Status { get; set; }
            public int? DeleteAt { get; set; }
        }

        public class SummaryDataResponse
        {
            public int Id { get; set; }
            public string? UserId { get; set; }
            public string? BillingCycle { get; set; }
            public int? TypeCdd { get; set; }
            public int? Project { get; set; }
            public int? Company { get; set; }
            public int? Announcement { get; set; }
            public int? Position { get; set; }
            public string? HRContact { get; set; }
            public string? Branch { get; set; }
            public decimal? Salary { get; set; }
            public int? WorkArea { get; set; }
            public int? Prefix { get; set; }
            public string? NameTH { get; set; }
            public string? NameENG { get; set; }
            public string? Nikname { get; set; }
            public string? Tel { get; set; }
            public string? LineId { get; set; }
            public string? Email { get; set; }
            public DateTimeOffset? Birthday { get; set; }
            public int? Religion { get; set; }
            public int? Nationality { get; set; }
            public string? IDCard { get; set; }
            public DateTimeOffset? EndDateIDCard { get; set; }
            public int? MilitaryStatus { get; set; }
            public int? MarriedStatus { get; set; }
            public string? IdCardAddress { get; set; }
            public string? CurrentAddress { get; set; }
            public int? Qualification { get; set; }
            public string? StudyLocation { get; set; }
            public string? ComapnyName { get; set; }
            public string? StartEndWork { get; set; }
            public string? WorkPosition { get; set; }
            public string? WorkSalary { get; set; }
            public string? NoteWork { get; set; }
            public int? LanguageAbility { get; set; }
            public string? LanguageAbilityOther { get; set; }
            public string? OtherSpecialAbility { get; set; }
            public int? WorkOtheProvinces { get; set; }
            public int? HistoryCovidVaccine { get; set; }
            public string? WhoCanCheckName { get; set; }
            public string? WhoCanCheckTel { get; set; }
            public string? WhoCanCheckAdress { get; set; }
            public string? WhoCanCheckRelated { get; set; }
            public string? EmergencyName { get; set; }
            public string? EmergencyTel { get; set; }
            public string? EmergencyAdress { get; set; }
            public string? EmergencyRelated { get; set; }
            public string? DocumentCDD { get; set; }
            public string? DocumentWorkCertification { get; set; }
            public string? DocumentBank { get; set; }
            public string? DocumentHistroy { get; set; }
            public string? DocumentJng { get; set; }
            public string? KPI { get; set; }
            public int? Status { get; set; }
            public DateTime? StatusStart { get; set; }
            public DateTime? StatusEnd { get; set; }
            public string? NoteStatus { get; set; }
            public string? Cause { get; set; }
            public DateTimeOffset? CreatedDate { get; set; }
            public DateTimeOffset? UpdatedDate { get; set; }
            public int? DeleteAt { get; set; }
            public DateTime? DateStatus { get; set; }
        }
    }
}
