using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Recruitment.Areas.Identity.GeneralProperty;

namespace Recruitment.Areas.Identity.Data
{
    public class Company : IProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name", TypeName = "nvarchar(255)")]
        public string? Name { get; set; }

        [Column("status")] // สถานะข้อมูล 
        public int? Status { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }

        [Column("deleteAt")] // สถานะการลบข้อมูล
        public int? DeleteAt { get; set; }
    }
}
