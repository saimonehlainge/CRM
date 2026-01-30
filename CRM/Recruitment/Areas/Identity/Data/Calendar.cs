using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
    public class Calendar : IProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("userid_create", TypeName = "nvarchar(255)")]
        public string? UseridCreate { get; set; }

        [Column("userid", TypeName = "nvarchar(255)")]
        public string? Userid { get; set; }

        [Column("detail", TypeName = "nvarchar(255)")]
        public string? Detail { get; set; }

        [Column("description", TypeName = "nvarchar(255)")]
        public string? Description { get; set; }

        [Column("startdate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? StartDate { get; set; }

        [Column("enddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? EndDate { get; set; }
        [Column("status")]
        public int? Status { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
