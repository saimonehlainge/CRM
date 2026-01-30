using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Recruitment.Areas.Identity.Data
{
    public class PermissionSetting : IProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("userid", TypeName = "nvarchar(255)")]
        public string? UserId { get; set; }

        [Column("permission", TypeName = "nvarchar(255)")]
        public string? Permission { get; set; }

        [Column("create")]
        public bool? create { get; set; }

        [Column("edit")]
        public bool? edit { get; set; }

        [Column("setting")]
        public bool? setting { get; set; }

        [Column("delete")]
        public bool? delete { get; set; }

        [Column("viewteam")]
        public bool? viewteam { get; set; }

        [Column("viewproject")]
        public bool? viewproject { get; set; }

        [Column("createcalendar")]
        public bool? createcalendar { get; set; }

        [Column("viewcalendar")]
        public bool? viewcalendar { get; set; }

        [Column("onlyinfo")]
        public bool? onlyinfo { get; set; }

        [Column("viewprojectmanage")]
        public bool? viewprojectmanage { get; set; }

        [Column("editinfo")]
        public bool? editinfo { get; set; }

        [Column("editteam")]
        public bool? editteam { get; set; }

        [Column("reportteam")]
        public bool? reportteam { get; set; }

        [Column("reportinfo")]
        public bool? reportinfo { get; set; }

        [Column("reportproject")]
        public bool? reportproject { get; set; }

        [Column("reporttargetvsachieved")]
        public bool? reporttargetvsachieved { get; set; }

        [Column("createddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column("updateddate", TypeName = "datetimeoffset(7)")]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
