using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
    public class Subdistricts : IProperty
    {
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

        [Column("Latitude", TypeName = "decimal(6, 3)")]
        public decimal? Latitude { get; set; }

        [Column("Longitude", TypeName = "decimal(6, 3)")]
        public decimal? Longitude { get; set; }

        [Column("DistrictId")]
        public int DistrictId { get; set; }

        [Column("ZipCode")]
        public int? ZipCode { get; set; }

        [Column("status")] // สถานะข้อมูล 
        public int? Status { get; set; }

        [Column("deleteAt")] // สถานะการลบข้อมูล
        public int? DeleteAt { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }

        public Districts? Districts { get; set; }
    }
}
