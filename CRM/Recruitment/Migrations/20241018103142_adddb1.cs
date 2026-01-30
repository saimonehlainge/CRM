using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Recruitment.Migrations
{
    /// <inheritdoc />
    public partial class adddb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    lastname = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    rawpassword = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    isactive = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    detail = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    startdate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    enddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CDD",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billingcycle = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    type_cdd = table.Column<int>(type: "int", nullable: true),
                    project = table.Column<int>(type: "int", nullable: true),
                    company = table.Column<int>(type: "int", nullable: true),
                    announcement = table.Column<int>(type: "int", nullable: true),
                    position = table.Column<int>(type: "int", nullable: true),
                    hr_contact = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    branch = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    workarea = table.Column<int>(type: "int", nullable: true),
                    prefix = table.Column<int>(type: "int", nullable: true),
                    nameth = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    nameeng = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    nikname = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    tel = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    lineid = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    birthday = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    religion = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    nationality = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    idcard = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    enddateidcard = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    militarstatus = table.Column<int>(type: "int", nullable: true),
                    marriedstatus = table.Column<int>(type: "int", nullable: true),
                    idcardaddress = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    currentaddress = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    qualification = table.Column<int>(type: "int", nullable: true),
                    studylocation = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    companyname = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    start_end_work = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    workposition = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    worksalary = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    notework = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    languageability = table.Column<int>(type: "int", nullable: true),
                    languageAbilityOther = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    otherspecialability = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    workOtheprovinces = table.Column<int>(type: "int", nullable: true),
                    historyCovidVaccine = table.Column<int>(type: "int", nullable: true),
                    whocancheck_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whocancheck_tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whocancheck_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    whocancheck_related = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emergency_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emergency_tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emergency_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emergency_related = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    documentCDD = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    documentWorkCertification = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    documentBank = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    documentHistory = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    documentJng = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDD", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EmailSetting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSetting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryCovid",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryCovid", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageAbility",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageAbility", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MarriedStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarriedStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Nationality",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationality", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    view = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bossid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    departmentId = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionSetting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    permissionMenu = table.Column<int>(type: "int", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSetting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    typeform = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Prefix",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefix", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    NameInThai = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RecruitStatus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitStatus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Religion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Summary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cdd_id = table.Column<int>(type: "int", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    oldchange = table.Column<int>(type: "int", nullable: true),
                    newchange = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOtherProvinces",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOtherProvinces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CDDCondition",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cdd_id = table.Column<int>(type: "int", nullable: true),
                    usedwork = table.Column<int>(type: "int", nullable: true),
                    applicant_age = table.Column<int>(type: "int", nullable: true),
                    applicant_pregnant = table.Column<int>(type: "int", nullable: true),
                    ever_applied = table.Column<int>(type: "int", nullable: true),
                    studying = table.Column<int>(type: "int", nullable: true),
                    defect = table.Column<int>(type: "int", nullable: true),
                    current_degree = table.Column<int>(type: "int", nullable: true),
                    under_pressure = table.Column<int>(type: "int", nullable: true),
                    government_work = table.Column<int>(type: "int", nullable: true),
                    specific_qualifications = table.Column<int>(type: "int", nullable: true),
                    history_congenital_disease = table.Column<int>(type: "int", nullable: true),
                    congenital_disease = table.Column<int>(type: "int", nullable: true),
                    wage = table.Column<int>(type: "int", nullable: true),
                    social_security = table.Column<int>(type: "int", nullable: true),
                    work6stop1 = table.Column<int>(type: "int", nullable: true),
                    deduction_wages = table.Column<int>(type: "int", nullable: true),
                    deduction_wages50 = table.Column<int>(type: "int", nullable: true),
                    numbercalls = table.Column<int>(type: "int", nullable: true),
                    numbercalls15 = table.Column<int>(type: "int", nullable: true),
                    missing_work = table.Column<int>(type: "int", nullable: true),
                    cost_work2 = table.Column<int>(type: "int", nullable: true),
                    trend12 = table.Column<int>(type: "int", nullable: true),
                    trend23 = table.Column<int>(type: "int", nullable: true),
                    confirm_info = table.Column<int>(type: "int", nullable: true),
                    nationality2 = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDDCondition", x => x.id);
                    table.ForeignKey(
                        name: "FK_CDDCondition_CDD_cdd_id",
                        column: x => x.cdd_id,
                        principalTable: "CDD",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    NameInThai = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subdistricts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    NameInThai = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(6,3)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(6,3)", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    deleteAt = table.Column<int>(type: "int", nullable: true),
                    createddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true),
                    updateddate = table.Column<DateTimeOffset>(type: "datetimeoffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subdistricts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subdistricts_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Announcement",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7186), new TimeSpan(0, 7, 0, 0, 0)), null, "Facebook", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7201), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7208), new TimeSpan(0, 7, 0, 0, 0)), null, "Line", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7210), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7213), new TimeSpan(0, 7, 0, 0, 0)), null, "JobThaiweb", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7216), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7219), new TimeSpan(0, 7, 0, 0, 0)), null, "Jobfinfin", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7221), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7224), new TimeSpan(0, 7, 0, 0, 0)), null, "Facebook Group", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7228), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 6, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7243), new TimeSpan(0, 7, 0, 0, 0)), null, "โทรศัพท์", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7245), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 7, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7248), new TimeSpan(0, 7, 0, 0, 0)), null, "Line @", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7249), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 8, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7252), new TimeSpan(0, 7, 0, 0, 0)), null, "Facebook Page", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7254), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 9, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7257), new TimeSpan(0, 7, 0, 0, 0)), null, "Friend get Friend", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7259), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 10, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7264), new TimeSpan(0, 7, 0, 0, 0)), null, "80 List", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7266), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 11, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7269), new TimeSpan(0, 7, 0, 0, 0)), null, "JobBKK", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7272), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 12, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7275), new TimeSpan(0, 7, 0, 0, 0)), null, "JobPub", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7276), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 13, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7279), new TimeSpan(0, 7, 0, 0, 0)), null, "JobThai", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7281), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 14, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7284), new TimeSpan(0, 7, 0, 0, 0)), null, "อื่นๆ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7317), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "deleteAt", "Name", "NormalizedName", "UpdatedDate" },
                values: new object[,]
                {
                    { "2eba34d3-8c87-46ff-a570-66618b782ca4", null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7686), new TimeSpan(0, 7, 0, 0, 0)), null, "manager", "MANAGER", new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7688), new TimeSpan(0, 7, 0, 0, 0)) },
                    { "6829e1d3-8a24-4e11-8a72-8087fd268c55", null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7701), new TimeSpan(0, 7, 0, 0, 0)), null, "project owner", "PROJECT OWNER", new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7702), new TimeSpan(0, 7, 0, 0, 0)) },
                    { "72d38ca5-9687-4d64-8fac-f6a4c08d161c", null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7675), new TimeSpan(0, 7, 0, 0, 0)), null, "md", "MD", new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7677), new TimeSpan(0, 7, 0, 0, 0)) },
                    { "772cf8fe-3b96-4a5a-90d2-8847f5fba527", null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7692), new TimeSpan(0, 7, 0, 0, 0)), null, "sup", "SUP", new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7694), new TimeSpan(0, 7, 0, 0, 0)) },
                    { "a8c19066-41e4-4397-b060-b60715f7d237", null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7713), new TimeSpan(0, 7, 0, 0, 0)), null, "recruiter", "RECRUITER", new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7714), new TimeSpan(0, 7, 0, 0, 0)) },
                    { "d5c41079-16c6-40af-95dc-752ace0e82a2", null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7582), new TimeSpan(0, 7, 0, 0, 0)), null, "admin", "ADMIN", new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 497, DateTimeKind.Unspecified).AddTicks(7639), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "address", "ConcurrencyStamp", "createddate", "deleteAt", "Email", "EmailConfirmed", "firstname", "isactive", "lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "rawpassword", "SecurityStamp", "TwoFactorEnabled", "updateddate", "UserName" },
                values: new object[] { "ea863b8a-216a-49ff-b63a-75f33c9d4b45", 0, null, "c3646da8-aeca-4c49-a882-ce5290d71924", new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(6254), new TimeSpan(0, 7, 0, 0, 0)), null, "admin@recruitment.com", false, "admin", null, "admin", false, null, "ADMIN@RECRUITMENT.COM", "ADMIN@RECRUITMENT.COM", "AQAAAAIAAYagAAAAELYBqobKB60Nb94WM5yAwqJj58FgXV1hEQeaBSv1+6oGQTNrFjm878WIo5FR+ttQqw==", null, false, null, "16c39056-4290-4a1b-9e6b-bd38df9d1edb", false, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(6313), new TimeSpan(0, 7, 0, 0, 0)), "admin@recruitment.com" });

            migrationBuilder.InsertData(
                table: "HistoryCovid",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9106), new TimeSpan(0, 7, 0, 0, 0)), null, "1 เข็ม", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9112), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9118), new TimeSpan(0, 7, 0, 0, 0)), null, "2 เข็ม", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9120), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9124), new TimeSpan(0, 7, 0, 0, 0)), null, "3 เข็ม", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9126), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9129), new TimeSpan(0, 7, 0, 0, 0)), null, "ยังไม่ได้รับวัคซีน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9132), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9135), new TimeSpan(0, 7, 0, 0, 0)), null, "อื่นๆ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9137), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "LanguageAbility",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8959), new TimeSpan(0, 7, 0, 0, 0)), null, "ภาษาอังกฤษ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8964), new TimeSpan(0, 7, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "MarriedStatus",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8692), new TimeSpan(0, 7, 0, 0, 0)), null, "โสด", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8697), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8702), new TimeSpan(0, 7, 0, 0, 0)), null, "สมรส", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8704), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8707), new TimeSpan(0, 7, 0, 0, 0)), null, "หย่าร้าง", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8709), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8713), new TimeSpan(0, 7, 0, 0, 0)), null, "หม้าย", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8716), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8719), new TimeSpan(0, 7, 0, 0, 0)), null, "แยกกัน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8721), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "MilitaryStatus",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8602), new TimeSpan(0, 7, 0, 0, 0)), null, "ยังไม่เกณฑ์ทหาร", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8606), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8612), new TimeSpan(0, 7, 0, 0, 0)), null, "เกณฑ์ทหารแล้ว", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8614), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Nationality",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8533), new TimeSpan(0, 7, 0, 0, 0)), null, "ไทย", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8538), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8543), new TimeSpan(0, 7, 0, 0, 0)), null, "อิ่นๆ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8545), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "PermissionDetail",
                columns: new[] { "Id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1335), new TimeSpan(0, 7, 0, 0, 0)), null, "สร้างข้อมูล", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1371), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1375), new TimeSpan(0, 7, 0, 0, 0)), null, "แก้ไขข้อมูล Masterdate List", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1376), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1381), new TimeSpan(0, 7, 0, 0, 0)), null, "ตั้งค่าระบบ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1382), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1385), new TimeSpan(0, 7, 0, 0, 0)), null, "ลบข้อมูล", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1386), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1388), new TimeSpan(0, 7, 0, 0, 0)), null, "เห็นข้อมูลของทีม", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1390), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 6, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1394), new TimeSpan(0, 7, 0, 0, 0)), null, "เห็นข้อมูล Project ทั้งหมด", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1395), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 7, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1397), new TimeSpan(0, 7, 0, 0, 0)), null, "สร้างข้อมูลในปฎิทิน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1399), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 8, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1401), new TimeSpan(0, 7, 0, 0, 0)), null, "เห็นข้อมูลในปฎิทิน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1403), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 9, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1405), new TimeSpan(0, 7, 0, 0, 0)), null, "เห็นเฉพาะข้อมูลตนเอง", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1406), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 10, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1409), new TimeSpan(0, 7, 0, 0, 0)), null, "เห็นเฉพาะข้อมูล Project ที่ตัวเองดูแล", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1411), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 11, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1413), new TimeSpan(0, 7, 0, 0, 0)), null, "แก้ไขเฉพาะข้อมูลตนเอง", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1415), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 12, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1417), new TimeSpan(0, 7, 0, 0, 0)), null, "แก้ไขข้อมูลของทีม", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1419), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 13, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1421), new TimeSpan(0, 7, 0, 0, 0)), null, "ดึงรายงานข้อมูลของทีม", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1423), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 14, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1425), new TimeSpan(0, 7, 0, 0, 0)), null, "ดึงรายงานข้อมูลตนเอง", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1426), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 15, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1429), new TimeSpan(0, 7, 0, 0, 0)), null, "ดึงรายงานข้อมูลของ Project", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1430), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 16, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1432), new TimeSpan(0, 7, 0, 0, 0)), null, "ดึงรายงานข้อมูล Target Vs Achieved", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 600, DateTimeKind.Unspecified).AddTicks(1434), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "typeform", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(7997), new TimeSpan(0, 7, 0, 0, 0)), null, "TSR", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8012), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8020), new TimeSpan(0, 7, 0, 0, 0)), null, "TSR Collecter", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8022), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8026), new TimeSpan(0, 7, 0, 0, 0)), null, "TSR Executive", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8028), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8031), new TimeSpan(0, 7, 0, 0, 0)), null, "HR Recruit", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8032), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8035), new TimeSpan(0, 7, 0, 0, 0)), null, "Leader", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8037), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 6, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8041), new TimeSpan(0, 7, 0, 0, 0)), null, "Ac", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8043), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 7, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8046), new TimeSpan(0, 7, 0, 0, 0)), null, "TVD-อาหารเสริม", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8048), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 8, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8050), new TimeSpan(0, 7, 0, 0, 0)), null, "Call Center-AIA", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8052), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 9, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8055), new TimeSpan(0, 7, 0, 0, 0)), null, "Call Service-AIA", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8057), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 10, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8061), new TimeSpan(0, 7, 0, 0, 0)), null, "Reinstatement", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8063), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 11, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8067), new TimeSpan(0, 7, 0, 0, 0)), null, "Direct Sales", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8069), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 12, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8072), new TimeSpan(0, 7, 0, 0, 0)), null, "UBO Leader", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8073), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 13, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8076), new TimeSpan(0, 7, 0, 0, 0)), null, "Supervisor", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8078), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 14, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8081), new TimeSpan(0, 7, 0, 0, 0)), null, "Truck Driver", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8083), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 15, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8087), new TimeSpan(0, 7, 0, 0, 0)), null, "Sales Executive", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8089), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 16, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8092), new TimeSpan(0, 7, 0, 0, 0)), null, "Administrative", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8093), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 17, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8096), new TimeSpan(0, 7, 0, 0, 0)), null, "Accounting", 1, null, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8099), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Prefix",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8327), new TimeSpan(0, 7, 0, 0, 0)), null, "นาย", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8334), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8340), new TimeSpan(0, 7, 0, 0, 0)), null, "นางสาว", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8341), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8344), new TimeSpan(0, 7, 0, 0, 0)), null, "นาง", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8345), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8348), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่ระบุ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8350), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9209), new TimeSpan(0, 7, 0, 0, 0)), null, "Project", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9213), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9218), new TimeSpan(0, 7, 0, 0, 0)), null, "AIA", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9221), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9225), new TimeSpan(0, 7, 0, 0, 0)), null, "AZAY", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9227), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9231), new TimeSpan(0, 7, 0, 0, 0)), null, "TCIB", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9233), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9235), new TimeSpan(0, 7, 0, 0, 0)), null, "TVDxAIA", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9237), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 6, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9241), new TimeSpan(0, 7, 0, 0, 0)), null, "TVD", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9243), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 7, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9246), new TimeSpan(0, 7, 0, 0, 0)), null, "Cressi", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9248), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 8, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9251), new TimeSpan(0, 7, 0, 0, 0)), null, "BSP", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9253), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 9, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9256), new TimeSpan(0, 7, 0, 0, 0)), null, "BSP(HR)", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9258), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 10, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9262), new TimeSpan(0, 7, 0, 0, 0)), null, "BSP(MGR)", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9264), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 11, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9267), new TimeSpan(0, 7, 0, 0, 0)), null, "R89 RC", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9268), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 12, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9271), new TimeSpan(0, 7, 0, 0, 0)), null, "AC1", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9273), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 13, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9276), new TimeSpan(0, 7, 0, 0, 0)), null, "Muvmii", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9279), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 14, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9281), new TimeSpan(0, 7, 0, 0, 0)), null, "Lazada", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9283), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 15, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9286), new TimeSpan(0, 7, 0, 0, 0)), null, "Sale Marketing", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9288), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 16, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9291), new TimeSpan(0, 7, 0, 0, 0)), null, "AWC", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9293), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 17, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9297), new TimeSpan(0, 7, 0, 0, 0)), null, "Next Job", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(9299), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Qualification",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8796), new TimeSpan(0, 7, 0, 0, 0)), null, "มัธยมต้น", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8800), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8806), new TimeSpan(0, 7, 0, 0, 0)), null, "มัธยมปลาย", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8808), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8812), new TimeSpan(0, 7, 0, 0, 0)), null, "ป.ตรี", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8813), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8816), new TimeSpan(0, 7, 0, 0, 0)), null, "ป.โท", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8818), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8822), new TimeSpan(0, 7, 0, 0, 0)), null, "ป.เอก", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8824), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "RecruitStatus",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3147), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่สนใจ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3191), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3195), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่ผ่านคุณสมบัติ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3196), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3199), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่ตอบ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3200), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3203), new TimeSpan(0, 7, 0, 0, 0)), null, "นัดสัมภาษณ์แล้ว", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3204), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 5, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3207), new TimeSpan(0, 7, 0, 0, 0)), null, "เพื่อติดตาม", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3208), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 6, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3211), new TimeSpan(0, 7, 0, 0, 0)), null, "รอสัมภาษณ์_ลูกค้า", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3213), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 7, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3215), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่ไปตามนัด_ลูกค้า", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3217), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 8, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3219), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่ผ่านสัมภาษณ์_ลูกค้า", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3221), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 9, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3223), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่ผ่านสัมภาษณ์_ลูกค้า2", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3224), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 10, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3227), new TimeSpan(0, 7, 0, 0, 0)), null, "รอผลสัมภาษณ์_ลูกค้า", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3229), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 11, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3231), new TimeSpan(0, 7, 0, 0, 0)), null, "รอเริ่มงาน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3233), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 12, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3235), new TimeSpan(0, 7, 0, 0, 0)), null, "ผู้สมัคร_นัดแล้วไม่มา", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3236), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 13, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3238), new TimeSpan(0, 7, 0, 0, 0)), null, "ผู้สมัคร_ยกเลิก (แจ้งยกเลิก)", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3239), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 14, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3242), new TimeSpan(0, 7, 0, 0, 0)), null, "ผู้สมัคร_สอบใบอนุญาตไม่ผ่าน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3243), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 15, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3245), new TimeSpan(0, 7, 0, 0, 0)), null, "ผู้สมัคร_ไปเริ่มงานแล้วไม่ชอบ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3246), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 16, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3248), new TimeSpan(0, 7, 0, 0, 0)), null, "ผู้สมัคร_ไม่ผ่านเทรนงาน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3250), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 17, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3252), new TimeSpan(0, 7, 0, 0, 0)), null, "ผู้สมัคร_ไม่ผ่านการสอบ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3253), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 18, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3256), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่สนใจ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3258), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 19, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3260), new TimeSpan(0, 7, 0, 0, 0)), null, "ลูกค้า_ผิดเงื่อนไข", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3262), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 20, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3264), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่ไปเริ่มงาน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3265), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 21, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3268), new TimeSpan(0, 7, 0, 0, 0)), null, "ลาออกก่อนวางบิล", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3269), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 22, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3271), new TimeSpan(0, 7, 0, 0, 0)), null, "เริ่มงานแล้วรอวางบิล", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3273), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 23, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3276), new TimeSpan(0, 7, 0, 0, 0)), null, "ติดตามให้พ้นการันตี", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3277), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 24, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3279), new TimeSpan(0, 7, 0, 0, 0)), null, "KPI_0.5ครั้งที่#1", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3281), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 25, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3283), new TimeSpan(0, 7, 0, 0, 0)), null, "KPI_0.5ครั้งที่#2", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3284), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 26, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3287), new TimeSpan(0, 7, 0, 0, 0)), null, "KPI_1.0", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3289), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 27, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3291), new TimeSpan(0, 7, 0, 0, 0)), null, "KPI_ตามตำแหน่งงาน", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3292), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 28, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3294), new TimeSpan(0, 7, 0, 0, 0)), null, "ค่าคอมเท่านั้น", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3295), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 29, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3298), new TimeSpan(0, 7, 0, 0, 0)), null, "ค่าคอมคนสอนน้องใหม่", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3299), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 30, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3301), new TimeSpan(0, 7, 0, 0, 0)), null, "ค่าคอมช่วยเหลือน้องใหม่", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3303), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 31, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3306), new TimeSpan(0, 7, 0, 0, 0)), null, "Replace", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3307), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 32, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3309), new TimeSpan(0, 7, 0, 0, 0)), null, "ครบการันตี", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 597, DateTimeKind.Unspecified).AddTicks(3311), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Religion",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8439), new TimeSpan(0, 7, 0, 0, 0)), null, "ศาสนาพุทธ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8444), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8449), new TimeSpan(0, 7, 0, 0, 0)), null, "ศาสนาคริสต์", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8451), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8454), new TimeSpan(0, 7, 0, 0, 0)), null, "ศาสนาอิสลาม", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8456), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 4, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8459), new TimeSpan(0, 7, 0, 0, 0)), null, "อิ่นๆ", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 594, DateTimeKind.Unspecified).AddTicks(8462), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "WorkOtherProvinces",
                columns: new[] { "id", "createddate", "deleteAt", "name", "status", "updateddate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 598, DateTimeKind.Unspecified).AddTicks(7590), new TimeSpan(0, 7, 0, 0, 0)), null, "ได้", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 598, DateTimeKind.Unspecified).AddTicks(7629), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 598, DateTimeKind.Unspecified).AddTicks(7633), new TimeSpan(0, 7, 0, 0, 0)), null, "ไม่ได้", 1, new DateTimeOffset(new DateTime(2024, 10, 18, 17, 31, 39, 598, DateTimeKind.Unspecified).AddTicks(7635), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d5c41079-16c6-40af-95dc-752ace0e82a2", "ea863b8a-216a-49ff-b63a-75f33c9d4b45" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CDDCondition_cdd_id",
                table: "CDDCondition",
                column: "cdd_id");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Subdistricts_DistrictId",
                table: "Subdistricts",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcement");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "CDDCondition");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "EmailSetting");

            migrationBuilder.DropTable(
                name: "HistoryCovid");

            migrationBuilder.DropTable(
                name: "LanguageAbility");

            migrationBuilder.DropTable(
                name: "MarriedStatus");

            migrationBuilder.DropTable(
                name: "MilitaryStatus");

            migrationBuilder.DropTable(
                name: "Nationality");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "PermissionDetail");

            migrationBuilder.DropTable(
                name: "PermissionSetting");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Prefix");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "RecruitStatus");

            migrationBuilder.DropTable(
                name: "Religion");

            migrationBuilder.DropTable(
                name: "Subdistricts");

            migrationBuilder.DropTable(
                name: "Summary");

            migrationBuilder.DropTable(
                name: "WorkOtherProvinces");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CDD");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
