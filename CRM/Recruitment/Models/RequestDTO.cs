using Microsoft.AspNetCore.Components.Web;
using Recruitment.Areas.Identity.Data;
using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Models
{
    public class RequestDTO
    {
        public class SetKpiComm
        {
            public int Id { get; set; }
            public int? Project { get; set; }
            public int? SetKpi_Day { get; set; }
            public int? GuaranteeDay { get; set; }
            public decimal? Comm { get; set; }
            public decimal? Kpi { get; set; }
            public int? Rank { get; set; }
            public int? Status { get; set; }
        }

        public class CalculateCollectionDays
        {
            public int Id { get; set; }
            public int? Project { get; set; }
            public int? CollectionPeriod { get; set; }
            public int? Status { get; set; }
        }

        public class ThinkingTimeDay
        {
            public int Id { get; set; }
            public int? Project { get; set; }
            public int? Thinkingtimedays { get; set; }
            public int? Status { get; set; }
        }

        public class TeamRequest
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class UserTeamRequset
        {
            public int Id { get; set; }
            public string? userId { get; set; }
            public int? departmentId { get; set; }
            public int? teamposition { get; set; }
            public int? teamId { get; set; }
        }

        public class EmailSettingRequest
        {
            public int Id { get; set; }
            public string? Email { get; set; } // Email
        }

        public class MailRequest
        {
            public string? ToEmail { get; set; }
            public string? Subject { get; set; }
            public string? Body { get; set; }
            public List<IFormFile>? Attachments { get; set; }
        }

        public class CommentCDDRequset
        {
            public int? Id { get; set; }
            public int? CDDId { get; set; }
            public string? Highlights { get; set; }
            public string? Observations { get; set; }
            public decimal? Score { get; set; }
        }

        public class ChangeCDDRequset
        {
            public int? Id { get; set; }
            public string? userid { get; set; }
            public int? Status { get; set; }
            public int? Project { get; set; }
            public string? KPI { get; set; }
            public string? NoteStatus { get; set; }
            public string? Cause { get; set; }
            public string? WorkSalary { get; set; } // เงินเดือน      
            public DateTime? StatusStart { get; set; }
            public DateTime? StatusEnd { get; set; }
            public DateTime? datekpiSuccess { get; set; }
            public DateTime? datestatus { get; set; }
        }

        public class DTORequest
        {
            public int? Id { get; set; }
            public string? userId { get; set; }
            public int? type_cdd { get; set; } // ประเภทใบสมัครงาน
            public int? Company { get; set; } //บริษัทที่ต้องการสมัคร
            public int? Announcement { get; set; } // เห็นประกาศช่องทางใด         
            public int? Position { get; set; } // ตำแหน่ง           
            public string? HRContact { get; set; } // HR ติดต่อ
            public string? Branch { get; set; } // สาขา/ออฟฟิศที่ทำงาน
            public decimal? Salary { get; set; } // เงินเดือน      
            public int? WorkArea { get; set; } // ต้องการงานในพื้นที่      
            public int? Prefix { get; set; } // คำนำหน้า           
            public string? NameTH { get; set; } // ชื่อ-นามสกุล ไทย        
            public string? NameENG { get; set; } // ชื่อ-นามสกุล อังกฤษ          
            public string? Nikname { get; set; } // ชื่อเล่น           
            public string? Tel { get; set; } // เบอร์โทรศัพท์        
            public string? LineId { get; set; } // ไลน์ไอดี          
            public string? Email { get; set; }  // อีเมล         
            public DateTimeOffset? Birthday { get; set; } // วันเกิด          
            public int? Religion { get; set; }  // ศาสนา            
            public int? Nationality { get; set; } // สัญชาติ            
            public string? IDCard { get; set; } // เลขบัตรประชาชน           
            public DateTimeOffset? EndDateIDCard { get; set; } // วันหมดอายุบัตรประชาชน           
            public int? MilitaryStatus { get; set; } // สถานะทางทหาร            
            public int? MarriedStatus { get; set; } // สถานะสมรส          
            public string? IdCardAddress { get; set; }  // ที่อยู่ตามเลขบัตรประชาชน            
            public string? CurrentAddress { get; set; } // ที่อยู่ปัจจุบัน           
            public int? Qualification { get; set; } // วุฒิ            
            public string? StudyLocation { get; set; } // ชื่อสถานที่ศึกษา           
            public string? ComapnyName { get; set; } // ชื่อบริษัท
            public string? StartEndWork { get; set; } // เดือนและปีที่เริ่มและสิ้นสุด
            public string? WorkPosition { get; set; } // ตำแหน่งงาน
            public string? WorkSalary { get; set; } // เงินเดือน
            public string? NoteWork { get; set; } // เงินเดือน
            public int? LanguageAbility { get; set; } // ความสามารถภาษา
            public string? LanguageAbilityOther { get; set; } // ความสามารถภาษาอื่นๆ
            public string? OtherSpecialAbility { get; set; } // ความสามารถพิเศษ           
            public int? WorkOtheProvinces { get; set; } // สามารถไปปฎิบัติงานต่างจังหวัด            
            public int? HistoryCovidVaccine { get; set; } // ประวัติวัคซีนโควิด            
            public string? WhoCanCheckName { get; set; } // ชื่อผู้ติดต่อ
            public string? WhoCanCheckTel { get; set; } // เบอร์ผู้ติดต่อ
            public string? WhoCanCheckAdress { get; set; } // ที่อยู่ผู้ติดต่อ
            public string? WhoCanCheckRelated { get; set; } // เกี่ยวข้องกับผู้ติดต่อ
            public string? EmergencyName { get; set; } // ชื่อผู้ติดต่อฉุกเฉิน
            public string? EmergencyTel { get; set; } // เบอร์ผู้ติดต่อฉุกเฉิน
            public string? EmergencyAdress { get; set; } // ที่อยู่ผู้ติดต่อฉุกเฉิน
            public string? EmergencyRelated { get; set; } // เกี่ยวข้องผู้ติดต่อฉุกเฉิน
            public IFormFile? DocumentCDD { get; set; } // เอกสารประกอบการสมัครงาน            
            public IFormFile? DocumentWorkCertification { get; set; } // เอกสารรับรองการทำงาน
            public IFormFile? DocumentBank { get; set; } // เอกสารธนาคาร
            public IFormFile? DocumentHistroy { get; set; } // เอกสารธนาคาร
            public IFormFile? DocumentJng { get; set; } // เอกสารธนาคาร
            public string? congenitaldisease_detail { get; set; } // โรคประจำตัว
            public int? Status { get; set; } // สถานะข้อมูล 

            //----------------------- เงื่อนไข
            public int? Usedwork { get; set; } //อุปกรณ์ที่ใช้ในการทำงาน
            public int? Applicant_age { get; set; } //ผู้สมัครมีอายุระหว่าง 18-40 ปี
            public int? Applicant_pregnant { get; set; } //ผู้สมัครอยู่ระหว่างตั้งครรภ์ หรือมีแผนการตั้งครรภ์ในระยะเวลาเร็วๆ นี้
            public int? Ever_applied { get; set; } //ท่านเคยสมัครหรือทำงานที่ บริษัท ไบร์ทสเปซ หรือไม่
            public int? Studying { get; set; } //อยู่ในระหว่างกำลังศึกษาอยู่ หรือมีแผนที่จะเรียนต่อ ใช่หรือไม่ใช่
            public int? Defect { get; set; } //มีความบกพร่อง หรือพิการทางด้านร่างกาย ใช่หรือไม่ใช่
            public int? Current_degree { get; set; } //วุฒิปัจจุบันของท่านน้อยกว่า ม.6 ใช่หรือไม่
            public int? Under_pressure { get; set; } //ผู้สมัครสามารถรับเเรงกดดันได้ดีในการทำงานที่มียอด และทำงานเลยเวลางานเลิกปกติได้ ถึง 20.00 น. ใช่หรือไม่ใช่
            public int? Government_work { get; set; } //ผู้สมัครมีญาติพี่น้องที่รับราชการใช่ หรือ ไม่ใช่
            public int? Specific_qualifications { get; set; } //ผู้สมัครมีวุฒิ ป.ตรี  คณะรัฐศาสตร์ คณะนิติศาสตร์  คณะศึกษาศาสตร์ ดังที่กล่าวมานี้ ใช่หรือไม่ใช่
            public int? History_congenital_disease { get; set; } //ท่านมีโรคประจำตัว ใช่หรือไม่ใช่
            public int? Congenital_disease { get; set; } //หากมีโรคประจำตัว
            public int? Wage { get; set; } //ค่าแรงวันละ 363/วัน ต่อเดือนเฉลี่ยประมาณ 9,438 ขึ้นอยู่กับจำนวณวันในแต่ล่ะเดือน และมีค่าคอมมิชชั่นขึ้นอยู่กับความสามารถ ซึ่งค่าคอมมิชชั่นจะออกทุกเดือนในรอบวันที่ 5
            public int? Social_security { get; set; } //ทางบริษัทมีสวัสดิการเป็น ประกันสังคมให้ นับจากวันที่เซ็นสัญญาเป็นพนักงานประจำ
            public int? Work6stop1 { get; set; } //ทำงาน 6 วันต่อสัปดาห์ วันหยุด 1 วัน จะหยุดพร้อมกันทั้งทีม ทางหัวหน้าทีมจะทำตารางแจ้งให้ทราบในกลุ่มทีม(วันหยุดอาจไม่ตรงกับเสาร์/อาทิตย์)
            public int? Deduction_wages { get; set; } //เป็นงานทำประจำแต่คิดค่าแรงเป็นรายวันจ่ายตามวันทำงานจริง การขาดงานไม่มีเหตุผลเพียงพอ บริษัทจะหักค่าแรง 2 เท่า
            public int? Deduction_wages50 { get; set; } //ทำงาน 8.30 น. เลิกงาน 18.00 น. และถ้าหากทำยอดไม่ถึง 50% - 70 % ของยอดที่ได้รับมา ต้องทำงานต่อถึง 20.00 โดยเป็นความรับผิดชอบในการทำยอดจะไม่มีการจ่ายค่าโอทีเพิ่มให้
            public int? Numbercalls { get; set; } //จำนวนการโทรต้องมากกว่าวันละ 280 รายชื่อ และมีระยะเวลาการคุยกับลูกค้ามากกว่า 2 ชม. ต่อวัน
            public int? Numbercalls15 { get; set; } //การโทรในเวลางานหากหยุดโทรติดต่อกันเกิน 15 นาทีหากมีการตรวจพบจะถูกเช็คขาดวันนั้นทันที
            public int? Missing_work { get; set; } //หากมีการขาดงานบ่อย หรือ ลางานบ่อย หรือ ทำยอดไม่ได้ติดต่อกันเป็นเวลาหลายวัน บริษัทอาจมีการพิจารณาให้ออก ทันที
            public int? Cost_work2 { get; set; } //วันหยุดนักขัตฤกษ์ทางบริษัทจะมีการจ่ายค่าแรงเป็น 2 เท่าให้หากมีการตกลงให้เข้ามาทำงาน
            public int? Trend12 { get; set; } //  ลงชื่อแล้วไม่สามารถเข้าเทรนตามรอบได้ทางบริษัทจะขออนุญาตตัดสิทธิ์ในการเข้าเทรน
            public int? Trend23 { get; set; } //การเทรนงานอบรม แต่ละรอบจะทำการอบรม 2-3 วัน ผ่านโปรแกรม Zoom
            public int? Confirm_info { get; set; } //ข้าพเจ้าในนามของ "ผู้สมัครงาน" มีบัตรประจำตัวประชาชนที่ออกโดยประเทศไทย
            public int? Nationality2 { get; set; } //ข้าพเจ้ายินยอมให้ตัดสิทธิ์ในการสมัครงานทันที

        }

        public class PrefixRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class QualificationRequest
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class PositionRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public List<int>? Form { get; set; }
            public int? Status { get; set; }
        }

        public class AnnouncementRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class WorkOtherProvincesRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class MilitaryStatusRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class MarriedStatusRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class ProvincesRequest
        {
            public int? Id { get; set; }
            public int? Code { get; set; }
            public string? NameInThai { get; set; }
            public string? NameInEnglish { get; set; }
            public int? Status { get; set; }
        }

        public class DistrictsRequest
        {
            public int? Id { get; set; }
            public int? Code { get; set; }
            public string? NameInThai { get; set; }
            public string? NameInEnglish { get; set; }
            public int ProvinceId { get; set; }
            public int? Status { get; set; }
        }

        public class SubDistrictsRequest
        {
            public int? Id { get; set; }
            public int? Code { get; set; }
            public string? NameInThai { get; set; }
            public string? NameInEnglish { get; set; }
            public decimal? Latitude { get; set; }
            public decimal? Longitude { get; set; }
            public int DistrictId { get; set; }
            public int? ZipCode { get; set; }
            public int? Status { get; set; }
        }

        public class LanguageRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class CompanyRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class HistoryCovidRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class RecruitStatusRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public List<int>? Form { get; set; }
            public int? Status { get; set; }
        }

        public class ProjectRequest
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? Status { get; set; }
        }

        public class UserRequest
        {
            public string? Id { get; set; }
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
            public string? Address { get; set; }
            public string? UserName { get; set; }
            public string? RawPassword { get; set; }
            public int? Status { get; set; }
            public string? RoleId { get; set; }
            public int? DepartmentId { get; set; }
            public int? ProjectId { get; set; }
            public int? TargetKpi { get; set; }
            public DateTime? Targetdate { get; set; }
            public DateTime? Todate { get; set; }
            public int? StatusTarget { get; set; }
        }

        public class UserRequests
        {
            public string? Id { get; set; }
            public string? Firstname { get; set; }
            public string? Lastname { get; set; }
            public string? Address { get; set; }
            public string? UserName { get; set; }
            public string? RawPassword { get; set; }
            public IsActive? Status { get; set; }
            public string? RoleId { get; set; }
            public int? DepartmentId { get; set; }
            public int? ProjectId { get; set; }
            public int? TargetKpi { get; set; }
            public string? Targetdate { get; set; }
            public string? Todate { get; set; }
            public int? StatusTarget { get; set; }
        }

        public class ChangeEvent
        {
            public string? EventName { get; set; }           
            public string? start { get; set; }
            public string? end { get; set; }
            public string? description { get;set; }

            public int? ChangeId { get; set; }
            public string? ChangeEventName { get; set; }
            public string? Changedescription { get; set; }
            public int? ChangeStatus { get; set; }
            public string? ChangeStart { get; set; }
            public string? ChangeEnd { get; set; }
        }

        public class CalendarRequset
        {
            public int? Id { get; set; }
            public List<string>? userid { get; set; }
            public string? userid_create { get; set; }
            public string? Detail { get; set; }
            public string? Description { get; set; }
            public DateTimeOffset? StartDate { get; set; }
            public DateTimeOffset? EndDate { get; set; }
            public int? Status { get; set; }
        }

        public class PermissionRequset
        {
            public List<string>? Permission { get; set; }
            public List<string>? Create { get; set; }
            public List<string>? Edit { get; set; }
            public List<string>? Setting { get; set; }
            public List<string>? Delete { get; set; }
            public List<string>? ViewTeam { get; set; }
            public List<string>? ViewProject { get; set; }
            public List<string>? CreateCalendar { get; set; }
            public List<string>? ViewCalendar { get; set; }
            public List<string>? OnlyInfo { get; set; }
            public List<string>? ViewProjectManage { get; set; }
            public List<string>? EditInfo { get; set; }
            public List<string>? EditTeam { get; set; }
            public List<string>? ReportTeam { get; set; }
            public List<string>? ReportInfo { get; set; }
            public List<string>? ReportProject { get; set; }
            public List<string>? ReportTargetvsAchieved { get; set; }
        }
    }
}
