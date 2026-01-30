using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class CalendarModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalendarModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetDataCalendar(string? startdate, string? enddate, int? status)
        {
            try
            {
                var (userid, roles, _, department) = User.GetUser();
                var team = await _unitOfWork.PermissionRepository.GetAllAsync();
                var DB = await _unitOfWork.CalendarRepository.GetAllAsync();
                List<ResponseDTO.Calendar> calendars = new List<ResponseDTO.Calendar>();
                List<ResponseDTO.Calendar> calendars_only = new List<ResponseDTO.Calendar>();
                List<ResponseDTO.Calendar> calendars_new = new List<ResponseDTO.Calendar>();

                var aDB = DB.Where(x => (x.StartDate != null && x.EndDate != null) && x.CreatedDate != null && x.CreatedDate.Value.Year == DateTime.Now.Year) //x.CreatedDate != null && x.CreatedDate!.Value.Month >= DateTime.Now.Month &&
                     .GroupBy(x => new
                     {
                         x.UseridCreate,
                         x.Userid,
                         x.Detail,
                         x.Description,
                         x.StartDate,
                         x.EndDate,
                         x.Status,
                         x.CreatedDate,
                         x.UpdatedDate,
                     }).ToList();

                if (startdate != null)
                {
                    var c = Convert.ToDateTime(startdate);
                    aDB = aDB.Where(x => (x.Key.StartDate != null && x.Key.StartDate.Value.Date >= c.Date)).ToList();
                }
                
                if (enddate != null)
                {
                    var e = Convert.ToDateTime(enddate);
                    aDB = aDB.Where(x => (x.Key.EndDate != null && x.Key.EndDate.Value.Date <= e.Date)).ToList();
                }

                if (status == null)
                {
                    aDB = aDB.Where(x => x.Key.Status == null || x.Key.Status == 2).ToList();
                }

                if (status != null)
                {
                    if (status == 2 || status == -1)
                    {
                        aDB = aDB.Where(x => x.Key.Status == null || x.Key.Status == 2).ToList();
                    }
                    else
                    {
                        aDB = aDB.Where(x => x.Key.Status == status).ToList();
                    }
                }

                if (roles == "recruiter")
                {
                    aDB = aDB.Where(x => x.Key.Userid == userid).ToList();
                }

                var color = "";
                foreach (var item in aDB)
                {
                    var detail = "";
                    if (item.Key.Status == 1)
                    {
                        detail = item.Key.Detail + " ( ทำแล้ว )";
                    }
                    else if (item.Key.Status == 2)
                    {
                        detail = item.Key.Detail + " ( ยังไม่ได้ทำ )";
                    }
                    else if (item.Key.Status == 3)
                    {
                        detail = item.Key.Detail + " ( ยกเลิก )";
                    }
                    else
                    {
                        detail = item.Key.Detail;
                    }

                    var WhereRole = await _unitOfWork.UsersRepository.FindRolueUser(item.Key.UseridCreate);
                    if (WhereRole != null)
                    {
                        color = WhereColor(WhereRole.Name);
                    }
                    else
                    {
                        var WhereRole2 = await _unitOfWork.UsersRepository.FindRolueUser(item.Key.Userid);
                        if (WhereRole2 != null)
                        {
                            if (WhereRole2 != null)
                            {
                                color = WhereColor(WhereRole2.Name);
                            }
                            else
                            {
                                color = WhereColor("recruiter");
                            }
                        }
                    }

                    calendars_new.Add(new ResponseDTO.Calendar
                    {
                        StartDate = item.Key.StartDate,
                        Description = item.Key.Description,
                        Detail = detail,
                        EndDate = item.Key.EndDate,
                        Userid = item.Key.Userid,
                        Color = color,
                        Status = item.Key.Status
                    });
                }

                var Result = calendars_new.GroupBy(x=> new { x.Detail, x.StartDate, x.EndDate, x.Color, x.Description }).Select(x => new
                {
                    title = x.Key.Detail,
                    start = x.Key.StartDate == null ? null : x.Key.StartDate.Value.ToString("yyyy-MM-dd") + "T00:00:00",
                    end = x.Key.EndDate == null ? null : x.Key.EndDate.Value.ToString("yyyy-MM-dd") + "T23:59:59",
                    color = x.Key.Color,  //"#f00",
                    textColor = "black",
                    //id = x.Id,
                    description = x.Key.Description == null ? "" : x.Key.Description
                }).ToList();

                return new JsonResult(Result);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public string WhereColor(string? rolue)
        {
            var color = "";
            if (rolue == "md")
            {
                var yellow_gold = "#FFDF00"; //สีเหลืองทอง MD
                color = yellow_gold;
            }
            else if (rolue == "manager")
            {
                var light_blue = "#ADD8E6"; //สีฟ้า Manager
                color = light_blue;
            }
            else if (rolue == "sup")
            {
                var green = "#008000"; // สีเขียว Supervisor
                color = green;
            }
            else if (rolue == "project owner")
            {
                var orange = "#FFA500"; // สีส้ม Project Owner
                color = orange;
            }
            else if (rolue == "recruiter")
            {
                var purple = "#A020F0"; // สีม่วง Recruiter
                color = purple;
            }
            else if (rolue == "1")
            {
                var grey = "#808080";
                color = grey;
            }
            else
            {
                color = "#f00";
            }
            return color;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAddData(RequestDTO.CalendarRequset request)
        {
            var i = 0;
            try
            {
                var (userid, roles, _, department) = User.GetUser();
                List<RequestDTO.CalendarRequset> CalendarRequset = new List<RequestDTO.CalendarRequset>();

                if (roles != "recruiter")
                {
                    var getuser = await _unitOfWork.UsersRepository.GetUserResponse();
                    var getprojectowner = getuser.FirstOrDefault(x => x.Id.ToLower() == userid.ToLower() );
                    var getuser_project = getuser.Where(x => x.Project == getprojectowner.Project && x.Permission == "recruiter").ToList();
                    List<string> adduser = new List<string>();
                    foreach (var item in getuser_project)
                    {
                        adduser.Add(item.Id);
                    }
                    if (adduser.Count() != 0)
                    { 
                        request.userid = adduser;
                    }
                }

                CalendarRequset.Add(new RequestDTO.CalendarRequset
                {
                    StartDate = request.StartDate,
                    Status = request.Status,
                    Description = request.Description,
                    Detail = request.Detail,
                    EndDate = request.EndDate,
                    userid = request.userid,
                    userid_create = userid
                });

                await _unitOfWork.CalendarRepository.InsertCalendar(CalendarRequset);
                i = 1;
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostRemoveData(RequestDTO.ChangeEvent request)
        {
            var i = 0;
            try
            {
                var DB = await _unitOfWork.CalendarRepository.GetAllAsync();
                var EventName = request.EventName.Split(" ");
                var First = DB.FirstOrDefault(x => x.Detail.Contains(EventName[0]));
                if (First != null)
                {
                    await _unitOfWork.CalendarRepository.RemoveAsync(First);
                    await _unitOfWork.CompleteAsync();
                    i = 1;
                }
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetCheckDateEvent(string? check_date, string? EventName)
        {
            try
            {
                var DB = await _unitOfWork.CalendarRepository.GetAllAsync();
                var CDate = Convert.ToDateTime(check_date);
                var valueEvent = Regex.Replace(EventName, @"\s*\(.*?\)", "");
                var Where = DB.FirstOrDefault(x => x.StartDate != null && x.StartDate.Value.Date == CDate.Date && x.Detail.Contains(valueEvent)); //==
                var (userid, roles, _, department) = User.GetUser();
                var CheckEdit = 0;
                var GetCheck = DB.FirstOrDefault(x => x.UseridCreate == userid && (x.StartDate != null && x.StartDate.Value.Date == CDate.Date) && x.Detail.Contains(valueEvent));
                if (GetCheck != null || roles != "recruiter")
                {
                    CheckEdit = 1;
                    Where = GetCheck;
                }
                else
                {
                    Where = DB.FirstOrDefault(x => x.Userid == userid && (x.StartDate != null && x.StartDate.Value.Date == CDate.Date) && x.Detail.Contains(valueEvent));
                }

                return new JsonResult(new { db = Where, checkedit = CheckEdit });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostChangeEvent(RequestDTO.ChangeEvent request)
        {
            try
            {
                var DB = await _unitOfWork.CalendarRepository.GetAllAsync();
                //var EventName = request.EventName.Split(" ");
                var (userid, roles, _, department) = User.GetUser();

                var valueEvent = Regex.Replace(request.EventName, @"\s*\(.*?\)", "");
                var First = DB.Where(x => (x.Userid != null && x.Userid == userid) && (x.Detail != null && x.Detail == valueEvent)).ToList();

                var CStart = Convert.ToDateTime(request.start);
                var CEnd = Convert.ToDateTime(request.end);

                First = First.Where(x => x.Description == request.description && (x.StartDate != null && x.StartDate.Value.Date == CStart.Date) && (x.EndDate != null && x.EndDate.Value.Date == CEnd.Date)).ToList();

                DateTime? ChangeStart = null;
                DateTime? ChangeEnd = null;
                string? ChangeEventName = null;
                string? Changedescription = null;

                if (request.ChangeStart != null && request.ChangeEventName != "")
                {
                    ChangeStart = Convert.ToDateTime(request.ChangeStart);
                }

                if (request.ChangeEnd != null && request.ChangeEventName != "")
                {
                    ChangeEnd = Convert.ToDateTime(request.ChangeEnd);
                }

                if (request.ChangeEventName != null && request.ChangeEventName != "")
                {
                    ChangeEventName = request.ChangeEventName;
                }

                if (request.Changedescription != null && request.Changedescription != "")
                {
                    Changedescription = request.Changedescription;
                }

                if (request.ChangeStatus == -1)
                {
                    request.ChangeStatus = null;
                }

                First = First.Select(x => new Calendar
                {
                    StartDate = ChangeStart != null ? ChangeStart : x.StartDate,
                    EndDate = ChangeEnd != null ? ChangeEnd : x.EndDate,
                    Detail = ChangeEventName != null ? ChangeEventName : x.Detail,
                    Description = Changedescription != null ? Changedescription : x.Description,
                    Status = request.ChangeStatus,
                    //=========
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Userid = x.Userid,
                    UseridCreate = x.UseridCreate
                }).ToList();

                _unitOfWork.CalendarRepository.UpdateRange(First);
                await _unitOfWork.CompleteAsync();
                return new JsonResult(1);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
