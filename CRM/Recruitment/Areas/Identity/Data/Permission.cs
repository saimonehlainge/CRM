using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
    public class Permission : IProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("bossId")]
        public string? BossId { get; set; }

        [Column("userId")]
        public string? UserId { get; set; }

        [Column("departmentId")]
        public int? DepartmentId { get; set; }

        [Column("teamId")]
        public int? TeamId { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }

        [Column("deleteAt")] // สถานะการลบข้อมูล
        public int? DeleteAt { get; set; }
    }
}
