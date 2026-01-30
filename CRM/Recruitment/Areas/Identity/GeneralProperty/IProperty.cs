namespace Recruitment.Areas.Identity.GeneralProperty
{
    public enum IsActive
    {
        ไม่ใช้งาน = 0,
        ใช้งาน = 1
    }

    public interface IProperty
    {
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
