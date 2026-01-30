using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
    public class CDDCondition : IProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("cdd_id")]
        public int? CDDId { get; set; }

        [Column("usedwork")]
        public int? Usedwork { get; set; } //อุปกรณ์ที่ใช้ในการทำงาน

        [Column("applicant_age")]
        public int? Applicant_age { get; set; } //ผู้สมัครมีอายุระหว่าง 18-40 ปี

        [Column("applicant_pregnant")]
        public int? Applicant_pregnant { get; set; } //ผู้สมัครอยู่ระหว่างตั้งครรภ์ หรือมีแผนการตั้งครรภ์ในระยะเวลาเร็วๆ นี้

        [Column("ever_applied")]
        public int? Ever_applied { get; set; } //ท่านเคยสมัครหรือทำงานที่ บริษัท ไบร์ทสเปซ หรือไม่

        [Column("studying")]
        public int? Studying { get; set; } //อยู่ในระหว่างกำลังศึกษาอยู่ หรือมีแผนที่จะเรียนต่อ ใช่หรือไม่ใช่

        [Column("defect")]
        public int? Defect { get; set; } //มีความบกพร่อง หรือพิการทางด้านร่างกาย ใช่หรือไม่ใช่

        [Column("current_degree")]
        public int? Current_degree { get; set; } //วุฒิปัจจุบันของท่านน้อยกว่า ม.6 ใช่หรือไม่

        [Column("under_pressure")]
        public int? Under_pressure { get; set; } //ผู้สมัครสามารถรับเเรงกดดันได้ดีในการทำงานที่มียอด และทำงานเลยเวลางานเลิกปกติได้ ถึง 20.00 น. ใช่หรือไม่ใช่

        [Column("government_work")]
        public int? Government_work { get; set; } //ผู้สมัครมีญาติพี่น้องที่รับราชการใช่ หรือ ไม่ใช่

        [Column("specific_qualifications")]
        public int? Specific_qualifications { get; set; } //ผู้สมัครมีวุฒิ ป.ตรี  คณะรัฐศาสตร์ คณะนิติศาสตร์  คณะศึกษาศาสตร์ ดังที่กล่าวมานี้ ใช่หรือไม่ใช่

        [Column("history_congenital_disease")]
        public int? History_congenital_disease { get; set; } //ท่านมีโรคประจำตัว ใช่หรือไม่ใช่

        [Column("congenital_disease")]
        public int? Congenital_disease { get; set; } //หากมีโรคประจำตัว

        [Column("congenitaldisease_detail", TypeName = "nvarchar(255)")] //โรคประจำตัว
        public string? congenitaldisease_detail { get; set; }

        [Column("wage")]
        public int? Wage { get; set; } //ค่าแรงวันละ 363/วัน ต่อเดือนเฉลี่ยประมาณ 9,438 ขึ้นอยู่กับจำนวณวันในแต่ล่ะเดือน และมีค่าคอมมิชชั่นขึ้นอยู่กับความสามารถ ซึ่งค่าคอมมิชชั่นจะออกทุกเดือนในรอบวันที่ 5

        [Column("social_security")]
        public int? Social_security { get; set; } //ทางบริษัทมีสวัสดิการเป็น ประกันสังคมให้ นับจากวันที่เซ็นสัญญาเป็นพนักงานประจำ

        [Column("work6stop1")]
        public int? Work6stop1 { get; set; } //ทำงาน 6 วันต่อสัปดาห์ วันหยุด 1 วัน จะหยุดพร้อมกันทั้งทีม ทางหัวหน้าทีมจะทำตารางแจ้งให้ทราบในกลุ่มทีม(วันหยุดอาจไม่ตรงกับเสาร์/อาทิตย์)

        [Column("deduction_wages")]
        public int? Deduction_wages { get; set; } //เป็นงานทำประจำแต่คิดค่าแรงเป็นรายวันจ่ายตามวันทำงานจริง การขาดงานไม่มีเหตุผลเพียงพอ บริษัทจะหักค่าแรง 2 เท่า

        [Column("deduction_wages50")]
        public int? Deduction_wages50 { get; set; } //ทำงาน 8.30 น. เลิกงาน 18.00 น. และถ้าหากทำยอดไม่ถึง 50% - 70 % ของยอดที่ได้รับมา ต้องทำงานต่อถึง 20.00 โดยเป็นความรับผิดชอบในการทำยอดจะไม่มีการจ่ายค่าโอทีเพิ่มให้

        [Column("numbercalls")]
        public int? Numbercalls { get; set; } //จำนวนการโทรต้องมากกว่าวันละ 280 รายชื่อ และมีระยะเวลาการคุยกับลูกค้ามากกว่า 2 ชม. ต่อวัน

        [Column("numbercalls15")]
        public int? Numbercalls15 { get; set; } //การโทรในเวลางานหากหยุดโทรติดต่อกันเกิน 15 นาทีหากมีการตรวจพบจะถูกเช็คขาดวันนั้นทันที

        [Column("missing_work")]
        public int? Missing_work { get; set; } //หากมีการขาดงานบ่อย หรือ ลางานบ่อย หรือ ทำยอดไม่ได้ติดต่อกันเป็นเวลาหลายวัน บริษัทอาจมีการพิจารณาให้ออก ทันที

        [Column("cost_work2")]
        public int? Cost_work2 { get; set; } //วันหยุดนักขัตฤกษ์ทางบริษัทจะมีการจ่ายค่าแรงเป็น 2 เท่าให้หากมีการตกลงให้เข้ามาทำงาน

        [Column("trend12")]
        public int? Trend12 { get; set; } //  ลงชื่อแล้วไม่สามารถเข้าเทรนตามรอบได้ทางบริษัทจะขออนุญาตตัดสิทธิ์ในการเข้าเทรน

        [Column("trend23")]
        public int? Trend23 { get; set; } //การเทรนงานอบรม แต่ละรอบจะทำการอบรม 2-3 วัน ผ่านโปรแกรม Zoom

        [Column("confirm_info")]
        public int? Confirm_info { get; set; } //ข้าพเจ้าในนามของ "ผู้สมัครงาน" มีบัตรประจำตัวประชาชนที่ออกโดยประเทศไทย

        [Column("nationality2")]
        public int? Nationality2 { get; set; } //ข้าพเจ้ายินยอมให้ตัดสิทธิ์ในการสมัครงานทันที

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }

        public CDD? CDD { get; set; }
    }
}
