using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
    public class SetKpiComm : IProperty // วันเก็บเงิน kpi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("project")]
        public int? Project { get; set; }

        [Column("setkpi_day")]
        public int? SetKpi_Day { get; set; }

        [Column("guarantee_day")]
        public int? GuaranteeDay { get; set; }

        [Column("comm", TypeName = "decimal(18, 2)")]
        public decimal? Comm { get; set; }

        [Column("kpi", TypeName = "decimal(18, 2)")]
        public decimal? Kpi { get; set; }

        [Column("rank")]
        public int? Rank { get; set; }

        [Column("status")]
        public int? Status { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }

        [Column("deleteAt")] // สถานะการลบข้อมูล
        public int? DeleteAt { get; set; }
    }
}
