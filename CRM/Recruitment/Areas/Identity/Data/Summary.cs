using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
	public class Summary : IProperty
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		[Column("cdd_id")]
		public int? CDDId { get; set; }

		[Column("detail", TypeName = "nvarchar(255)")]
		public string? Detail { get; set; }

        [Column("oldchange")]
        public int? OldChange { get; set; }

        [Column("newchange")]
        public int? NewChange { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
		public DateTimeOffset? CreatedDate { get; set; }

		[Column("updateddate", TypeName = "datetimeoffset(7)")]
		public DateTimeOffset? UpdatedDate { get; set; }
	}
}
