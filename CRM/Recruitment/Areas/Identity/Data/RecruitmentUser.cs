using Microsoft.AspNetCore.Identity;
using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recruitment.Areas.Identity.Data;

// Add profile data for application users by adding properties to the RecruitmentUser class
public class RecruitmentUser : IdentityUser, IProperty
{
    public RecruitmentUser() : base()
    {

    }

    [Column("firstname", TypeName = "nvarchar(255)")]
    public string? Firstname { get; set; }

    [Column("lastname", TypeName = "nvarchar(255)")]
    public string? Lastname { get; set; }

    [Column("address", TypeName = "nvarchar(255)")]
    public string? Address { get; set; }

    [Column("rawpassword", TypeName = "nvarchar(255)")]
    public string? RawPassword { get; set; }

    [Column("DepartmentId")]
    public int? DepartmentId { get; set; }

    [Column("Project")]
    public int? Project { get; set; }

    [Column("target_kpi")]
    public int? TargetKpi { get; set; }

    [Column("targetdate")]
    public DateTime? Targetdate { get; set; }

    [Column("todate")]
    public DateTime? Todate { get; set; }

    [Column("status_target")]
    public int? StatusTarget { get; set; }

    [Column("isactive")]
    public IsActive? IsActive { get; set; }

    [Column("createddate", TypeName = "datetimeoffset(7)")]
    public DateTimeOffset? CreatedDate { get; set; }

    [Column("updateddate", TypeName = "datetimeoffset(7)")]
    public DateTimeOffset? UpdatedDate { get; set; }

    [Column("deleteAt")] // สถานะการลบข้อมูล
    public int? DeleteAt { get; set; }
}

