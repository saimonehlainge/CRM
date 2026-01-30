using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
	public class Notification : IProperty
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }
        [Column("type")]
        public int? Type { get; set; }

        [Column("message", TypeName = "nvarchar(255)")]
		public string? Message { get; set; }

		[Column("view")]
		public int? View { get; set; }

        [Column("cdd_id")]
        public int? CDDId { get; set; }

        [Column("userid", TypeName = "nvarchar(255)")]
        public string? userid { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
		public DateTimeOffset? CreatedDate { get; set; }

		[Column("updateddate", TypeName = "datetimeoffset(7)")]
		public DateTimeOffset? UpdatedDate { get; set; }
	}
}
