using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recruitment.Areas.Identity.Data;
using Recruitment.Extensions;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Recruitment.Data;

public class RecruitmentContext : IdentityDbContext<RecruitmentUser, Role, string>
{
    public RecruitmentContext(DbContextOptions<RecruitmentContext> options)
        : base(options)
    {
    }

    public DbSet<CDD> CDD { get; set; }
    public DbSet<CDDCondition> CDDCondition { get; set; }
    public DbSet<Announcement> Announcement { get; set; }
    public DbSet<Position> Position { get; set; }
    public DbSet<Provinces> Provinces { get; set; }
    public DbSet<Districts> Districts { get; set; }
    public DbSet<Subdistricts> Subdistricts { get; set; }
    public DbSet<Prefix> Prefix { get; set; }
    public DbSet<Religion> Religion { get; set; }
    public DbSet<Nationality> Nationality { get; set; }
    public DbSet<MilitaryStatus> MilitaryStatus { get; set; }
    public DbSet<MarriedStatus> MarriedStatus { get; set; }
    public DbSet<Qualification> Qualification { get; set; }
    public DbSet<LanguageAbility> LanguageAbility { get; set; }
    public DbSet<HistoryCovid> HistoryCovid { get; set; }
    public DbSet<EmailSetting> EmailSetting { get; set; }
    public DbSet<Notification> Notification { get; set; }
    public DbSet<Calendar> Calendar { get; set; }
    public DbSet<Summary> Summary { get; set; }
    public DbSet<Permission> Permission { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<PermissionDetail> PermissionDetail { get; set; }
    public DbSet<PermissionSetting> PermissionSetting { get; set; }
    public DbSet<Team> Team { get; set; }
    public DbSet<CommentCDD> CommentCDD { get; set; }
    public DbSet<SetKpiComm> SetKpiComm { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<RecruitStatusFrom> RecruitStatusFrom { get; set; }
    public DbSet<RecruitStatus> RecruitStatus { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Seed();
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<CDD>()
            .HasMany(cd => cd.CDDCondition)
            .WithOne(d => d.CDD)
            .HasForeignKey(d => d.CDDId);

        builder.Entity<Provinces>()
            .HasMany(d => d.Districts)
            .WithOne(p => p.Provinces)
            .HasForeignKey(d => d.ProvinceId);

        builder.Entity<Districts>()
            .HasMany(s => s.Subdistricts)
            .WithOne(d => d.Districts)
            .HasForeignKey(d => d.DistrictId);
    }
}
