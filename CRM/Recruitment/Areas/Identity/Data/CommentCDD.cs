using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Recruitment.Areas.Identity.GeneralProperty;

namespace Recruitment.Areas.Identity.Data
{
    public class CommentCDD : IProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("cdd_id")]
        public int? CDDId { get; set; }

        [Column("highlights", TypeName = "nvarchar(255)")]
        public string? Highlights { get; set; }

        [Column("observations", TypeName = "nvarchar(255)")]
        public string? Observations { get; set; }

        [Column("score", TypeName = "decimal(18, 2)")]
        public decimal? Score { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
