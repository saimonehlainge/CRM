using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
    public class Provinces : IProperty
    {
        public Provinces()
        {
            this.Districts = new HashSet<Districts>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Code")]
        public int? Code { get; set; }

        [Column("NameInThai", TypeName = "nvarchar(150)")]
        public string? NameInThai { get; set; }

        [Column("NameInEnglish", TypeName = "nvarchar(150)")]
        public string? NameInEnglish { get; set; }

        [Column("status")] // สถานะข้อมูล 
        public int? Status { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }

        [Column("deleteAt")] // สถานะการลบข้อมูล
        public int? DeleteAt { get; set; }

        public ICollection<Districts>? Districts { get; set; }
    }
}
