using Microsoft.AspNetCore.Identity;
using Recruitment.Areas.Identity.GeneralProperty;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
namespace Recruitment.Areas.Identity.Data
{
    public class Role : IdentityRole, IProperty
    {
        public Role() { }

        public string? Name { get; set; }
        
        public DateTimeOffset? CreatedDate { get; set; }
        
        public DateTimeOffset? UpdatedDate { get; set; }

        [Column("deleteAt")] // สถานะการลบข้อมูล
        public int? DeleteAt { get; set; }
    }
}
