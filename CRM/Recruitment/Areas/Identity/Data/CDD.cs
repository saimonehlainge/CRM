using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
    public class CDD : IProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("userid", TypeName = "nvarchar(MAX)")]
        public string? UserId { get; set; }

        [Column("billingcycle", TypeName = "nvarchar(MAX)")]
        public string? BillingCycle { get; set; } // ตัดรอบบิล

        [Column("type_cdd")] // ประเภทใบสมัครงาน
        public int? TypeCdd { get; set; }

        [Column("project")] // โปรเจคkpi
        public int? Project { get; set; }

        [Column("company")] // บริษัทที่ต้องการสมัคร
        public int? Company { get; set; }

        [Column("announcement")] // เห็นประกาศช่องทางใด
        public int? Announcement { get; set; }

        [Column("position")] //ตำแหน่ง
        public int? Position { get; set; }

        [Column("hr_contact", TypeName = "nvarchar(MAX)")]
        public string? HRContact { get; set; }

        [Column("branch", TypeName = "nvarchar(MAX)")] // สาขา/ออฟฟิศที่ทำงาน
        public string? Branch { get; set; }

        [Column("salary", TypeName = "decimal(18, 2)")] // เงินเดือน
        public decimal? Salary { get; set; }

        [Column("workarea")] // ต้องการงานในพื้นที่
        public int? WorkArea { get; set; }

        [Column("prefix")] // คำนำหน้า
        public int? Prefix { get; set; }

        [Column("nameth", TypeName = "nvarchar(MAX)")] // ชื่อ-นามสกุล ไทย
        public string? NameTH { get; set; }

        [Column("nameeng", TypeName = "nvarchar(MAX)")] // ชื่อ-นามสกุล อังกฤษ
        public string? NameENG { get; set; }

        [Column("nikname", TypeName = "nvarchar(MAX)")] // ชื่อเล่น
        public string? Nikname { get; set; }

        [Column("tel", TypeName = "nvarchar(MAX)")] // เบอร์โทรศัพท์
        public string? Tel { get; set; }

        [Column("lineid", TypeName = "nvarchar(MAX)")] // ไลน์ไอดี
        public string? LineId { get; set; }

        [Column("email", TypeName = "nvarchar(MAX)")] // อีเมล
        public string? Email { get; set; }

        [Column("birthday")] // วันเกิด
        public DateTimeOffset? Birthday { get; set; }

        [Column("religion", TypeName = "nvarchar(MAX)")] // ศาสนา
        public int? Religion { get; set; }

        [Column("nationality", TypeName = "nvarchar(MAX)")] // สัญชาติ
        public int? Nationality { get; set; }

        [Column("idcard", TypeName = "nvarchar(MAX)")] // เลขบัตรประชาชน
        public string? IDCard { get; set; }

        [Column("enddateidcard")] // วันหมดอายุบัตรประชาชน
        public DateTimeOffset? EndDateIDCard { get; set; }

        [Column("militarstatus")] // สถานะทางทหาร
        public int? MilitaryStatus { get; set; }

        [Column("marriedstatus")] // สถานะสมรส
        public int? MarriedStatus { get; set; }

        [Column("idcardaddress", TypeName = "nvarchar(MAX)")] // ที่อยู่ตามเลขบัตรประชาชน
        public string? IdCardAddress { get; set; }

        [Column("currentaddress", TypeName = "nvarchar(MAX)")] // ที่อยู่ปัจจุบัน
        public string? CurrentAddress { get; set; }

        [Column("qualification")] // วุฒิ
        public int? Qualification { get; set; }

        [Column("studylocation", TypeName = "nvarchar(MAX)")] // ชื่อสถานที่ศึกษา
        public string? StudyLocation { get; set; }

        [Column("companyname", TypeName = "nvarchar(MAX)")] // ชื่อบริษัท
        public string? ComapnyName { get; set; }

        [Column("start_end_work", TypeName = "nvarchar(MAX)")] // เดือนและปีที่เริ่มและสิ้นสุด
        public string? StartEndWork { get; set; }

        [Column("workposition", TypeName = "nvarchar(MAX)")] // ตำแหน่งงาน
        public string? WorkPosition { get; set; }

        [Column("worksalary", TypeName = "nvarchar(MAX)")] // เงินเดือน
        public string? WorkSalary { get; set; }

        [Column("notework", TypeName = "nvarchar(MAX)")] // เงินเดือน
        public string? NoteWork { get; set; }

        [Column("languageability")] // ความสามารถภาษา
        public int? LanguageAbility { get; set; }

        [Column("languageAbilityOther", TypeName = "nvarchar(MAX)")] // ความสามารถภาษาอื่นๆ
        public string? LanguageAbilityOther { get; set; }

        [Column("otherspecialability", TypeName = "nvarchar(MAX)")] // ความสามารถพิเศษ
        public string? OtherSpecialAbility { get; set; }

        [Column("workOtheprovinces")] // สามารถไปปฎิบัติงานต่างจังหวัด
        public int? WorkOtheProvinces { get; set; }

        [Column("historyCovidVaccine")] // ประวัติวัคซีนโควิด
        public int? HistoryCovidVaccine { get; set; }

        [Column("whocancheck_name", TypeName = "nvarchar(MAX)")]
        public string? WhoCanCheckName { get; set; } // ชื่อผู้ติดต่อ

        [Column("whocancheck_tel", TypeName = "nvarchar(MAX)")]
        public string? WhoCanCheckTel { get; set; } // เบอร์ผู้ติดต่อ

        [Column("whocancheck_address", TypeName = "nvarchar(MAX)")]
        public string? WhoCanCheckAdress { get; set; } // ที่อยู่ผู้ติดต่อ

        [Column("whocancheck_related", TypeName = "nvarchar(MAX)")]
        public string? WhoCanCheckRelated { get; set; } // เกี่ยวข้องกับผู้ติดต่อ

        [Column("emergency_name", TypeName = "nvarchar(MAX)")]
        public string? EmergencyName { get; set; } // ชื่อผู้ติดต่อฉุกเฉิน

        [Column("emergency_tel", TypeName = "nvarchar(MAX)")]
        public string? EmergencyTel { get; set; } // เบอร์ผู้ติดต่อฉุกเฉิน

        [Column("emergency_address", TypeName = "nvarchar(MAX)")]
        public string? EmergencyAdress { get; set; } // ที่อยู่ผู้ติดต่อฉุกเฉิน

        [Column("emergency_related", TypeName = "nvarchar(MAX)")]
        public string? EmergencyRelated { get; set; } // เกี่ยวข้องผู้ติดต่อฉุกเฉิน

        [Column("documentCDD", TypeName = "nvarchar(MAX)")] // เอกสารประกอบการสมัครงาน
        public string? DocumentCDD { get; set; }

        [Column("documentWorkCertification", TypeName = "nvarchar(MAX)")] // เอกสารรับรองการทำงาน
        public string? DocumentWorkCertification { get; set; }

        [Column("documentBank", TypeName = "nvarchar(MAX)")] // เอกสารธนาคาร
        public string? DocumentBank { get; set; }

        [Column("documentHistory", TypeName = "nvarchar(MAX)")] // เอกสารธนาคาร
        public string? DocumentHistroy { get; set; }

        [Column("documentJng", TypeName = "nvarchar(MAX)")] // เอกสารธนาคาร
        public string? DocumentJng { get; set; }

        [Column("kpi", TypeName = "nvarchar(MAX)")] // KPI
        public string? KPI { get; set; }

        [Column("status")] // สถานะข้อมูล 
        public int? Status { get; set; }

        [Column("status_start")] //วันที่เปลื่ยน สถานะ
        public DateTime? StatusStart { get; set; }

        [Column("status_end")] // ถึงวีนที่เปลื่ยน สถานะ
        public DateTime? StatusEnd { get; set; }

        [Column("notestatus", TypeName = "nvarchar(MAX)")] // หมายเหตุ เปลื่ยน สถานะข้อมูล 
        public string? NoteStatus { get; set; }

        [Column("cause", TypeName = "nvarchar(MAX)")] //สาเหตุ
        public string? Cause { get; set; }

        [Column("datestatus")] // ถึงวีนที่เปลื่ยน สถานะ
        public DateTime? Datestatus { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }

        [Column("deleteAt")] // สถานะการลบข้อมูล
        public int? DeleteAt { get; set; }

        public ICollection<CDDCondition>? CDDCondition { get; set; }
    }
}
