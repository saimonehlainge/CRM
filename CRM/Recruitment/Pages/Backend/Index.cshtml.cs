using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Helpers;
using Recruitment.Repositories;
using static Recruitment.Models.ResponseDTO;
using static Recruitment.Pages.Backend.ReportModel;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8629 // Nullable value type may be null.
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        public class ShowChart()
        {
            public string? username { get; set; }
            public List<string>? name { get; set; }
            public List<string>? detail { get; set; }
            public List<DetailChart>? list { get; set; }
        }

        public class DetailChart()
        {
            public List<string>? data { get; set; }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetChartData(int? Project, string? start, string? to, int? type, string? userid)
        {
            try
            {
                var DB = await _unitOfWork.CDDRepository.GetAllAsync();
                var DBProject = await _unitOfWork.ProjectRepository.GetAllAsync();
                DB = DB.Where(x => x.DeleteAt != 1

                           && (x.UserId != null
                && x.UserId != "0c3590c4-48d8-45b4-adce-bedd24cf0a61"
                && x.UserId != "0cb5d203-b9af-4090-882a-fe38c00483b7"
                && x.UserId != "196aa2a4-ce96-4901-bbb8-d9134111677a"
                && x.UserId != "1cf9829a-1bc2-495b-aa78-8c1ad8e4452c"
                && x.UserId != "21ebc19a-6070-49da-8cc1-a909a7011d8a"
                && x.UserId != "28439221-573f-4778-ba14-5ebab5554a53"
                && x.UserId != "2f29c9c2-9a87-4d7a-8c69-16eb963dfa93"
                && x.UserId != "3abd0dd6-a578-4435-8dcc-5d7912aec33b"
                && x.UserId != "4057cef0-ba38-4a95-b587-13ea288b9e41"
                && x.UserId != "73aa4663-dd99-44b3-8be7-b7892d051057"
                && x.UserId != "76249517-e95e-49c8-9281-220f57ca1759"
                && x.UserId != "897f08aa-4b8b-41eb-b3dc-f72aedb73d5f"
                && x.UserId != "8a0a9b9d-d2c4-4078-ae1b-5d76a2834ad1"
                && x.UserId != "9c5c98fc-05d5-4488-98f4-dd2569e9aef4"
                && x.UserId != "a3cdc399-2390-4ca3-8383-01ab454af0e2"
                && x.UserId != "a6631ea4-3c97-4d2c-af52-0b67908d691d"
                && x.UserId != "b5b96ad1-508b-44de-8356-102239e824c4"
                && x.UserId != "b72ceec1-a175-43f9-a2c4-4680f6c260ed"
                && x.UserId != "b8964f16-7c2e-4714-978d-3de5534a6e8b"
                && x.UserId != "bbacaa2b-427a-462c-802b-400e05b3b938"
                && x.UserId != "c075ebad-9277-478e-b86c-07e35c6bb0c1"
                && x.UserId != "dafd30e6-e23f-48cb-b0f1-88a5d5ffda8f"
                && x.UserId != "dd8d932e-75d8-4ee8-ba16-4e82fe4e9fd0"
                && x.UserId != "e4826f75-5873-4632-aea4-ec7fd7cb8eaf"
                && x.UserId != "f1ebd969-ab89-48b7-b7a9-008c3dc126e0")

                ).ToList();

                if (start == null || start == "")
                {
                    //DB = DB.Where(x => x.DeleteAt != 1 && (x.CreatedDate.Value.Month == DateTime.Now.Month && x.CreatedDate.Value.Year == DateTime.Now.Year)).ToList();
                }

                var fixDate = DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null); //DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null);
                DB = DB.Where(x => x.CreatedDate != null && x.CreatedDate.Value.Date >= fixDate.Date).ToList();

                var (userids, roles, _, department) = User.GetUser();
                List<CDD> NewCDD = new List<CDD>();
                var spit = userid == null ? null : userid.Split(',');
                if (spit != null)
                {
                    foreach (var item in spit)
                    {
                        var where_user = DB.Where(x => x.UserId != null && x.UserId.ToLower() == item.ToLower()).ToList();
                        NewCDD.AddRange(where_user);
                    }
                }
                else
                {

                    if (roles == "recruiter")
                    {
                        DB = DB.Where(x => x.UserId != null && x.UserId.ToLower() == userids.ToLower()).ToList();
                        //var db = await _unitOfWork.UserRoleRepository.GetUserRole();
                        //db = db.Where(x => x.RoleId.ToLower() == "b68ff80f-5702-4cb5-a6bd-b50ed58c8068").ToList();
                        //foreach (var item in db)
                        //{
                        //    var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.UserId.ToLower()).ToList();
                        //    NewCDD.AddRange(where_user);
                        //}
                    }
                }

                if (NewCDD.Count() != 0)
                {
                    DB = NewCDD;
                }

                if (roles == "project owner")
                {
                    var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                    var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);
                    Project = where_project.Project;
                }

                if (Project != null && Project != -1)
                {
                    DB = DB.Where(x => x.Project == Project).ToList();
                }

                if (start != null && start != "")
                {
                    //DB = DB.Where(x => x.CreatedDate.Value.Date >= Convert.ToDateTime(start).Date).ToList(); //old
                    DB = DB.Where(x => x.Datestatus != null && x.Datestatus.Value.Date >= Convert.ToDateTime(start).Date).ToList(); // change by sai mon
                }

                if (to != null && to != "")
                {
                    //DB = DB.Where(x => x.CreatedDate.Value.Date <= Convert.ToDateTime(to).Date).ToList(); //old
                    DB = DB.Where(x => x.Datestatus != null && x.Datestatus.Value.Date <= Convert.ToDateTime(to).Date).ToList();// change by sai mon
                }

                var RecruitStatus = await _unitOfWork.RecruitStatusRepository.GetAllAsync();

                #region test status
                // ไม่สนใจ
                var status_not_interested = RecruitStatus.FirstOrDefault(x => x.Id == 1 && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("ไม่สนใจ")) : null;
                // ไม่มาสกรีน
                var status_Not_coming_to_screen = RecruitStatus.FirstOrDefault(x => x.Id == 36 && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("ไม่มาสกรีน")) : null;
                // ไม่ผ่านสกรีน
                var status_Did_not_pass_the_screen = RecruitStatus.FirstOrDefault(x => (x.Id == 8 || x.Id == 7) && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("ไม่ผ่านสกรีน")) : null;
                // ไม่มาสัม
                var status_Didnt_come_to_the_interview = RecruitStatus.FirstOrDefault(x => x.Id == 12 && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("ไม่มาสัม")) : null;
                // ไม่ผ่านสัม
                var status_Didnt_pass_the_interview = RecruitStatus.FirstOrDefault(x => (x.Id == 17 || x.Id == 9) && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("ไม่ผ่านสัม")) : null;
                // ไม่ไปเริ่มงาน
                var status_Not_going_to_start_work = RecruitStatus.FirstOrDefault(x => x.Id == 20 && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("ไม่ไปเริ่มงาน")) : null;
                // ไปเริ่มงานแล้ว แต่ออกก่อนเก็บเงิน
                var status_I_started_working_but_left_before_collecting_the_money = RecruitStatus.FirstOrDefault(x => x.Id == 21 && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("ไปเริ่มงานแล้ว")) : null;
                // เก็บเงินได้แล้ว
                var status_I_have_already_collected_the_money = RecruitStatus.FirstOrDefault(x => (x.Id == 24 || x.Id == 25 || x.Id == 27 || x.Id == 28 || x.Id == 30 || x.Id == 37 || x.Id == 38) && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("เก็บเงินได้แล้ว") || x.Name.Contains("Success")) : null;
                // ออกก่อนพ้นการันตี
                var status_Exit_before_the_guarantee_expires = RecruitStatus.FirstOrDefault(x => x.Id == 32 && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("ออกก่อนพ้นการันตี")) : null;
                // รอเริ่มงาน
                var Com_only = RecruitStatus.FirstOrDefault(x => x.Id == 11 && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("รอเริ่มงาน")) : null;
                // รอเก็บเงิน
                var Waiting_work = RecruitStatus.FirstOrDefault(x => x.Id == 34 && x.DeleteAt != null) != null ? RecruitStatus.FirstOrDefault(x => x.Name.Contains("รอเก็บเงิน")) : null;
                #endregion

                if (type == 1)
                {
                    List<ShowChart> ShowChart = new List<ShowChart>();
                    var Group_User = await _unitOfWork.UsersRepository.GetUserResponse();
                    Group_User = Group_User.Where(x => x.Permission == "recruiter").ToList();
                    if (NewCDD.Count() != 0)
                    {
                        Group_User = NewCDD.GroupBy(x => new { x.UserId }).Select(x => new UserResponse
                        {
                            Id = x.Key.UserId
                        }).ToList();
                    }

                    var checkUserIds = DB.Select(x => x.UserId)
                     .Intersect(Group_User.Select(x => x.Id))
                     .OrderBy(id => id)
                     .ToList();

                    var getuser = await _unitOfWork.UsersRepository.GetAllAsync();

                    foreach (var itemu in checkUserIds)
                    {
                        var GetDB = DB.Where(x => (x.UserId != null && x.UserId.ToLower() == itemu.ToLower())).ToList();
                        var firstname = getuser.FirstOrDefault(x => x.Id.ToLower() == itemu.ToLower());
                        if (firstname != null)
                        {
                            List<DetailChart> DetailChart = new List<DetailChart>();
                            for (var a = 0; a < 5; a++)
                            {
                                List<string>? data = new List<string>();
                                if (a == 0)
                                {
                                    for (var i = 0; i < 10; i++)
                                    {
                                        if (i == 0)
                                        {
                                            var Count = GetDB.Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 1)
                                        {
                                            var Count = GetDB.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 2)
                                        {
                                            var Count = GetDB.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 3)
                                        {
                                            var Count = GetDB.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 4)
                                        {
                                            var Count = GetDB.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 5)
                                        {
                                            var Count = GetDB.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 6)
                                        {
                                            var Count = GetDB.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 7)
                                        {
                                            var Count = GetDB.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 8)
                                        {
                                            var Count = GetDB.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 9)
                                        {
                                            var Count = GetDB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                            data.Add(Count.ToString());
                                        }
                                    }
                                }
                                else if (a == 1)
                                {
                                    var Count = GetDB.Count();
                                    for (var i = 0; i < 10; i++)
                                    {
                                        double? sum = 0;
                                        if (i == 0)
                                        {
                                            data.Add(""); //Count.ToString()
                                        }
                                        else if (i == 1)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 2)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                            if (Count2 > 0)
                                            {
                                                Count = Count - Count2;
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            }
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 3)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                            if (Count2 > 0)
                                            {
                                                Count = Count - Count2;
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            }
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 4)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                            if (Count2 > 0)
                                            {
                                                Count = Count - Count2;
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            }
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 5)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                            if (Count2 > 0)
                                            {
                                                Count = Count - Count2;
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            }
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 6)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                            if (Count2 > 0)
                                            {
                                                Count = Count - Count2;
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            }
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 7)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                            if (Count2 > 0)
                                            {
                                                Count = Count - Count2;
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            }
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 8)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                            if (Count2 > 0)
                                            {
                                                Count = Count - Count2;
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            }
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 9)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                            if (Count2 > 0)
                                            {
                                                Count = Count - Count2;
                                                sum = Math.Round((double)Count2 / Count * 100, 2);
                                            }
                                            data.Add(sum.ToString());
                                        }
                                    }
                                }
                                else if (a == 2)
                                {
                                    var Count = GetDB.Count();
                                    for (var i = 0; i < 10; i++)
                                    {
                                        if (i == 0)
                                        {
                                            data.Add(""); //Count.ToString()
                                        }
                                        else if (i == 1)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 2)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 3)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 4)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 5)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 6)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 7)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 8)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                        else if (i == 9)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                            Count = Count - Count2;
                                            data.Add(Count.ToString());
                                        }
                                    }
                                }
                                else if (a == 3)
                                {
                                    decimal Count = GetDB.Count();
                                    decimal Count_kpi = GetDB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    for (var i = 0; i < 10; i++)
                                    {
                                        decimal? sum = 0;
                                        if (i == 0)
                                        {
                                            if (Count > 0)
                                                sum = Math.Round((decimal)Count_kpi / Count * 100, 2);
                                            var sumtotal = sum;
                                            data.Add(sumtotal.ToString());
                                        }
                                        else if (i == 1)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 2)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 3)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 4)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 5)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 6)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 7)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 8)
                                        {
                                            var Count2 = GetDB.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                        else if (i == 9)
                                        {
                                            var Count2 = GetDB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                            if (Count2 > 0)
                                                sum = Math.Round(Count_kpi / Count2 * 100, 2);
                                            data.Add(sum.ToString());
                                        }
                                    }
                                }
                                else if (a == 4)
                                {
                                    decimal Count = GetDB.Count();
                                    decimal Count_kpi = GetDB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    for (var i = 0; i < 1; i++)
                                    {
                                        if (i == 0)
                                        {
                                            var kpi = GetDB.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                            var notpass = GetDB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                            var suma = Math.Round((double)notpass / kpi * 100, 2);
                                            data.Add(suma.ToString());
                                        }
                                    }
                                }

                                DetailChart.Add(new IndexModel.DetailChart
                                {
                                    data = data,
                                });
                            }

                            var listdatail = new List<string>()
                            {
                                "จำนวน",
                                "%",
                                "เหลือ",
                                "% ในการเก็บเงินได้",
                                "% ไม่พ้น replace"
                            };

                            var listname = new List<string>()
                            {
                                "Total Lead",
                                "Not interested (ไม่สนใจ)",
                                "Screen - No Show (ไม่มาสกรีน)",
                                "Screen - Not Pass (ไม่ผ่านสกรีน)",
                                "Client - No Show (ไม่มาสัม)",
                                "Client - Not Pass (ไม่ผ่านสัม)",
                                "Not join (ไม่ไปเริ่มงาน)",
                                "Drop out (ไปเริ่มงานแล้ว แต่ออกก่อนเก็บเงิน)",
                                "Success KPI (เก็บเงินได้)",
                                "Not pass guarantee - Replacement"
                            };

                            ShowChart.Add(new IndexModel.ShowChart
                            {
                                username = firstname.Firstname + " " + firstname.Lastname,
                                name = listname,
                                detail = listdatail,
                                list = DetailChart
                            });
                        }
                    }

                    return new JsonResult(ShowChart);
                }
                else if (type == 2)
                {
                    #region เก็บไว้สำหรับ type 2-4 ขี้เกียจแก้
                    List<string> name = new List<string>();
                    List<string> detail = new List<string>();
                    List<string> data = new List<string>();
                    List<string> data2 = new List<string>();
                    List<string> data3 = new List<string>();
                    List<string> data4 = new List<string>();
                    List<string> data5 = new List<string>();
                    List<string> data6 = new List<string>();
                    List<string> data7 = new List<string>();
                    List<string> data8 = new List<string>();
                    List<string> data9 = new List<string>();
                    List<string> data10 = new List<string>();
                    #endregion

                    name.Add("Total Lead");
                    name.Add("Not interested (ไม่สนใจ)");
                    name.Add("Screen - No Show (ไม่มาสกรีน)");
                    name.Add("Screen - Not Pass (ไม่ผ่านสกรีน)");
                    name.Add("Client - No Show (ไม่มาสัม)");
                    name.Add("Client - Not Pass (ไม่ผ่านสัม)");
                    name.Add("Not join (ไม่ไปเริ่มงาน)");
                    name.Add("Drop out (ไปเริ่มงานแล้ว แต่ออกก่อนเก็บเงิน)");
                    name.Add("Success KPI (เก็บเงินได้)");
                    name.Add("Not pass guarantee - Replacement");

                    var DBProjects = DB.Where(x => x.Project != null).GroupBy(x => new { x.Project }).ToList();

                    for (var w = 0; w < DBProjects.Count(); w++)
                    {
                        var aawewe = DBProject.FirstOrDefault(x => x.Id == DBProjects.ToList()[w].Key.Project);
                        detail.Add(aawewe.Name);
                        var CheckPrject = DB.Where(x => x.Project == DBProjects.ToList()[w].Key.Project).ToList();
                        for (var i = 0; i < 10; i++)
                        {
                            if (i == 0)
                            {
                                var Coun2 = CheckPrject.Count();
                                data.Add(Coun2.ToString());
                            }
                            if (i == 1)
                            {
                                var Coun2 = CheckPrject.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).Count();
                                data2.Add(Coun2.ToString());
                            }
                            if (i == 2)
                            {
                                var Coun2 = CheckPrject.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).Count();
                                data3.Add(Coun2.ToString());
                            }
                            if (i == 3)
                            {
                                var Coun2 = CheckPrject.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).Count();
                                data4.Add(Coun2.ToString());
                            }
                            if (i == 4)
                            {
                                var Coun2 = CheckPrject.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).Count();
                                data5.Add(Coun2.ToString());
                            }
                            if (i == 5)
                            {
                                var Coun2 = CheckPrject.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).Count();
                                data6.Add(Coun2.ToString());
                            }
                            if (i == 6)
                            {
                                var Coun2 = CheckPrject.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).Count();
                                data7.Add(Coun2.ToString());
                            }
                            if (i == 7)
                            {
                                var Coun2 = CheckPrject.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).Count();
                                data8.Add(Coun2.ToString());
                            }
                            if (i == 8)
                            {
                                var Coun2 = CheckPrject.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).Count();
                                data9.Add(Coun2.ToString());
                            }
                            if (i == 9)
                            {
                                var Coun2 = CheckPrject.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).Count();
                                data10.Add(Coun2.ToString());
                            }
                        }
                    }

                    return new JsonResult(new { detail, name, data, data2, data3, data4, data5, data6, data7, data8, data9, data10 });
                }
                else if (type == 3)
                {
                    #region เก็บไว้สำหรับ type 2-4 ขี้เกียจแก้
                    List<string> name = new List<string>();
                    List<string> detail = new List<string>();
                    List<string> data = new List<string>();
                    List<string> data2 = new List<string>();
                    List<string> data3 = new List<string>();
                    List<string> data4 = new List<string>();
                    List<string> data5 = new List<string>();
                    List<string> data6 = new List<string>();
                    List<string> data7 = new List<string>();
                    List<string> data8 = new List<string>();
                    List<string> data9 = new List<string>();
                    List<string> data10 = new List<string>();
                    #endregion

                    var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                    var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);

                    #region colse by sai mon
                    //if (roles == "recruiter")
                    //{
                    //    DB = DB.Where(x => x.Project == where_project.Project).ToList();
                    //    DBProject = DBProject.Where(x => x.Id == where_project.Project).ToList();
                    //}
                    //else if (roles == "project owner")
                    //{
                    //    DBProject = DBProject.Where(x => x.Id == where_project.Project).ToList();
                    //}
                    #endregion

                    var CheckProject = DB.Where(x => x.UserId != "b8964f16-7c2e-4714-978d-3de5534a6e8b").ToList();
                    var GroupProject = CheckProject.OrderBy(o => o.Project).GroupBy(x => new { x.Project }).ToList();

                    for (var a = 0; a < 3; a++)
                    {
                        if (a == 0)
                        {
                            detail.Add("Total KPI");
                            //var Count = CheckProject.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) ||
                            //                                    (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                            var Count = CheckProject.ToList(); //change by sai mon
                            foreach (var group in GroupProject)
                            {
                                var projectId = group.Key.Project;
                                name.Add(DBProject.FirstOrDefault(x => x.Id == projectId)?.Name ?? "-");
                                var countKPIs = Count.Count(x => x.Project == projectId);

                                var countKPI = group.Where(x => x.Project == projectId).Count();
                                data.Add(countKPI.ToString());
                            }
                        }
                        else if (a == 1)
                        {
                            detail.Add("Total Replace");
                            //var Count = CheckProject.Where(x => x.Status == 35).ToList();
                            var Count = CheckProject.Where(x => x.Status == 32).ToList();
                            foreach (var group in GroupProject)
                            {
                                var projectId = group.Key.Project;
                                //var countReplace = Count.Count(x => x.Project == projectId);
                                var countReplace = group.Where(x => x.Project == projectId).Count();

                                data2.Add(countReplace.ToString());
                            }
                        }
                        else if (a == 2)
                        {
                            detail.Add("% Replace");
                            foreach (var group in GroupProject)
                            {
                                var projectId = group.Key.Project;
                                var countKPI = CheckProject.Count(x => x.Project == projectId &&
                                    ((x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) ||
                                     (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)));
                                var countReplace = CheckProject.Count(x => x.Project == projectId && x.Status == 35);

                                if (countKPI + countReplace == 0)
                                {
                                    data3.Add("0.00");
                                }
                                else
                                {
                                    var percent = ((countReplace * 100.0) / (countKPI + countReplace)).ToString("0.00");
                                    data3.Add(percent);
                                }
                            }
                        }
                    }

                    return new JsonResult(new { detail, name, data, data2, data3 });
                }
                else if (type == 4)
                {
                    #region เก็บไว้สำหรับ type 2-4 ขี้เกียจแก้
                    List<string> name = new List<string>();
                    List<string> detail = new List<string>();
                    List<string> data = new List<string>();
                    List<string> data2 = new List<string>();
                    List<string> data3 = new List<string>();
                    List<string> data4 = new List<string>();
                    List<string> data5 = new List<string>();
                    List<string> data6 = new List<string>();
                    List<string> data7 = new List<string>();
                    List<string> data8 = new List<string>();
                    List<string> data9 = new List<string>();
                    List<string> data10 = new List<string>();
                    #endregion

                    var total = DB.Where(x => x.UpdatedDate.Value.Month == DateTime.Now.Month 
                    && ((x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) 
                    || (status_I_have_already_collected_the_money != null 
                    && x.Status == status_I_have_already_collected_the_money.Id)) 
                    || x.Status == 32).ToList();
                    var dsds = await _unitOfWork.UsersRepository.GetAllAsync();
                    var target = dsds.FirstOrDefault(x => x.Id.ToLower() == userids.ToLower()).TargetKpi;
                    for (var i = 0; i < 3; i++)
                    {
                        if (i == 0)
                        {
                            detail.Add("Achieved");
                            data.Add(total.Count().ToString());
                        }
                        if (i == 1)
                        {
                            detail.Add("รอเก็บเงิน");
                            var DB2 = DB.Where(x => x.Status == 34 || (Waiting_work != null && x.Status == Waiting_work.Id)).ToList();
                            data.Add(DB2.Count().ToString());
                        }
                        if (i == 2)
                        {
                            detail.Add("รอเริ่มงาน");
                            var DB2 = DB.Where(x => x.Status == 11 || (Com_only != null && x.Status == Com_only.Id)).ToList();
                            data.Add(DB2.Count().ToString());
                        }
                    }
                    return new JsonResult(new { detail, data, Count = target }); //total.Count
                }

                return new JsonResult("ไม่มีข้อมูล");
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

    }
}
