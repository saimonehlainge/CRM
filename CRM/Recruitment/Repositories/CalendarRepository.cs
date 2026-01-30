using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Models;

namespace Recruitment.Repositories
{
    public interface ICalendarRepository : IGenericRepository<Calendar>
    {
        Task InsertCalendar(List<RequestDTO.CalendarRequset> request);
    }

    public class CalendarRepository : GenericRepository<Calendar>, ICalendarRepository
    {
        public CalendarRepository(RecruitmentContext context) : base(context)
        { 
        
        }

        public async Task InsertCalendar(List<RequestDTO.CalendarRequset> request)
        {
            
            List<Calendar> calendar = new List<Calendar>();
            List<Notification> notification = new List<Notification>();

            foreach (var item in request)
            {
                var ss = 0;
                if (item.userid != null)
                {
                    for (var i = 0; i < item.userid.Count; i++)
                    {
                        calendar.Add(new Calendar
                        {
                            Userid = item.userid[ss],
                            UseridCreate = item.userid_create,
                            Detail = item.Detail,
                            Description = item.Description,
                            StartDate = item.StartDate,
                            EndDate = item.EndDate,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now
                        });

                        notification.Add(new Notification
                        {
                            Message = "มีกิจกรรม เพิ่มใหม่ กรุณาตรวจสอบ",
                            UpdatedDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            CDDId = null,
                            userid = item.userid[ss],
                            Type = 3
                        });
                        ss++;
                    }
                }
                else
                {
                    calendar.Add(new Calendar
                    {
                        Userid = item.userid_create,
                        UseridCreate = null,
                        Detail = item.Detail,
                        Description = item.Description,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    });

                    notification.Add(new Notification
                    {
                        Message = "มีกิจกรรม เพิ่มใหม่ กรุณาตรวจสอบ",
                        UpdatedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        CDDId = null,
                        userid = item.userid_create,
                        Type = 3
                    });
                }
            }
            await _context.Notification.AddRangeAsync(notification);
            await _context.Calendar.AddRangeAsync(calendar);
            await _context.SaveChangesAsync();
        }
    }
}
