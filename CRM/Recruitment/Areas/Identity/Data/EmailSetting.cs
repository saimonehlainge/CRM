using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data
{
	public class EmailSetting : IProperty
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public int Id { get; set; }

		[Column("email", TypeName = "nvarchar(255)")]
		public string? Email { get; set; } // Email

		[Column("createddate", TypeName = "datetimeoffset(7)")]
		public DateTimeOffset? CreatedDate { get; set; }

		[Column("updateddate", TypeName = "datetimeoffset(7)")]
		public DateTimeOffset? UpdatedDate { get; set; }

		[Column("deleteAt")] // สถานะการลบข้อมูล
		public int? DeleteAt { get; set; }
	}
}
