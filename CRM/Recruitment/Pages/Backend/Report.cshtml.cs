using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Recruitment.Areas.Identity.Data;
using Recruitment.Helpers;
using Recruitment.Models;
using Recruitment.Repositories;
using System.Collections.Immutable;
using System.Globalization;
using static Recruitment.Models.ResponseDTO;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8629 // Nullable value type may be null.
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class ReportModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTable(int? Project, string? StartDate, string? EndDate, int? Type, string? userid, int? Status)
        {
            try
            {
                var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();
                GetDB = GetDB.Where(x => x.DeleteAt != 1
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

                var fixDate = DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null);
                GetDB = GetDB.Where(x => x.Project != -1 && x.CreatedDate != null && x.CreatedDate.Value.Date >= fixDate.Date).ToList();

                var (userids, roles, _, department) = User.GetUser();

                List<CDD> NewCDD = new List<CDD>();
                var spit = userid == null ? null : userid.Split(',');
                if (spit != null)
                {
                    foreach (var item in spit)
                    {
                        var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.ToLower()).ToList();
                        NewCDD.AddRange(where_user);
                    }
                }
                else
                {
                    if (roles == "recruiter")
                    {
                        GetDB = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == userids.ToLower()).ToList();
                    }
                }

                if (roles == "project owner")
                {
                    var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                    var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);
                    Project = where_project.Project;
                }

                if (NewCDD.Count() != 0)
                {
                    GetDB = NewCDD;
                }

                if (Project != null && Project != -1)
                {
                    GetDB = GetDB.Where(x => x.Project == Project).ToList();
                }

                if (StartDate != null)
                {
                    GetDB = GetDB.Where(x => x.Datestatus != null && x.Datestatus.Value.Date >= Convert.ToDateTime(StartDate).Date).ToList();
                }

                if (EndDate != null)
                {
                    GetDB = GetDB.Where(x => x.Datestatus != null && x.Datestatus.Value.Date <= Convert.ToDateTime(EndDate).Date).ToList();
                }

                // close 16/10/2025
                //if (Status != null && Status != -1)
                //{
                //    GetDB = GetDB.Where(x => x.Status == Status).ToList();
                //}

                var RecruitStatus = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
                var Group = GetDB.GroupBy(x => new { x.UserId }).ToList();
                List<ReportResponse> NewDB = new List<ReportResponse>();

                var user = await _unitOfWork.UsersRepository.GetUserResponse();

                if (StartDate == null || StartDate == "")
                {
                    //GetDB = GetDB.Where(x => x.DeleteAt != 1 && (x.CreatedDate.Value.Month == DateTime.Now.Month && x.CreatedDate.Value.Year == DateTime.Now.Year)).ToList();
                }

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

                if (Type == 1)
                {
                    user = user.Where(x => x.Permission == "recruiter").ToList();
                    foreach (var item in user)
                    {
                        var where = GetDB.Where(x => item.Id != null && x.UserId == item.Id).ToList();
                        if (where.Count() != 0)
                        {
                            decimal Count_kpi = where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                            for (var a = 0; a < 5; a++)
                            {
                                double Count = where.Count();
                                if (a == 0)
                                {
                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = user == null ? null : user.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower()).Firstname,
                                        status = "จำนวน",
                                        total_lead = Count.ToString(),
                                        not_interested = where.Count() == 0 ? "" : where.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count().ToString(),
                                        screen_noshow = where.Count() == 0 ? "" : where.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count().ToString(),
                                        screen_notpass = where.Count() == 0 ? "" : where.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count().ToString(),
                                        client_noshow = where.Count() == 0 ? "" : where.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count().ToString(),
                                        client_notpass = where.Count() == 0 ? "" : where.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count().ToString(),
                                        notjoin = where.Count() == 0 ? "" : where.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count().ToString(),
                                        dropout = where.Count() == 0 ? "" : where.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count().ToString(),
                                        success_kpi = where.Count() == 0 ? "" : where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count().ToString(),
                                        notpass_guarantee = where.Count() == 0 ? "" : where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count().ToString()
                                    });
                                }
                                else if (a == 1)
                                {
                                    double? sum = 0;

                                    var count2 = where.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                    if (count2 > 0)
                                        sum = Math.Round((double)count2 / Count * 100, 2);
                                    var not_interested = sum;

                                    var count3 = where.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                    if (count3 > 0)
                                    {
                                        Count = Count - count3;
                                        sum = Math.Round((double)count3 / Count * 100, 2);
                                    }
                                    else
                                    {
                                        sum = 0;
                                    }
                                    var screen_noshow = sum;

                                    var count4 = where.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                    if (count4 > 0)
                                    {
                                        Count = Count - count4;
                                        sum = Math.Round((double)count4 / Count * 100, 2);
                                    }
                                    else
                                    {
                                        sum = 0;
                                    }
                                    var screen_notpass = sum;

                                    var count5 = where.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                    if (count5 > 0)
                                    {
                                        Count = Count - count5;
                                        sum = Math.Round((double)count5 / Count * 100, 2);
                                    }
                                    else
                                    {
                                        sum = 0;
                                    }
                                    var client_noshow = sum;

                                    var count7 = where.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                    if (count7 > 0)
                                    {
                                        Count = Count - count7;
                                        sum = Math.Round((double)count7 / Count * 100, 2);
                                    }
                                    else
                                    {
                                        sum = 0;
                                    }
                                    var client_notpass = sum;

                                    var count8 = where.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                    if (count8 > 0)
                                    {
                                        Count = Count - count8;
                                        sum = Math.Round((double)count8 / Count * 100, 2);
                                    }
                                    else
                                    {
                                        sum = 0;
                                    }
                                    var notjoin = sum;

                                    var count9 = where.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                    if (count9 > 0)
                                    {
                                        Count = Count - count9;
                                        sum = Math.Round((double)count9 / Count * 100, 2);
                                    }
                                    else
                                    {
                                        sum = 0;
                                    }
                                    var dropout = sum;

                                    var count10 = where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                    if (count10 > 0)
                                    {
                                        Count = Count - count10;
                                        sum = Math.Round((double)count10 / Count * 100, 2);
                                    }
                                    else
                                    {
                                        sum = 0;
                                    }
                                    var success_kpi = sum;

                                    var count11 = where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    if (count11 > 0)
                                    {
                                        Count = Count - count11;
                                        sum = Math.Round((double)count11 / Count * 100, 2);
                                    }
                                    else
                                    {
                                        sum = 0;
                                    }
                                    var notpass_guarantee = sum;

                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = user == null ? null : user.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower()).Firstname,
                                        status = "%",
                                        total_lead = null,
                                        not_interested = not_interested.ToString() + " %",
                                        screen_noshow = screen_noshow.ToString() + " %",
                                        screen_notpass = screen_notpass.ToString() + " %",
                                        client_noshow = client_noshow.ToString() + " %",
                                        client_notpass = client_notpass.ToString() + " %",
                                        notjoin = notjoin.ToString() + " %",
                                        dropout = dropout.ToString() + " %",
                                        success_kpi = success_kpi.ToString() + " %",
                                        notpass_guarantee = notpass_guarantee.ToString() + " %"
                                    });
                                }
                                else if (a == 2)
                                {
                                    var not_interested = Count - where.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).Count();
                                    var screen_noshow = not_interested - where.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).Count();
                                    var screen_notpass = screen_noshow - where.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).Count();
                                    var client_noshow = screen_notpass - where.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).Count();
                                    var client_notpass = client_noshow - where.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).Count();
                                    var notjoin = client_notpass - where.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).Count();
                                    var dropout = notjoin - where.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).Count();
                                    var success_kpi = dropout - where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).Count();
                                    var notpass_guarantee = success_kpi - where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).Count();

                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = user == null ? null : user.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower()).Firstname,
                                        status = "เหลือ",
                                        total_lead = null,
                                        not_interested = not_interested.ToString(),
                                        screen_noshow = screen_noshow.ToString(),
                                        screen_notpass = screen_notpass.ToString(),
                                        client_noshow = client_noshow.ToString(),
                                        client_notpass = client_notpass.ToString(),
                                        notjoin = notjoin.ToString(),
                                        dropout = dropout.ToString(),
                                        success_kpi = success_kpi.ToString(),
                                        notpass_guarantee = notpass_guarantee.ToString()
                                    });
                                }
                                else if (a == 3)
                                {
                                    double? sum = 0;

                                    var suma = Math.Round((double)Count_kpi / Count * 100, 2);
                                    var sumtotal = suma;

                                    var count2 = where.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                    if (count2 > 0)
                                        sum = Math.Round((double)Count_kpi / count2 * 100, 2);
                                    var not_interested = sum;

                                    var count3 = where.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                    if (count3 > 0)
                                        sum = Math.Round((double)Count_kpi / count3 * 100, 2);
                                    var screen_noshow = sum;

                                    var count4 = where.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                    if (count4 > 0)
                                        sum = Math.Round((double)Count_kpi / count4 * 100, 2);
                                    var screen_notpass = sum;

                                    var count5 = where.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                    if (count5 > 0)
                                        sum = Math.Round((double)Count_kpi / count5 * 100, 2);
                                    var client_noshow = sum;

                                    var count7 = where.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                    if (count7 > 0)
                                        sum = Math.Round((double)Count_kpi / count7 * 100, 2);
                                    var client_notpass = sum;

                                    var count8 = where.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                    if (count8 > 0)
                                        sum = Math.Round((double)Count_kpi / count8 * 100, 2);
                                    var notjoin = sum;

                                    var count9 = where.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                    if (count9 > 0)
                                        sum = Math.Round((double)Count_kpi / count9 * 100, 2);
                                    var dropout = sum;

                                    var count10 = where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                    if (count10 > 0)
                                        sum = Math.Round((double)Count_kpi / count10 * 100, 2);
                                    var success_kpi = sum;

                                    var count11 = where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    if (count11 > 0)
                                        sum = Math.Round((double)Count_kpi / count11 * 100, 2);
                                    var notpass_guarantee = sum;

                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = user == null ? null : user.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower()).Firstname,
                                        status = "% ในการเก็บเงินได้",
                                        total_lead = sumtotal.ToString() + " %",
                                        not_interested = not_interested.ToString() + " %",
                                        screen_noshow = screen_noshow.ToString() + " %",
                                        screen_notpass = screen_notpass.ToString() + " %",
                                        client_noshow = client_noshow.ToString() + " %",
                                        client_notpass = client_notpass.ToString() + " %",
                                        notjoin = notjoin.ToString() + " %",
                                        dropout = dropout.ToString() + " %",
                                        success_kpi = success_kpi.ToString() + " %",
                                        notpass_guarantee = notpass_guarantee.ToString() + " %"
                                    });
                                }
                                else if (a == 4)
                                {
                                    var kpi = where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                    var notpass = where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    var suma = Math.Round((double)notpass / kpi * 100, 2);
                                    var sumtotal = suma;

                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = user == null ? null : user.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower()).Firstname,
                                        status = "% ไม่พ้น replace",
                                        total_lead = sumtotal.ToString() + " %",
                                        not_interested = null,
                                        screen_noshow = null,
                                        screen_notpass = null,
                                        client_noshow = null,
                                        client_notpass = null,
                                        notjoin = null,
                                        dropout = null,
                                        success_kpi = null,
                                        notpass_guarantee = null
                                    });
                                }
                            }
                        }
                    }
                }
                else if (Type == 2)
                {
                    // 1. โหลด project ทั้งหมดและกรอง DeleteAt
                    var projectAll = await _unitOfWork.ProjectRepository.GetAllAsync();
                    projectAll = projectAll.Where(x => x.DeleteAt != 1).ToList();

                    // 2. ดึงเฉพาะ projectId ที่มีการใช้งานจริง
                    var usedProjectIds = GetDB
                        .Where(x => x.Project != null && x.Project != -1 && x.UserId != null)
                        .Select(x => x.Project)
                        .Distinct()
                        .ToList();

                    // 3. สร้าง project ที่มีการใช้งานจริงเท่านั้น
                    var activeProjects = projectAll.Where(x => usedProjectIds.Contains(x.Id)).ToList();

                    List<string> columns = new List<string>();
                    List<List<string>> allRows = new List<List<string>>();

                    // เริ่ม header
                    columns.Add("Recruit");
                    columns.Add("Status");

                    // เพิ่ม column โปรเจคที่มีข้อมูลจริง
                    foreach (var pj in activeProjects)
                    {
                        columns.Add(pj.Name);
                    }
                    columns.Add("Total All Port");

                    // วนลูป recruiter
                    //user = user.Where(x => x.Permission == "recruiter").ToList();
                    var aa = GetDB.Where(x => x.UserId != null).GroupBy(x => new { x.UserId }).ToList();
                    //var aa = GetDB.Where(x=>x.Project != null).GroupBy(x => new { x.Project }).ToList();
                    foreach (var item in aa)
                    {
                        var where = GetDB.Where(x => x.Project != null && (item.Key.UserId != null && x.UserId == item.Key.UserId)).ToList();
                        //var where = GetDB.Where(x => x.Project != null && item.Key.Project != null && x.Project == item.Key.Project).ToList();

                        for (var asd = 0; asd < 6; asd++)
                        {
                            // ชื่อ recruiter เปลื่ยนเป็น project
                            var name = user?.FirstOrDefault(x => item.Key.UserId != null && x.Id.ToLower() == item.Key.UserId.ToLower())?.Firstname;
                            if (name != null)
                            {
                                var ss = new List<string>();
                                //var projectname = projectAll?.FirstOrDefault(x => item.Key.Project != null && x.Id == item.Key.Project)?.Name;
                                //ss.Add(projectname);
                                ss.Add(name);

                                // สถานะ
                                switch (asd)
                                {
                                    case 0: ss.Add("จำนวน"); break;
                                    case 1: ss.Add("% Share"); break;
                                    case 2: ss.Add("จำนวน KPI (เก็บเงินได้)"); break;
                                    case 3: ss.Add("จำนวนที่เก็บเงินได้ แต่ไม่พ้นรีเพลส"); break;
                                    case 4: ss.Add("จำนวนทั้งหมด"); break;
                                    case 5: ss.Add("% Share"); break;
                                }

                                // วนเฉพาะ project ที่มีข้อมูล
                                foreach (var pj in activeProjects)
                                {
                                    var count = where.Where(x => x.Project == pj.Id).ToList();

                                    if (asd == 0)
                                    {
                                        ss.Add(count.Count().ToString());
                                    }
                                    else if (asd == 1)
                                    {
                                        var total = where.Count();
                                        var percent = total == 0 ? "0.00" : ((((double)count.Count()) / total) * 100).ToString("0.00");
                                        ss.Add(percent + " %");
                                    }
                                    else if (asd == 2)
                                    {
                                        var kpi = count.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) ||
                                                                   (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                                        ss.Add(kpi.Count().ToString());
                                    }
                                    else if (asd == 3)
                                    {
                                        var notPassed = count.Count(x => x.Status == 32);
                                        ss.Add(notPassed.ToString());
                                    }
                                    else if (asd == 4)
                                    {
                                        var kpi = count.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) ||
                                                                   (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                                        var notPassed = count.Count(x => x.Status == 32);
                                        ss.Add((kpi.Count() + notPassed).ToString());
                                    }
                                    else if (asd == 5)
                                    {
                                        var kpi = count.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) ||
                                                                   (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                                        var notPassed = count.Count(x => x.Status == 32);
                                        var total = count.Count();
                                        var percent = total == 0 ? "0.00" : (((((double)kpi.Count() + notPassed) * 100) / total)).ToString("0.00");
                                        ss.Add(percent + " %");
                                    }
                                }

                                // Total per row
                                if (asd == 0)
                                {
                                    ss.Add(where.Count().ToString());
                                }
                                else if (asd == 1)
                                {
                                    ss.Add("");
                                }
                                else if (asd == 2)
                                {
                                    var where_kpi = where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) ||
                                                                     (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                                    ss.Add(where_kpi.Count().ToString());
                                }
                                else if (asd == 3)
                                {
                                    var re = where.Where(x => x.Status == 32).ToList();
                                    ss.Add(re.Count().ToString());
                                }
                                else if (asd == 4)
                                {
                                    var where_kpi = where.Count(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) ||
                                                                     (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id));
                                    var re = where.Count(x => x.Status == 32);
                                    ss.Add((where_kpi + re).ToString());
                                }
                                else if (asd == 5)
                                {
                                    ss.Add("");
                                }

                                allRows.Add(ss);
                            }
                        }
                    }

                    // ส่งกลับ
                    NewDB.Add(new ResponseDTO.ReportResponse
                    {
                        project = columns,
                        CountData = allRows
                    });

                }
                else if (Type == 21)
                {
                    user = user.Where(x => x.Permission == "recruiter").ToList();
                    var GetProject = await _unitOfWork.ProjectRepository.GetAllAsync();
                    foreach (var item in GetProject)
                    {
                        var where = GetDB.Where(x => item.Id != null && x.Project == item.Id && x.UserId != null).ToList();
                        if (where.Count() != 0)
                        {
                            decimal Count_kpi = where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                            for (var a = 0; a < 3; a++)
                            {
                                decimal Count = where.Count();
                                if (a == 0)
                                {
                                    //user == null ? null : user.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower()).Firstname,
                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = item.Name,
                                        status = "Amount",
                                        total_lead = Count.ToString(),
                                        not_interested = where.Count() == 0 ? "" : where.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count().ToString(),
                                        screen_noshow = where.Count() == 0 ? "" : where.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count().ToString(),
                                        screen_notpass = where.Count() == 0 ? "" : where.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count().ToString(),
                                        client_noshow = where.Count() == 0 ? "" : where.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count().ToString(),
                                        client_notpass = where.Count() == 0 ? "" : where.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count().ToString(),
                                        notjoin = where.Count() == 0 ? "" : where.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count().ToString(),
                                        dropout = where.Count() == 0 ? "" : where.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count().ToString(),
                                        success_kpi = where.Count() == 0 ? "" : where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count().ToString(),
                                        notpass_guarantee = where.Count() == 0 ? "" : where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count().ToString()
                                    });
                                }
                                else if (a == 1)
                                {
                                    var not_interested = Count - where.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).Count();
                                    var screen_noshow = not_interested - where.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).Count();
                                    var screen_notpass = screen_noshow - where.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).Count();
                                    var client_noshow = screen_notpass - where.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).Count();
                                    var client_notpass = client_noshow - where.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).Count();
                                    var notjoin = client_notpass - where.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).Count();
                                    var dropout = notjoin - where.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).Count();
                                    var success_kpi = dropout - where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).Count();
                                    var notpass_guarantee = success_kpi - where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).Count();
                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = item.Name, //user == null ? null : user.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower()).Firstname,
                                        status = "หายไป",
                                        total_lead = null,
                                        not_interested = not_interested.ToString(),
                                        screen_noshow = screen_noshow.ToString(),
                                        screen_notpass = screen_notpass.ToString(),
                                        client_noshow = client_noshow.ToString(),
                                        client_notpass = client_notpass.ToString(),
                                        notjoin = notjoin.ToString(),
                                        dropout = dropout.ToString(),
                                        success_kpi = success_kpi.ToString(),
                                        notpass_guarantee = notpass_guarantee.ToString()
                                    });
                                }
                                else if (a == 2)
                                {
                                    double? sum = 0;
                                    var count2 = where.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                    if (count2 > 0)
                                        sum = Math.Round((double)Count_kpi / count2 * 100, 2);
                                    var not_interested = sum;

                                    var count3 = where.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                    if (count3 > 0)
                                        sum = Math.Round((double)Count_kpi / count3 * 100, 2);
                                    var screen_noshow = sum;

                                    var count4 = where.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                    if (count4 > 0)
                                        sum = Math.Round((double)Count_kpi / count4 * 100, 2);
                                    var screen_notpass = sum;

                                    var count5 = where.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                    if (count5 > 0)
                                        sum = Math.Round((double)Count_kpi / count5 * 100, 2);
                                    var client_noshow = sum;

                                    var count7 = where.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                    if (count7 > 0)
                                        sum = Math.Round((double)Count_kpi / count7 * 100, 2);
                                    var client_notpass = sum;

                                    var count8 = where.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                    if (count8 > 0)
                                        sum = Math.Round((double)Count_kpi / count8 * 100, 2);
                                    var notjoin = sum;

                                    var count9 = where.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                    if (count9 > 0)
                                        sum = Math.Round((double)Count_kpi / count9 * 100, 2);
                                    var dropout = sum;

                                    var count10 = where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                    if (count10 > 0)
                                        sum = Math.Round((double)Count_kpi / count10 * 100, 2);
                                    var success_kpi = sum;

                                    var count11 = where.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    if (count11 > 0)
                                        sum = Math.Round((double)Count_kpi / count11 * 100, 2);
                                    var notpass_guarantee = sum;

                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = item.Name, //user == null ? null : user.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower()).Firstname,
                                        status = "% หายไป",
                                        total_lead = null,
                                        not_interested = not_interested.ToString() + " %",
                                        screen_noshow = screen_noshow.ToString() + " %",
                                        screen_notpass = screen_notpass.ToString() + " %",
                                        client_noshow = client_noshow.ToString() + " %",
                                        client_notpass = client_notpass.ToString() + " %",
                                        notjoin = notjoin.ToString() + " %",
                                        dropout = dropout.ToString() + " %",
                                        success_kpi = success_kpi.ToString() + " %",
                                        notpass_guarantee = notpass_guarantee.ToString() + " %"
                                    });
                                }
                            }
                        }
                    }
                }
                else if (Type == 3)
                {
                    #region close by sai mon 06/11/2025
                    //if (roles == "recruiter")
                    //{
                    //    var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                    //    var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);
                    //    //GetDB = GetDB.Where(x => (x.Project == where_project.Project) && (where_project.Project != -1)).ToList(); //F
                    //    GetDB = GetDB.Where(x => (x.Project == where_project.Project)).ToList(); // sai mon
                    //}
                    #endregion

                    var project = await _unitOfWork.ProjectRepository.GetAllAsync();
                    project = project.Where(x => x.DeleteAt != 1).ToList();

                    List<string> columns = new List<string>();
                    List<List<string>> allRows = new List<List<string>>();

                    //user = user.Where(x => x.Permission == "recruiter" && x.Project != -1).ToList();//f 

                    user = user.Where(x => x.Permission == "recruiter").ToList();//sai mon 06/11/2025
                    columns.Add("Recruit");
                    columns.Add("Status");
                    foreach (var item in user)
                    {
                        var where = GetDB.Where(x => x.Project != null && item.Id != null && x.UserId == item.Id).ToList();
                        if (where.Count() != 0)
                        {
                            for (var asd = 0; asd < 6; asd++)
                            {
                                var ss = new List<string>();

                                // 1. Recruit name
                                var name = user?.FirstOrDefault(x => item.Id != null && x.Id.ToLower() == item.Id.ToLower())?.Firstname;
                                ss.Add(name);

                                // 2. Status label
                                switch (asd)
                                {
                                    case 0: ss.Add("จำนวน"); break;
                                    case 1: ss.Add("% Schare"); break;
                                    case 2: ss.Add("จำนวน KPI (เก็บเงินได้)"); break;
                                    case 3: ss.Add("จำนวนที่เก็บเงินได้ แต่ไม่พ้นรีเพลส"); break;
                                    case 4: ss.Add("จำนวนทั้งหมด"); break;
                                    case 5: ss.Add("% Share"); break;
                                }

                                // 3. Projects loop
                                var GroupCDD = where.GroupBy(x => new { x.Project }).ToList();
                                foreach (var item_project in GroupCDD)
                                {
                                    var projectId = item_project.Key.Project;
                                    var count = where.Where(x => x.Project == projectId).ToList();

                                    if (asd == 0)
                                    {
                                        // แถวแรก: จำนวน
                                        var wp = project.FirstOrDefault(x => x.Id == projectId);
                                        columns.Add(wp?.Name ?? "-");
                                        ss.Add(count.Count().ToString());
                                    }
                                    else if (asd == 1)
                                    {
                                        // คำนวณ % Share
                                        var total = where.Count();
                                        var percent = total == 0 ? "0.00" : (((double)count.Count() * 100) / total).ToString("F2");
                                        ss.Add(percent + " %");
                                    }
                                    else if (asd == 2)
                                    {
                                        // จำนวน KPI (เก็บเงินได้)
                                        var kpi = count.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                                        ss.Add(kpi.Count().ToString());
                                    }
                                    else if (asd == 3)
                                    {
                                        // จำนวนที่เก็บเงินได้ แต่ไม่พ้นรีเพลส
                                        var notPassed = count.Count(x => x.Status == 32); // หรือ status รีเพลส
                                        ss.Add(notPassed.ToString());
                                    }
                                    else if (asd == 4)
                                    {
                                        // จำนวนทั้งหมด = KPI + ไม่พ้นรีเพลส
                                        var kpi = count.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                                        var notPassed = count.Count(x => x.Status == 32);
                                        ss.Add((kpi.Count() + notPassed).ToString());
                                    }
                                    else if (asd == 5)
                                    {
                                        // % Share = KPI + ไม่พ้นรีเพลส เทียบกับ Total
                                        var kpi = count.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                                        var notPassed = count.Where(x => x.Status == 32);
                                        var total = count.Count();
                                        var percent = total == 0 ? "0.00" : ((((double)kpi.Count() + notPassed.Count()) * 100) / total).ToString("F2");
                                        ss.Add(percent + " %");
                                    }
                                }

                                // 4. Add total column
                                if (asd == 0)
                                {
                                    columns.Add("Total All Port");
                                    ss.Add(where.Count().ToString());
                                }
                                else if (asd == 1)
                                {
                                    ss.Add("");
                                }
                                else if (asd == 2)
                                {
                                    var where_kpi = where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList();
                                    ss.Add(where_kpi.Count().ToString());
                                }
                                else if (asd == 3)
                                {
                                    var re = where.Where(x => x.Status == 32).ToList();
                                    ss.Add(re.Count().ToString());
                                }
                                else if (asd == 4)
                                {
                                    var where_kpi = where.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                    var re = where.Where(x => x.Status == 32).ToList().Count();
                                    var rewhere_kpi = where_kpi + re;
                                    ss.Add(rewhere_kpi.ToString());
                                }
                                else if (asd == 5)
                                {
                                    ss.Add("");
                                }

                                // 5. Add row
                                allRows.Add(ss);
                            }
                        }
                    }

                    // 6. ส่งกลับ JSON หรือใช้ใน View
                    NewDB.Add(new ResponseDTO.ReportResponse
                    {
                        project = columns,
                        CountData = allRows
                    });
                }
                else if (Type == 4)
                {
                    List<ResponseDTO.UserResponse> new_user = new List<ResponseDTO.UserResponse>();
                    var target = userid == null ? null : userid.Split(',');
                    if (spit != null)
                    {
                        foreach (var item in target)
                        {
                            var where_user = user.Where(x => x.Id != null && x.Id.ToLower() == item.ToLower()).ToList();
                            new_user.AddRange(where_user);
                        }
                    }
                    else
                    {
                        if (roles == "recruiter")
                        {
                            user = user.Where(x => x.Id != null && x.Id.ToLower() == userids.ToLower()).ToList();
                            //var db = await _unitOfWork.UserRoleRepository.GetUserRole();
                            //db = db.Where(x => x.RoleId.ToLower() == "b68ff80f-5702-4cb5-a6bd-b50ed58c8068").ToList();
                            //foreach (var item in db)
                            //{
                            //    var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.UserId.ToLower()).ToList();
                            //    NewCDD.AddRange(where_user);
                            //}
                        }
                    }

                    if (new_user.Count() != 0)
                    {
                        user = new_user;
                    }

                    var port = await _unitOfWork.ProjectRepository.GetAllAsync();
                    var Rule_ = user.Where(x => x.Permission == "recruiter").ToList();
                    foreach (var item in Rule_)
                    {
                        for (var a = 0; a < 4; a++)
                        {
                            if (a == 0)
                            {
                                DateTime today = DateTime.Today;
                                int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
                                int remainingDays = daysInMonth - today.Day;

                                //old 
                                //var aa = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id)
                                //&& x.UpdatedDate.Value.Month == DateTime.Now.Month
                                //&& (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38)
                                //|| (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)
                                //|| x.Status == 32).ToList();
                                // end old

                                //up by sai mon
                                var S = Convert.ToDateTime(StartDate, new CultureInfo("en-US"));
                                var E = Convert.ToDateTime(EndDate, new CultureInfo("en-US"));

                                int countDays = today.Day;

                                var aa = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id)
                               && (x.Datestatus != null && x.Datestatus.Value.Date >= S.Date && x.Datestatus.Value.Date <= E.Date)
                               && ((x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38))
                               || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)
                               || x.Status == 32).ToList();

                                //var totalKpi = aa.Sum(x => string.IsNullOrEmpty(x.KPI) ? 0m : Convert.ToDecimal(x.KPI)); // old
                                var totalKpi = aa.Count();
                                // end sai mon

                                //string screen_noshow = aa.Count() == 0 || item.Target == 0 ? "0" : (((double)aa.Count() / (double)item.Target) * 100).ToString("F2"); // old
                                string screen_noshow = aa.Count() == 0 || item.Target == 0 ? "0" : (((double)totalKpi / (double)item.Target) * 100).ToString("F2");  //update by sai mon

                                if (today.Date > E.Date)
                                {
                                    remainingDays = 0;
                                    countDays = E.Day + today.Day;
                                }

                                //var runrate = Convert.ToDouble(aa.Count() + (((double)aa.Count() / today.Day) * remainingDays)); // old
                                //var runrate2 = Convert.ToDouble(runrate / item.Target); // old

                                var runrate = Convert.ToDouble(totalKpi + ((totalKpi / 5) * remainingDays));
                                var runrate2 = Convert.ToDouble(runrate / item.Target);

                                NewDB.Add(new ResponseDTO.ReportResponse
                                {
                                    name = item.Firstname,
                                    status = "",
                                    total_lead = item.Target.ToString(),
                                    //not_interested = aa.Count().ToString(), // old
                                    not_interested = totalKpi.ToString(),
                                    screen_noshow = screen_noshow.ToString(),
                                    //screen_notpass = today.Day.ToString(), //old
                                    screen_notpass = countDays.ToString(),
                                    client_noshow = remainingDays.ToString(),
                                    client_notpass = runrate.ToString("F2"),
                                    notjoin = runrate2.ToString("F2") + "%",
                                    dropout = null,
                                    success_kpi = null,
                                    notpass_guarantee = null
                                });
                            }
                            else if (a == 1)
                            {
                                var GetDBs = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id) && x.UpdatedDate.Value.Month == DateTime.Now.Month && (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id) || (x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id))).ToList();
                                var aa = await _unitOfWork.RecruitStatusRepository.GetAllAsync();

                                if (GetDBs.Count() != 0)
                                {
                                    foreach (var items in GetDBs)
                                    {
                                        var aaa = aa.FirstOrDefault(x => x.Id == items.Status);
                                        var bbb = port.FirstOrDefault(x => x.Id == items.Project);
                                        NewDB.Add(new ResponseDTO.ReportResponse
                                        {
                                            name = "Achieved",
                                            status = "",
                                            total_lead = bbb == null ? null : bbb.Name,
                                            not_interested = items.NameTH,
                                            screen_noshow = aaa.Name,
                                            screen_notpass = null,
                                            client_noshow = null,
                                            client_notpass = null,
                                            notjoin = null,
                                            dropout = null,
                                            success_kpi = null,
                                            notpass_guarantee = null
                                        });
                                    }
                                }
                            }
                            else if (a == 2)
                            {
                                var GetDBs = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id) && x.UpdatedDate.Value.Month == DateTime.Now.Month && (x.Status == 34 || (Waiting_work != null && x.Status == Waiting_work.Id))).ToList();
                                var aa = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
                                if (GetDBs.Count() != 0)
                                {
                                    foreach (var items in GetDBs)
                                    {
                                        var aaa = aa.FirstOrDefault(x => x.Id == items.Status);
                                        var bbb = port.FirstOrDefault(x => x.Id == items.Project);
                                        NewDB.Add(new ResponseDTO.ReportResponse
                                        {
                                            name = "รอเก็บเงิน",
                                            status = "",
                                            total_lead = bbb == null ? null : bbb.Name,
                                            not_interested = items.NameTH,
                                            screen_noshow = aaa.Name,
                                            screen_notpass = null,
                                            client_noshow = null,
                                            client_notpass = null,
                                            notjoin = null,
                                            dropout = null,
                                            success_kpi = null,
                                            notpass_guarantee = null
                                        });
                                    }
                                }
                                else
                                {
                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = "รอเก็บเงิน",
                                        status = "",
                                        total_lead = null,
                                        not_interested = null,
                                        screen_noshow = null,
                                        screen_notpass = null,
                                        client_noshow = null,
                                        client_notpass = null,
                                        notjoin = null,
                                        dropout = null,
                                        success_kpi = null,
                                        notpass_guarantee = null
                                    });
                                }
                            }
                            else if (a == 3)
                            {
                                var GetDBs = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id) && x.UpdatedDate.Value.Month == DateTime.Now.Month && (x.Status == 11 || (Waiting_work != null && x.Status == Waiting_work.Id))).ToList();
                                if (GetDBs.Count() != 0)
                                {
                                    var aa = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
                                    foreach (var items in GetDBs)
                                    {
                                        var aaa = aa.FirstOrDefault(x => x.Id == items.Status);
                                        var bbb = port.FirstOrDefault(x => x.Id == items.Project);
                                        NewDB.Add(new ResponseDTO.ReportResponse
                                        {
                                            name = "รอเริ่มงาน",
                                            status = "",
                                            total_lead = bbb == null ? null : bbb.Name,
                                            not_interested = items.NameTH,
                                            screen_noshow = aaa.Name,
                                            screen_notpass = null,
                                            client_noshow = null,
                                            client_notpass = null,
                                            notjoin = null,
                                            dropout = null,
                                            success_kpi = null,
                                            notpass_guarantee = null
                                        });
                                    }
                                }
                                else
                                {
                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        name = "รอเริ่มงาน",
                                        status = "",
                                        total_lead = null,
                                        not_interested = null,
                                        screen_noshow = null,
                                        screen_notpass = null,
                                        client_noshow = null,
                                        client_notpass = null,
                                        notjoin = null,
                                        dropout = null,
                                        success_kpi = null,
                                        notpass_guarantee = null
                                    });
                                }
                            }
                        }
                    }
                    //}

                    NewDB = NewDB.GroupBy(x => new
                    {
                        x.name,
                        x.status,
                        x.total_lead,
                        x.not_interested,
                        x.screen_noshow,
                        x.screen_notpass,
                        x.client_noshow,
                        x.client_notpass,
                        x.notjoin,
                        x.dropout,
                        x.success_kpi,
                        x.notpass_guarantee
                    }).Select(x => new ReportResponse
                    {
                        name = x.Key.name,
                        status = x.Key.status,
                        total_lead = x.Key.total_lead,
                        not_interested = x.Key.not_interested,
                        screen_noshow = x.Key.screen_noshow,
                        screen_notpass = x.Key.screen_notpass,
                        client_noshow = x.Key.client_noshow,
                        client_notpass = x.Key.client_notpass,
                        notjoin = x.Key.notjoin,
                        dropout = x.Key.dropout,
                        success_kpi = x.Key.success_kpi,
                        notpass_guarantee = x.Key.screen_noshow,
                    }).ToList();
                }
                else if (Type == 41)
                {
                    List<ResponseDTO.UserResponse> new_user = new List<ResponseDTO.UserResponse>();
                    var target = userid == null ? null : userid.Split(',');
                    if (spit != null)
                    {
                        foreach (var item in target)
                        {
                            var where_user = user.Where(x => x.Id != null && x.Id.ToLower() == item.ToLower()).ToList();
                            new_user.AddRange(where_user);
                        }
                    }
                    else
                    {
                        if (roles == "recruiter")
                        {
                            user = user.Where(x => x.Id != null && x.Id.ToLower() == userids.ToLower()).ToList();
                            //var db = await _unitOfWork.UserRoleRepository.GetUserRole();
                            //db = db.Where(x => x.RoleId.ToLower() == "b68ff80f-5702-4cb5-a6bd-b50ed58c8068").ToList();
                            //foreach (var item in db)
                            //{
                            //    var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.UserId.ToLower()).ToList();
                            //    NewCDD.AddRange(where_user);
                            //}
                        }
                    }

                    if (new_user.Count() != 0)
                    {
                        user = new_user;
                    }

                    var port = await _unitOfWork.ProjectRepository.GetAllAsync();
                    var Rule_ = user.Where(x => x.Permission == "recruiter").ToList();
                    foreach (var item in Rule_)
                    {
                        for (var a = 0; a < 4; a++)
                        {
                            if (a == 0)
                            {
                                //old
                                //var WCDD = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id)
                                //&& x.UpdatedDate.Value.Month == DateTime.Now.Month
                                //&& ((x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38)
                                //|| (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id))
                                //|| x.Status == 32).ToList();
                                //// end old

                                var S = Convert.ToDateTime(StartDate, new CultureInfo("en-US"));
                                var E = Convert.ToDateTime(EndDate, new CultureInfo("en-US"));
                                var WCDD = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id)
                               && (x.Datestatus != null && x.Datestatus.Value.Date >= S.Date && x.Datestatus.Value.Date <= E.Date)
                               && ((x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38))
                               || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)
                               || x.Status == 32).ToList();

                                //var totalKpi = WCDD.Sum(x => string.IsNullOrEmpty(x.KPI) ? 0m : Convert.ToDecimal(x.KPI)); //old
                                var totalKpi = WCDD.Count();

                                DateTime today = DateTime.Today;
                                int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
                                int remainingDays = daysInMonth - today.Day;

                                //var aaa = Convert.ToDouble(WCDD.Count()) / item.Target;
                                //string screen_noshow = WCDD.Count() == 0 || item.Target == 0 ? "0" : (((double)WCDD.Count() / (double)item.Target) * 100).ToString("F2"); // old
                                string screen_noshow = WCDD.Count() == 0 || item.Target == 0 ? "0" : (((double)totalKpi / (double)item.Target) * 100).ToString("F2");  //update by sai mon
                                NewDB.Add(new ResponseDTO.ReportResponse
                                {
                                    name = item.Firstname,
                                    status = "",
                                    total_lead = item.Target.ToString(),
                                    not_interested = totalKpi.ToString(),
                                    screen_noshow = screen_noshow + " %",
                                    screen_notpass = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id)
                                                    && x.UpdatedDate.Value.Month == DateTime.Now.Month
                                                    && (x.Status == 34 || (Waiting_work != null
                                                    && x.Status == Waiting_work.Id))).ToList().Count().ToString(),
                                    client_noshow = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id)
                                                    && x.UpdatedDate.Value.Month == DateTime.Now.Month
                                                    && (x.Status == 11 || (Waiting_work != null && x.Status == Waiting_work.Id)))
                                                    .ToList().Count().ToString(),
                                    client_notpass = null,
                                    notjoin = null,
                                    dropout = null,
                                    success_kpi = null,
                                    notpass_guarantee = null
                                });
                            }
                            else if (a == 1)
                            {
                                var GetDBs = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id) && x.UpdatedDate.Value.Month == DateTime.Now.Month && ((x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)) || x.Status == 32).ToList();
                                var aa = await _unitOfWork.RecruitStatusRepository.GetAllAsync();

                                if (GetDBs.Count() != 0)
                                {
                                    foreach (var items in GetDBs)
                                    {
                                        var aaa = aa.FirstOrDefault(x => x.Id == items.Status);
                                        var bbb = port.FirstOrDefault(x => x.Id == items.Project);
                                        NewDB.Add(new ResponseDTO.ReportResponse
                                        {
                                            //index = (i++),
                                            name = "Achieved", //CDDName = user == null ? null : user.FirstOrDefault(x => x.Id == item.Key.UserId).Name,
                                            status = "",
                                            total_lead = bbb == null ? null : bbb.Name,
                                            not_interested = items.NameTH,
                                            screen_noshow = aaa.Name,
                                            screen_notpass = null,
                                            client_noshow = null,
                                            client_notpass = null,
                                            notjoin = null,
                                            dropout = null,
                                            success_kpi = null,
                                            notpass_guarantee = null
                                        });
                                    }
                                }
                            }
                            else if (a == 2)
                            {
                                var GetDBs = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id) && x.UpdatedDate.Value.Month == DateTime.Now.Month && (x.Status == 34 || (Waiting_work != null && x.Status == Waiting_work.Id))).ToList();
                                var aa = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
                                if (GetDBs.Count() != 0)
                                {
                                    foreach (var items in GetDBs)
                                    {
                                        var aaa = aa.FirstOrDefault(x => x.Id == items.Status);
                                        var bbb = port.FirstOrDefault(x => x.Id == items.Project);
                                        NewDB.Add(new ResponseDTO.ReportResponse
                                        {
                                            //index = (i++),
                                            name = "รอเก็บเงิน", //CDDName = user == null ? null : user.FirstOrDefault(x => x.Id == item.Key.UserId).Name,
                                            status = "",
                                            total_lead = bbb == null ? null : bbb.Name,
                                            not_interested = items.NameTH,
                                            screen_noshow = aaa.Name,
                                            screen_notpass = null,
                                            client_noshow = null,
                                            client_notpass = null,
                                            notjoin = null,
                                            dropout = null,
                                            success_kpi = null,
                                            notpass_guarantee = null
                                        });
                                    }
                                }
                                else
                                {
                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        //index = (i++),
                                        name = "รอเก็บเงิน",
                                        status = "",
                                        total_lead = null,
                                        not_interested = null,
                                        screen_noshow = null,
                                        screen_notpass = null,
                                        client_noshow = null,
                                        client_notpass = null,
                                        notjoin = null,
                                        dropout = null,
                                        success_kpi = null,
                                        notpass_guarantee = null
                                    });
                                }
                            }
                            else if (a == 3)
                            {
                                var GetDBs = GetDB.Where(x => (x.UserId != null && x.UserId == item.Id) && x.UpdatedDate.Value.Month == DateTime.Now.Month && (x.Status == 11 || (Waiting_work != null && x.Status == Waiting_work.Id))).ToList();
                                if (GetDBs.Count() != 0)
                                {
                                    var aa = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
                                    foreach (var items in GetDBs)
                                    {
                                        var aaa = aa.FirstOrDefault(x => x.Id == items.Status);
                                        var bbb = port.FirstOrDefault(x => x.Id == items.Project);
                                        NewDB.Add(new ResponseDTO.ReportResponse
                                        {
                                            //index = (i++),
                                            name = "รอเริ่มงาน", //CDDName = user == null ? null : user.FirstOrDefault(x => x.Id == item.Key.UserId).Name,
                                            status = "",
                                            total_lead = bbb == null ? null : bbb.Name,
                                            not_interested = items.NameTH,
                                            screen_noshow = aaa.Name,
                                            screen_notpass = null,
                                            client_noshow = null,
                                            client_notpass = null,
                                            notjoin = null,
                                            dropout = null,
                                            success_kpi = null,
                                            notpass_guarantee = null
                                        });
                                    }
                                }
                                else
                                {
                                    NewDB.Add(new ResponseDTO.ReportResponse
                                    {
                                        //index = (i++),
                                        name = "รอเริ่มงาน", //CDDName = user == null ? null : user.FirstOrDefault(x => x.Id == item.Key.UserId).Name,
                                        status = "",
                                        total_lead = null,
                                        not_interested = null,
                                        screen_noshow = null,
                                        screen_notpass = null,
                                        client_noshow = null,
                                        client_notpass = null,
                                        notjoin = null,
                                        dropout = null,
                                        success_kpi = null,
                                        notpass_guarantee = null
                                    });
                                }
                            }
                        }
                    }
                    //}

                    NewDB = NewDB.GroupBy(x => new
                    {
                        x.name,
                        x.status,
                        x.total_lead,
                        x.not_interested,
                        x.screen_noshow,
                        x.screen_notpass,
                        x.client_noshow,
                        x.client_notpass,
                        x.notjoin,
                        x.dropout,
                        x.success_kpi,
                        x.notpass_guarantee
                    }).Select(x => new ReportResponse
                    {
                        name = x.Key.name,
                        status = x.Key.status,
                        total_lead = x.Key.total_lead,
                        not_interested = x.Key.not_interested,
                        screen_noshow = x.Key.screen_noshow,
                        screen_notpass = x.Key.screen_notpass,
                        client_noshow = x.Key.client_noshow,
                        client_notpass = x.Key.client_notpass,
                        notjoin = x.Key.notjoin,
                        dropout = x.Key.dropout,
                        success_kpi = x.Key.success_kpi,
                        notpass_guarantee = x.Key.screen_noshow,
                    }).ToList();
                }

                var jsonData = new { data = NewDB };
                return new JsonResult(jsonData);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetProject()
        {
            var db = await _unitOfWork.ProjectRepository.GetAllAsync();
            db = db.Where(x => x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetUser()
        {
            var db = await _unitOfWork.UserRoleRepository.GetUserRole();
            var (userid, roles, _, department) = User.GetUser();
            //if (roles == "recruiter")
            //{
            db = db.Where(x => x.RoleId.ToLower() == "b68ff80f-5702-4cb5-a6bd-b50ed58c8068").ToList();
            //}
            db = db.Where(x => x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        public async Task<IActionResult> OnGetUserProjectowner()
        {
            var db = await _unitOfWork.UserRoleRepository.GetUserRole();
            var (userid, roles, _, department) = User.GetUser();
            //if (roles == "recruiter")
            //{
            db = db.Where(x => x.RoleId.ToLower() == "97f7332b-04bf-4c53-b159-f39bfd511f44").ToList();
            //}
            db = db.Where(x => x.DeleteAt != 1).ToList();
            return new JsonResult(db);
        }

        public async Task<IActionResult> OnGetRecruitmentStatus()
        {
            var db = await _unitOfWork.RecruitStatusRepository.GetAllAsync();

            db = db.Where(x => x.DeleteAt != 1).ToList();
            return new JsonResult(db);
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
        public async Task<IActionResult> OnGetChartData(int? Project, string? start, string? to, int? type, string? userid, int? Status)
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

                var fixDate = DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null);
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
                    }
                }

                if (NewCDD.Count() != 0)
                {
                    DB = NewCDD;
                }

                var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                if (roles == "project owner")
                {
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
                    //DB = DB.Where(x => x.CreatedDate.Value.Date <= Convert.ToDateTime(to).Date).ToList();//old 
                    DB = DB.Where(x => x.Datestatus != null && x.Datestatus.Value.Date <= Convert.ToDateTime(to).Date).ToList();// change by sai mon
                }

                if (Status != null && Status != -1)
                {
                    DB = DB.Where(x => x.Status == Status).ToList();
                }


                if (start == null || start == "")
                {
                    //DB = DB.Where(x => x.DeleteAt != 1 && (x.CreatedDate.Value.Month == DateTime.Now.Month && x.CreatedDate.Value.Year == DateTime.Now.Year)).ToList();
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

                List<ShowChart> ShowChart = new List<ShowChart>();

                if (type == 1)
                {
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

                                DetailChart.Add(new ReportModel.DetailChart
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

                            ShowChart.Add(new ReportModel.ShowChart
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
                        if (aawewe != null)
                        {
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
                    }
                    return new JsonResult(new { detail, name, data, data2, data3, data4, data5, data6, data7, data8, data9, data10 });
                }
                else if (type == 21)
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

                    for (var a = 0; a < 3; a++)
                    {
                        if (a == 0)
                        {
                            detail.Add("Amount");
                            for (var i = 0; i < 10; i++)
                            {
                                if (i == 0)
                                {
                                    name.Add("Total Lead");
                                    var Count = DB.Count();
                                    data.Add(Count.ToString());
                                }
                                else if (i == 1)
                                {
                                    name.Add("Not interested (ไม่สนใจ)");
                                    var Count = DB.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                    data2.Add(Count.ToString());
                                }
                                else if (i == 2)
                                {
                                    name.Add("Screen - No Show (ไม่มาสกรีน)");
                                    var Count = DB.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                    data3.Add(Count.ToString());
                                }
                                else if (i == 3)
                                {
                                    name.Add("Screen - Not Pass (ไม่ผ่านสกรีน)");
                                    var Count = DB.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                    data4.Add(Count.ToString());
                                }
                                else if (i == 4)
                                {
                                    name.Add("Client - No Show (ไม่มาสัม)");
                                    var Count = DB.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                    data5.Add(Count.ToString());
                                }
                                else if (i == 5)
                                {
                                    name.Add("Client - Not Pass (ไม่ผ่านสัม)");
                                    var Count = DB.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                    data6.Add(Count.ToString());
                                }
                                else if (i == 6)
                                {
                                    name.Add("Not join (ไม่ไปเริ่มงาน)");
                                    var Count = DB.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                    data7.Add(Count.ToString());
                                }
                                else if (i == 7)
                                {
                                    name.Add("Drop out (ไปเริ่มงานแล้ว แต่ออกก่อนเก็บเงิน)");
                                    var Count = DB.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                    data8.Add(Count.ToString());
                                }
                                else if (i == 8)
                                {
                                    name.Add("Success KPI (เก็บเงินได้)");
                                    var Count = DB.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                    data9.Add(Count.ToString());
                                }
                                else if (i == 9)
                                {
                                    name.Add("Not pass guarantee - Replacement");
                                    var Count = DB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    data10.Add(Count.ToString());
                                }
                            }
                        }
                        else if (a == 1)
                        {
                            detail.Add("หายไป");
                            var Count = DB.Count();
                            for (var i = 0; i < 8; i++)
                            {
                                if (i == 0)
                                {
                                    data.Add(Count.ToString());
                                }
                                else if (i == 1)
                                {
                                    var Count2 = DB.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data2.Add(Count.ToString());
                                }
                                else if (i == 2)
                                {
                                    var Count2 = DB.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data3.Add(Count.ToString());
                                }
                                else if (i == 3)
                                {
                                    var Count2 = DB.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data4.Add(Count.ToString());
                                }
                                else if (i == 4)
                                {
                                    var Count2 = DB.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data5.Add(Count.ToString());
                                }
                                else if (i == 5)
                                {
                                    var Count2 = DB.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data6.Add(Count.ToString());
                                }
                                else if (i == 6)
                                {
                                    var Count2 = DB.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data7.Add(Count.ToString());
                                }
                                else if (i == 7)
                                {
                                    var Count2 = DB.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data8.Add(Count.ToString());
                                }
                                else if (i == 7)
                                {
                                    var Count2 = DB.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data9.Add(Count.ToString());
                                }
                                else if (i == 7)
                                {
                                    var Count2 = DB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    Count = Count - Count2;
                                    data10.Add(Count.ToString());
                                }
                            }
                        }
                        else if (a == 2)
                        {
                            detail.Add("% หายไป");
                            decimal Count = DB.Count();
                            for (var i = 0; i < 8; i++)
                            {
                                if (i == 0)
                                {
                                    data.Add(Count.ToString());
                                }
                                else if (i == 1)
                                {
                                    var Count2 = DB.Where(x => x.Status == 1 || (status_not_interested != null && x.Status == status_not_interested.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data2.Add(Count.ToString());
                                }
                                else if (i == 2)
                                {
                                    var Count2 = DB.Where(x => x.Status == 36 || (status_Not_coming_to_screen != null && x.Status == status_Not_coming_to_screen.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data3.Add(Count.ToString());
                                }
                                else if (i == 3)
                                {
                                    var Count2 = DB.Where(x => (x.Status == 8 || x.Status == 7) || (status_Did_not_pass_the_screen != null && x.Status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data4.Add(Count.ToString());
                                }
                                else if (i == 4)
                                {
                                    var Count2 = DB.Where(x => x.Status == 12 || (status_Didnt_come_to_the_interview != null && x.Status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data5.Add(Count.ToString());
                                }
                                else if (i == 5)
                                {
                                    var Count2 = DB.Where(x => (x.Status == 17 || x.Status == 9) || (status_Didnt_pass_the_interview != null && x.Status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data6.Add(Count.ToString());
                                }
                                else if (i == 6)
                                {
                                    var Count2 = DB.Where(x => x.Status == 20 || (status_Not_going_to_start_work != null && x.Status == status_Not_going_to_start_work.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data7.Add(Count.ToString());
                                }
                                else if (i == 7)
                                {
                                    var Count2 = DB.Where(x => x.Status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.Status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data8.Add(Count.ToString());
                                }
                                else if (i == 7)
                                {
                                    var Count2 = DB.Where(x => (x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) || (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data9.Add(Count.ToString());
                                }
                                else if (i == 7)
                                {
                                    var Count2 = DB.Where(x => x.Status == 32 || (status_Exit_before_the_guarantee_expires != null && x.Status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                                    if (Count2 > 0)
                                        Count = Math.Round(Count2 / Count * 100, 2);
                                    data10.Add(Count.ToString());
                                }
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
                    #endregion

                    //#region Filter by recruiter role // close by sai mon 06/11/2025
                    //if (roles == "recruiter")
                    //{
                    //    var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);
                    //    DB = DB.Where(x => x.Project == where_project.Project).ToList();
                    //}
                    //#endregion

                    var CheckProject = DB.Where(x => x.UserId != "b8964f16-7c2e-4714-978d-3de5534a6e8b").ToList();
                    var GroupProject = CheckProject.GroupBy(x => x.Project).ToList();

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
                                var projectId = group.Key;
                                name.Add(DBProject.FirstOrDefault(x => x.Id == projectId)?.Name ?? "-");
                                //var countKPI = Count.Count(x => x.Project == projectId); //old
                                var countKPI = group.Where(x => x.Project == projectId).Count();
                                data.Add(countKPI.ToString());
                            }
                        }
                        else if (a == 1)
                        {
                            detail.Add("Total Replace");
                            //var Count = CheckProject.Where(x => x.Status == 35).ToList(); // old
                            var Count = CheckProject.Where(x => x.Status == 32).ToList();
                            foreach (var group in GroupProject)
                            {
                                var projectId = group.Key;
                                //var countReplace = Count.Count(x => x.Project == projectId);//old
                                var countReplace = group.Where(x => x.Project == projectId).Count();
                                data2.Add(countReplace.ToString());
                            }
                        }
                        else if (a == 2)
                        {
                            detail.Add("% Replace");
                            foreach (var group in GroupProject)
                            {
                                var projectId = group.Key;
                                var countKPI = CheckProject.Where(x => x.Project == projectId &&
                                    ((x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38) ||
                                     (status_I_have_already_collected_the_money != null && x.Status == status_I_have_already_collected_the_money.Id))).Count();

                                //var countKPI = CheckProject.Count(); //change by sai mon

                                var countReplace = CheckProject.Where(x => x.Project == projectId && x.Status == 32).Count();

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
                    #endregion

                    var total = DB.Where(x => x.UpdatedDate.Value.Month == DateTime.Now.Month
                    && ((x.Status == 24 || x.Status == 25 || x.Status == 27 || x.Status == 28 || x.Status == 30 || x.Status == 37 || x.Status == 38)
                    || (status_I_have_already_collected_the_money != null
                    && x.Status == status_I_have_already_collected_the_money.Id))
                    || x.Status == 32).ToList(); //DB.Where(x => x.UpdatedDate.Value.Month == DateTime.Now.Month).ToList(); //&& x.Status == 22 || x.Status == 11
                    
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

        //[HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnGetExport_PerFormanceReportRC(int? Project, string? StartDate, string? EndDate, string? userid, int? Status)
        {
            try
            {
                var (userids, roles, _, department) = User.GetUser();
                var FileName = DateTime.Now.Ticks + ".xlsx";
                var NewPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Export/" + FileName);

                Stream stream = new MemoryStream();
                using (var package = new ExcelPackage(new FileInfo(NewPath)))
                {
                    ExcelWorksheet Sheet = package.Workbook.Worksheets.Add("Performance Report - RC");

                    #region ตาราง
                    Sheet.Cells["A1"].Value = "#";
                    Sheet.Cells["B1"].Value = "Name";
                    Sheet.Cells["C1"].Value = "Status";
                    Sheet.Cells["D1"].Value = "Total Lead";
                    Sheet.Cells["E1"].Value = "Not interested (ไม่สนใจ)";
                    Sheet.Cells["F1"].Value = "Screen - No Show (ไม่มาสกรีน)";
                    Sheet.Cells["G1"].Value = "Screen - Not Pass (ไม่ผ่านสกรีน)";
                    Sheet.Cells["H1"].Value = "Client - No Show (ไม่มาสัม)";
                    Sheet.Cells["I1"].Value = "Client - Not Pass (ไม่สัม)";
                    Sheet.Cells["J1"].Value = "Not join (ไม่ไปเริ่มงาน)";
                    Sheet.Cells["K1"].Value = "Drop out (ไปเริ่มงานแล้ว แต่ออกก่อนเก็บเงิน)";
                    Sheet.Cells["L1"].Value = "Success KPI (เก็บเงินได้)";
                    Sheet.Cells["M1"].Value = "Not pass guarantee - Replacement (ออกก่อนพ้นการันตี)";
                    #endregion

                    var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();

                    var fixDate = DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null);
                    GetDB = GetDB.Where(x => x.DeleteAt != 1 && x.CreatedDate != null && x.CreatedDate.Value.Date >= fixDate.Date).ToList();

                    if (StartDate == null || StartDate == "")
                    {
                        //GetDB = GetDB.Where(x => x.DeleteAt != 1 && (x.CreatedDate.Value.Month == DateTime.Now.Month && x.CreatedDate.Value.Year == DateTime.Now.Year)).ToList();
                    }

                    List<CDD> NewCDD = new List<CDD>();
                    //List<CDD> NewCDD2 = new List<CDD>();
                    var spit = userid == null ? null : userid.Split(',');
                    if (spit != null)
                    {
                        foreach (var item in spit)
                        {
                            var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.ToLower()).ToList();
                            NewCDD.AddRange(where_user);
                        }
                    }
                    else
                    {

                        if (roles == "recruiter")
                        {
                            NewCDD = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == userids.ToLower()).ToList();
                            //var db = await _unitOfWork.UserRoleRepository.GetUserRole();
                            //db = db.Where(x => x.RoleId.ToLower() == "b68ff80f-5702-4cb5-a6bd-b50ed58c8068").ToList();
                            //foreach (var item in db)
                            //{
                            //    var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.UserId.ToLower()).ToList();
                            //    NewCDD.AddRange(where_user);
                            //}
                        }
                    }

                    //if (NewCDD.Count() != 0)
                    //{
                    GetDB = NewCDD;
                    //}

                    if (roles == "project owner")
                    {
                        var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                        var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);
                        Project = where_project.Project;
                    }

                    //var spit_projectownerid = projectownerid == null ? null : projectownerid.Split(',');
                    //if(spit_projectownerid != null)
                    //{ 
                    //    var getuser = await _unitOfWork.UsersRepository.GetAllAsync();

                    //    foreach (var item in spit_projectownerid)
                    //    {
                    //        var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == item.ToLower());
                    //        var where_user = GetDB.Where(x => x.Project != null && x.Project == where_project.Project).ToList();
                    //        NewCDD2.AddRange(where_user);
                    //    }
                    //}

                    //if (NewCDD2.Count() != 0)
                    //{
                    //    GetDB = NewCDD2;
                    //}

                    var NewDB = GetDB.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.ReportResponseExport
                    {
                        id = x.Id,
                        userId = x.UserId,
                        name = x.NameTH,
                        nickname = x.Nikname,
                        tel = x.Tel,
                        project = x.Project,
                        kpi = x.KPI,
                        status = x.Status,
                        Date = x.CreatedDate
                    }).ToList();

                    if (Project != null && Project != -1)
                    {
                        NewDB = NewDB.Where(x => x.project == Project).ToList();
                    }

                    if (StartDate != null)
                    {
                        NewDB = NewDB.Where(x => x.Date >= Convert.ToDateTime(StartDate).Date).ToList();
                    }

                    if (EndDate != null)
                    {
                        NewDB = NewDB.Where(x => x.Date <= Convert.ToDateTime(EndDate)).ToList();
                    }

                    if (Status != null && Status != -1)
                    {
                        NewDB = NewDB.Where(x => x.status == Status).ToList();
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

                    int? index = 2;
                    var i = 0;
                    var Group = NewDB.Where(x => x.userId != null).GroupBy(x => new { x.userId }).ToList();
                    var DBUser = await _unitOfWork.UsersRepository.GetAllAsync();
                    foreach (var item in Group)
                    {
                        var Find = NewDB.Where(x => (x.userId != null && item.Key.userId != null) && x.userId.ToLower() == item.Key.userId.ToLower()).ToList();

                        Sheet.Cells["A" + index + ":A" + (index + 4)].Merge = true;
                        Sheet.Cells["A" + index].Value = (i = i + 1);
                        Sheet.Cells["B" + index + ":B" + (index + 4)].Merge = true;

                        var GetUser = DBUser.FirstOrDefault(x => (x.Id != null && item.Key.userId != null) && x.Id.ToLower() == item.Key.userId.ToLower());
                        var wwew = (GetUser == null ? null : GetUser.Firstname) + " " + (GetUser == null ? null : GetUser.Lastname);
                        Sheet.Cells["B" + index].Value = wwew;

                        for (var a = 0; a <= 4; a++)
                        {
                            if (a == 0)
                            {
                                Sheet.Cells["C" + index].Value = "จำนวน";
                                Sheet.Cells["D" + index].Value = Find.Count() == 0 ? null : Find.Count();
                                Sheet.Cells["E" + index].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 1 || (status_not_interested != null && x.status == status_not_interested.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 1 || (status_not_interested != null && x.status == status_not_interested.Id)).ToList().Count();
                                Sheet.Cells["F" + index].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 36 || (status_Not_coming_to_screen != null && x.status == status_Not_coming_to_screen.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 36 || (status_Not_coming_to_screen != null && x.status == status_Not_coming_to_screen.Id)).ToList().Count();
                                Sheet.Cells["G" + index].Value = Find.Count() == 0 ? null : Find.Where(x => (x.status == 8 || x.status == 7) || (status_Did_not_pass_the_screen != null && x.status == status_Did_not_pass_the_screen.Id)).ToList().Count() == 0 ? null : Find.Where(x => (x.status == 8 || x.status == 7) || (status_Did_not_pass_the_screen != null && x.status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                Sheet.Cells["H" + index].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 12 || (status_Didnt_come_to_the_interview != null && x.status == status_Didnt_come_to_the_interview.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 12 || (status_Didnt_come_to_the_interview != null && x.status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                Sheet.Cells["I" + index].Value = Find.Count() == 0 ? null : Find.Where(x => (x.status == 17 || x.status == 9) || (status_Didnt_pass_the_interview != null && x.status == status_Didnt_pass_the_interview.Id)).ToList().Count() == 0 ? null : Find.Where(x => (x.status == 17 || x.status == 9) || (status_Didnt_pass_the_interview != null && x.status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                Sheet.Cells["J" + index].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 20 || (status_Not_going_to_start_work != null && x.status == status_Not_going_to_start_work.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 20 || (status_Not_going_to_start_work != null && x.status == status_Not_going_to_start_work.Id)).ToList().Count();
                                Sheet.Cells["K" + index].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                Sheet.Cells["L" + index].Value = Find.Count() == 0 ? null : Find.Where(x => (x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id)).ToList().Count() == 0 ? null : Find.Where(x => (x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                Sheet.Cells["M" + index].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 32 || (status_Exit_before_the_guarantee_expires != null && x.status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 32 || (status_Exit_before_the_guarantee_expires != null && x.status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                            }
                            else if (a == 1)
                            {
                                var loop1 = index - 1;
                                var loop11 = loop1 + 2;
                                Sheet.Cells["C" + index].Value = "%";
                                Sheet.Cells["D" + index].Value = null;

                                Sheet.Cells["E" + index].Formula = "=E" + loop1 + "/D" + loop1 + "";
                                Sheet.Cells["E" + index].Style.Numberformat.Format = "0.00";

                                Sheet.Cells["F" + index].Formula = "=F" + loop1 + "/E" + loop11 + "";
                                Sheet.Cells["F" + index].Style.Numberformat.Format = "0.00";

                                Sheet.Cells["G" + index].Formula = "=G" + loop1 + "/F" + loop11 + "";
                                Sheet.Cells["G" + index].Style.Numberformat.Format = "0.00";

                                Sheet.Cells["H" + index].Formula = "=H" + loop1 + "/G" + loop11 + "";
                                Sheet.Cells["H" + index].Style.Numberformat.Format = "0.00";

                                Sheet.Cells["I" + index].Formula = "=I" + loop1 + "/H" + loop11 + "";
                                Sheet.Cells["I" + index].Style.Numberformat.Format = "0.00";

                                Sheet.Cells["J" + index].Formula = "=J" + loop1 + "/I" + loop11 + "";
                                Sheet.Cells["J" + index].Style.Numberformat.Format = "0.00";

                                Sheet.Cells["K" + index].Formula = "=K" + loop1 + "/J" + loop11 + "";
                                Sheet.Cells["K" + index].Style.Numberformat.Format = "0.00";

                                Sheet.Cells["L" + index].Formula = "=L" + loop1 + "/K" + loop11 + "";
                                Sheet.Cells["L" + index].Style.Numberformat.Format = "0.00";
                            }
                            else if (a == 2)
                            {
                                var loop2 = index - 2;
                                var loop22 = loop2 + 2;
                                Sheet.Cells["C" + index].Value = "เหลือ";
                                Sheet.Cells["D" + index].Value = null;
                                Sheet.Cells["E" + index].Formula = "=D" + loop2 + "-E" + loop2 + "";
                                Sheet.Cells["F" + index].Formula = "=E" + loop22 + "-F" + loop2 + "";
                                Sheet.Cells["G" + index].Formula = "=F" + loop22 + "-G" + loop2 + "";
                                Sheet.Cells["H" + index].Formula = "=G" + loop22 + "-H" + loop2 + "";
                                Sheet.Cells["I" + index].Formula = "=H" + loop22 + "-I" + loop2 + "";
                                Sheet.Cells["J" + index].Formula = "=I" + loop22 + "-J" + loop2 + "";
                                Sheet.Cells["K" + index].Formula = "=J" + loop22 + "-K" + loop2 + "";
                                Sheet.Cells["L" + index].Formula = "=K" + loop22 + "-L" + loop2 + "";
                                Sheet.Cells["M" + index].Formula = "=L" + loop22 + "-M" + loop2 + "";
                            }
                            else if (a == 3)
                            {
                                var loop3 = index - 3;
                                var loop33 = loop3 + 2;
                                Sheet.Cells["C" + index].Value = "% ในการเก็บเงินได้";
                                Sheet.Cells["D" + index].Value = null;
                                Sheet.Cells["E" + index].Formula = "=L" + loop3 + "/E" + loop33 + "";
                                Sheet.Cells["F" + index].Formula = "=L" + loop3 + "/F" + loop33 + "";
                                Sheet.Cells["G" + index].Formula = "=L" + loop3 + "/G" + loop33 + "";
                                Sheet.Cells["H" + index].Formula = "=L" + loop3 + "/H" + loop33 + "";
                                Sheet.Cells["I" + index].Formula = "=L" + loop3 + "/I" + loop33 + "";
                                Sheet.Cells["J" + index].Formula = "=L" + loop3 + "/J" + loop33 + "";
                                Sheet.Cells["K" + index].Formula = "=L" + loop3 + "/K" + loop33 + "";
                                Sheet.Cells["L" + index].Formula = "=L" + loop3 + "/L" + loop33 + "";
                                Sheet.Cells["M" + index].Formula = "=L" + loop3 + "/M" + loop33 + "";
                            }
                            else if (a == 4)
                            {
                                var loop4 = index - 4;
                                Sheet.Cells["C" + index].Value = "% ไม่พ้น replace";
                                Sheet.Cells["D" + index].Formula = "=L" + loop4 + "/M" + loop4 + "";
                            }
                            index++;
                        }
                    }

                    Sheet.Cells["A:M"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A:M"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Sheet.Cells["A:M"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:M"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:M"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:M"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:M"].AutoFitColumns();
                    package.SaveAs(NewPath);
                    stream.Position = 0;

                }
                return new JsonResult(new { fileUrl = FileName });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        //[HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnGetExport_PerFormanceReportPort(int? Project, string? StartDate, string? EndDate, string? userid, int? Status)
        {
            try
            {
                var FileName = DateTime.Now.Ticks + ".xlsx";
                var NewPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Export/" + FileName);
                var (userids, roles, _, department) = User.GetUser();

                Stream stream = new MemoryStream();
                var project = await _unitOfWork.ProjectRepository.GetAllAsync();
                var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();

                var fixDate = DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null);
                GetDB = GetDB.Where(x => x.DeleteAt != 1 && x.CreatedDate != null && x.CreatedDate.Value.Date >= fixDate.Date).ToList();

                List<CDD> NewCDD = new List<CDD>();
                var spit = userid == null ? null : userid.Split(',');
                if (spit != null)
                {
                    foreach (var item in spit)
                    {
                        var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.ToLower()).ToList();
                        NewCDD.AddRange(where_user);
                    }
                }
                else
                {

                    if (roles == "recruiter")
                    {
                        NewCDD = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == userids.ToLower()).ToList();

                        //var db = await _unitOfWork.UserRoleRepository.GetUserRole();
                        //db = db.Where(x => x.RoleId.ToLower() == "b68ff80f-5702-4cb5-a6bd-b50ed58c8068").ToList();
                        //foreach (var item in db)
                        //{
                        //    var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.UserId.ToLower()).ToList();
                        //    NewCDD.AddRange(where_user);
                        //}
                    }
                }

                //if (NewCDD.Count() != 0)
                //{
                GetDB = NewCDD;
                //}

                if (roles == "project owner")
                {
                    var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                    var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);
                    Project = where_project.Project;
                }

                if (StartDate == null || StartDate == "")
                {
                    //GetDB = GetDB.Where(x => x.DeleteAt != 1 && (x.CreatedDate.Value.Month == DateTime.Now.Month && x.CreatedDate.Value.Year == DateTime.Now.Year)).ToList();
                }

                var NewDB = GetDB.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.ReportResponseExport
                {
                    id = x.Id,
                    userId = x.UserId,
                    name = x.NameTH,
                    nickname = x.Nikname,
                    tel = x.Tel,
                    project = x.Project,
                    kpi = x.KPI,
                    status = x.Status,
                    Date = x.CreatedDate
                }).ToList();

                if (Project != null && Project != -1)
                {
                    NewDB = NewDB.Where(x => x.project == Project).ToList();
                }

                if (StartDate != null)
                {
                    NewDB = NewDB.Where(x => x.Date >= Convert.ToDateTime(StartDate).Date).ToList();
                }

                if (EndDate != null)
                {
                    NewDB = NewDB.Where(x => x.Date <= Convert.ToDateTime(EndDate)).ToList();
                }

                if (Status != null && Status != -1)
                {
                    NewDB = NewDB.Where(x => x.status == Status).ToList();
                }

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(NewPath)))
                {
                    ExcelWorksheet Sheet2 = package.Workbook.Worksheets.Add("ตารางสรุปแต่ละพอร์ท");
                    ExcelWorksheet Sheet = package.Workbook.Worksheets.Add("ตารางสรุป Allocation");

                    #region Sheet 1
                    Sheet2.Cells["A1"].Value = "#";
                    Sheet2.Cells["B1"].Value = "Name";
                    Sheet2.Cells["C1"].Value = "Status";
                    Sheet2.Cells["D1"].Value = "Total Lead";
                    Sheet2.Cells["E1"].Value = "Not Interested (ไม่สนใจ)";
                    Sheet2.Cells["F1"].Value = "Screen - No Show (ไม่มาสกรีน)";
                    Sheet2.Cells["G1"].Value = "Screen - Not Pass (ไม่ผ่านสกรีน)";
                    Sheet2.Cells["H1"].Value = "Client - No Show (ไม่มาสัม)";
                    Sheet2.Cells["I1"].Value = "Client - Not Pass (ไม่ผ่านสัม)";
                    Sheet2.Cells["J1"].Value = "Not join (ไม่ไปเริ่มงาน)";
                    Sheet2.Cells["K1"].Value = "Drop out (ไปเริ่มงานแล้ว แต่ออกก่อนเก็บเงิน)";
                    Sheet2.Cells["L1"].Value = "Success KPI (เก็บเงินได้)";
                    Sheet2.Cells["M1"].Value = "Not pass guarantee - Replacement (ออกก่อนพ้นการันตี)";

                    var DBUser = await _unitOfWork.UsersRepository.GetAllAsync();
                    var Group2 = NewDB.Where(x => x.userId != null).GroupBy(x => new { x.userId }).ToList();

                    int? index2 = 2;
                    var i2 = 0;

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


                    foreach (var item in Group2)
                    {
                        var GetUser = DBUser.FirstOrDefault(x => (x.Id != null && item.Key.userId != null) && x.Id.ToLower() == item.Key.userId.ToLower());
                        var Find = NewDB.Where(x => (x.userId != null && item.Key.userId != null) && x.userId.ToLower() == item.Key.userId.ToLower()).ToList();
                        Sheet2.Cells["A" + index2 + ":A" + (index2 + 4)].Merge = true;
                        Sheet2.Cells["A" + index2].Value = (i2 = i2 + 1);
                        Sheet2.Cells["B" + index2 + ":B" + (index2 + 4)].Merge = true;
                        var wwew = (GetUser == null ? null : GetUser.Firstname) + " " + (GetUser == null ? null : GetUser.Lastname);
                        Sheet2.Cells["B" + index2].Value = wwew;

                        for (var a = 0; a <= 4; a++)
                        {
                            if (a == 0)
                            {
                                Sheet2.Cells["C" + index2].Value = "จำนวน";
                                Sheet2.Cells["D" + index2].Value = Find.Count() == 0 ? null : Find.Count();
                                Sheet2.Cells["E" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 1 || (status_not_interested != null && x.status == status_not_interested.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 1 || (status_not_interested != null && x.status == status_not_interested.Id)).ToList().Count();
                                Sheet2.Cells["F" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 36 || (status_Not_coming_to_screen != null && x.status == status_Not_coming_to_screen.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 36 || (status_Not_coming_to_screen != null && x.status == status_Not_coming_to_screen.Id)).ToList().Count();
                                Sheet2.Cells["G" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => (x.status == 8 || x.status == 7) || (status_Did_not_pass_the_screen != null && x.status == status_Did_not_pass_the_screen.Id)).ToList().Count() == 0 ? null : Find.Where(x => (x.status == 8 || x.status == 7) || (status_Did_not_pass_the_screen != null && x.status == status_Did_not_pass_the_screen.Id)).ToList().Count();
                                Sheet2.Cells["H" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 12 || (status_Didnt_come_to_the_interview != null && x.status == status_Didnt_come_to_the_interview.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 12 || (status_Didnt_come_to_the_interview != null && x.status == status_Didnt_come_to_the_interview.Id)).ToList().Count();
                                Sheet2.Cells["I" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => (x.status == 17 || x.status == 9) || (status_Didnt_pass_the_interview != null && x.status == status_Didnt_pass_the_interview.Id)).ToList().Count() == 0 ? null : Find.Where(x => (x.status == 17 || x.status == 9) || (status_Didnt_pass_the_interview != null && x.status == status_Didnt_pass_the_interview.Id)).ToList().Count();
                                Sheet2.Cells["J" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 20 || (status_Not_going_to_start_work != null && x.status == status_Not_going_to_start_work.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 20 || (status_Not_going_to_start_work != null && x.status == status_Not_going_to_start_work.Id)).ToList().Count();
                                Sheet2.Cells["K" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 21 || (status_I_started_working_but_left_before_collecting_the_money != null && x.status == status_I_started_working_but_left_before_collecting_the_money.Id)).ToList().Count();
                                Sheet2.Cells["L" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => (x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id)).ToList().Count() == 0 ? null : Find.Where(x => (x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id)).ToList().Count();
                                Sheet2.Cells["M" + index2].Value = Find.Count() == 0 ? null : Find.Where(x => x.status == 31 || (status_Exit_before_the_guarantee_expires != null && x.status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count() == 0 ? null : Find.Where(x => x.status == 32 || (status_Exit_before_the_guarantee_expires != null && x.status == status_Exit_before_the_guarantee_expires.Id)).ToList().Count();
                            }
                            else if (a == 1)
                            {
                                var loop1 = index2 - 1;
                                var loop11 = loop1 + 2;
                                Sheet2.Cells["C" + index2].Value = "%";
                                Sheet2.Cells["D" + index2].Value = null;

                                Sheet2.Cells["E" + index2].Formula = "=E" + loop1 + "/D" + loop1 + "";
                                Sheet2.Cells["E" + index2].Style.Numberformat.Format = "0.00";

                                Sheet2.Cells["F" + index2].Formula = "=F" + loop1 + "/E" + loop11 + "";
                                Sheet2.Cells["F" + index2].Style.Numberformat.Format = "0.00";

                                Sheet2.Cells["G" + index2].Formula = "=G" + loop1 + "/F" + loop11 + "";
                                Sheet2.Cells["G" + index2].Style.Numberformat.Format = "0.00";

                                Sheet2.Cells["H" + index2].Formula = "=H" + loop1 + "/G" + loop11 + "";
                                Sheet2.Cells["H" + index2].Style.Numberformat.Format = "0.00";

                                Sheet2.Cells["I" + index2].Formula = "=I" + loop1 + "/H" + loop11 + "";
                                Sheet2.Cells["I" + index2].Style.Numberformat.Format = "0.00";

                                Sheet2.Cells["J" + index2].Formula = "=J" + loop1 + "/I" + loop11 + "";
                                Sheet2.Cells["J" + index2].Style.Numberformat.Format = "0.00";

                                Sheet2.Cells["K" + index2].Formula = "=K" + loop1 + "/J" + loop11 + "";
                                Sheet2.Cells["K" + index2].Style.Numberformat.Format = "0.00";

                                Sheet2.Cells["L" + index2].Formula = "=L" + loop1 + "/K" + loop11 + "";
                                Sheet2.Cells["L" + index2].Style.Numberformat.Format = "0.00";
                            }
                            else if (a == 2)
                            {
                                var loop2 = index2 - 2;
                                var loop22 = loop2 + 2;
                                Sheet2.Cells["C" + index2].Value = "เหลือ";
                                Sheet2.Cells["D" + index2].Value = null;
                                Sheet2.Cells["E" + index2].Formula = "=D" + loop2 + "-E" + loop2 + "";
                                Sheet2.Cells["F" + index2].Formula = "=E" + loop22 + "-F" + loop2 + "";
                                Sheet2.Cells["G" + index2].Formula = "=F" + loop22 + "-G" + loop2 + "";
                                Sheet2.Cells["H" + index2].Formula = "=G" + loop22 + "-H" + loop2 + "";
                                Sheet2.Cells["I" + index2].Formula = "=H" + loop22 + "-I" + loop2 + "";
                                Sheet2.Cells["J" + index2].Formula = "=I" + loop22 + "-J" + loop2 + "";
                                Sheet2.Cells["K" + index2].Formula = "=J" + loop22 + "-K" + loop2 + "";
                                Sheet2.Cells["L" + index2].Formula = "=K" + loop22 + "-L" + loop2 + "";
                                Sheet2.Cells["M" + index2].Formula = "=L" + loop22 + "-M" + loop2 + "";
                            }
                            else if (a == 3)
                            {
                                var loop3 = index2 - 3;
                                var loop33 = loop3 + 2;
                                Sheet2.Cells["C" + index2].Value = "% ในการเก็บเงินได้";
                                Sheet2.Cells["D" + index2].Value = null;
                                Sheet2.Cells["E" + index2].Formula = "=L" + loop3 + "/E" + loop33 + "";
                                Sheet2.Cells["E" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["F" + index2].Formula = "=L" + loop3 + "/F" + loop33 + "";
                                Sheet2.Cells["F" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["G" + index2].Formula = "=L" + loop3 + "/G" + loop33 + "";
                                Sheet2.Cells["G" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["H" + index2].Formula = "=L" + loop3 + "/H" + loop33 + "";
                                Sheet2.Cells["H" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["I" + index2].Formula = "=L" + loop3 + "/I" + loop33 + "";
                                Sheet2.Cells["I" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["J" + index2].Formula = "=L" + loop3 + "/J" + loop33 + "";
                                Sheet2.Cells["J" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["K" + index2].Formula = "=L" + loop3 + "/K" + loop33 + "";
                                Sheet2.Cells["K" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["L" + index2].Formula = "=L" + loop3 + "/L" + loop33 + "";
                                Sheet2.Cells["L" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["M" + index2].Formula = "=L" + loop3 + "/M" + loop33 + "";
                                Sheet2.Cells["M" + index2].Style.Numberformat.Format = "0.00";
                            }
                            else if (a == 4)
                            {
                                var loop4 = index2 - 4;
                                Sheet2.Cells["C" + index2].Value = "% ไม่พ้น replace";
                                Sheet2.Cells["D" + index2].Formula = "=L" + loop4 + "/M" + loop4 + "";
                            }
                            index2++;
                        }
                    }

                    Sheet2.Cells["A:AC"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet2.Cells["A:AC"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Sheet2.Cells["A:AC"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Sheet2.Cells["A:AC"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    Sheet2.Cells["A:AC"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Sheet2.Cells["A:AC"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Sheet2.Cells["A:AC"].AutoFitColumns();
                    #endregion

                    #region Sheet 2

                    #region ตาราง
                    Sheet.Cells["A1"].Value = "#";
                    Sheet.Cells["B1"].Value = "Name";
                    Sheet.Cells["C1"].Value = "Status";
                    Sheet.Cells["D1"].Value = "Total Lead";
                    var col = 0;

                    var CheckProject = NewDB.Where(x => x.project != -1 && x.project != null).GroupBy(x => new { x.project }).ToList();
                    for (col = 0; col < CheckProject.Count(); col++)
                    {
                        Sheet.Cells[1, col + 5].Value = project.FirstOrDefault(x => x.Id == CheckProject[col].Key.project).Name;
                    }

                    Sheet.Cells[1, col + 5].Value = "Total All Port";
                    #endregion

                    int index = 2;
                    var i = 0;
                    var Group = NewDB.Where(x => x.userId != null && x.project != -1 && x.project != null).GroupBy(x => new { x.userId }).ToList();
                    foreach (var item2 in Group)
                    {
                        var Find2 = NewDB.Where(x => x.project != null && (x.userId != null && item2.Key.userId != null) && x.userId == item2.Key.userId).ToList();
                        var GetUser2 = DBUser.FirstOrDefault(x => (x.Id != null && item2.Key.userId != null) && x.Id == item2.Key.userId);
                        Sheet.Cells["A" + index + ":A" + (index + 5)].Merge = true;
                        Sheet.Cells["A" + index].Value = (i = i + 1);
                        Sheet.Cells["B" + index + ":B" + (index + 5)].Merge = true;
                        Sheet.Cells["B" + index].Value = GetUser2.Firstname + " " + GetUser2.Lastname;

                        for (var a = 0; a <= 5; a++)
                        {
                            if (a == 0)
                            {
                                Sheet.Cells["C" + index].Value = "Total Lead";
                                Sheet.Cells["C" + index + ":C" + (index + 1)].Merge = true;
                                Sheet.Cells["D" + index].Value = "จำนวน";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    //var FindProject = project.ToList()[col];
                                    var count_project = Find2.Where(x => x.project == CheckProject[col].Key.project).ToList();
                                    Sheet.Cells[index, col + 5].Value = count_project.Count() == 0 ? null : count_project.Count();
                                }
                                Sheet.Cells[index, col + 5].Value = Find2.Count() == 0 ? null : Find2.Count();
                            }
                            else if (a == 1)
                            {
                                Sheet.Cells["D" + index].Value = "% Share";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    var Address = Sheet.Cells[index - 1, col + 5];
                                    Sheet.Cells[index, col + 5].Formula = "=" + Address + "/Z" + (index - 1) + "";
                                    Sheet.Cells[index, col + 5].Style.Numberformat.Format = "0.00";
                                }
                            }
                            else if (a == 2)
                            {
                                Sheet.Cells["C" + index].Value = "Total KPI";
                                Sheet.Cells["C" + index + ":C" + (index + 3)].Merge = true;
                                Sheet.Cells["D" + index].Value = "จำนวน KPI (เก็บเงินได้)";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    //var FindProject = project.ToList()[col];
                                    var count_project = Find2.Where(x => x.project == CheckProject[col].Key.project).Where(x => (x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id)).ToList();
                                    Sheet.Cells[index, col + 5].Value = count_project.Count() == 0 ? null : count_project.Count();
                                }
                            }
                            else if (a == 3)
                            {
                                Sheet.Cells["D" + index].Value = "จำนวนที่เก็ยเงินได้ แต่ไม่พ้นรีเพลส";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    //var FindProject = project.ToList()[col];
                                    var count_project = Find2.Where(x => x.project == CheckProject[col].Key.project).Where(x => x.status == 31 || (x.status != null && (status_Exit_before_the_guarantee_expires != null && x.status == status_Exit_before_the_guarantee_expires.Id))).ToList();
                                    Sheet.Cells[index, col + 5].Value = count_project.Count() == 0 ? null : count_project.Count();
                                }
                            }
                            else if (a == 4)
                            {
                                Sheet.Cells["D" + index].Value = "รวมทั้งหมด";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    var Address = Sheet.Cells[index - 1, col + 5];
                                    var Address2 = Sheet.Cells[index - 2, col + 5];
                                    Sheet.Cells[index, col + 5].Formula = "=" + Address + " + " + Address2 + "";
                                }
                            }
                            else if (a == 5)
                            {
                                Sheet.Cells["D" + index].Value = "% Share";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    var Address = Sheet.Cells[index - 1, col + 5];
                                    Sheet.Cells[index, col + 5].Formula = "=" + Address + "/Z" + (index - 1) + "";
                                    Sheet.Cells[index, col + 5].Style.Numberformat.Format = "0.00";
                                }
                            }
                            index++;
                        }
                    }
                    Sheet.Cells["A:M"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A:M"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Sheet.Cells["A:M"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:M"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:M"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:M"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:M"].AutoFitColumns();
                    //package.SaveAs(stream);
                    package.SaveAs(NewPath);
                    stream.Position = 0;
                    #endregion
                }
                return new JsonResult(new { fileUrl = FileName });
                //return PhysicalFile(NewPath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        //[HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnGetExport_ReplacementReport(int? Project, string? StartDate, string? EndDate, string? userid, int? Status)
        {
            try
            {
                var FileName = DateTime.Now.Ticks + ".xlsx";
                var NewPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Export/" + FileName);
                var (userids, roles, _, department) = User.GetUser();

                Stream stream = new MemoryStream();
                using (var package = new ExcelPackage(new FileInfo(NewPath)))
                {
                    ExcelWorksheet Sheet = package.Workbook.Worksheets.Add("Replacement Report");

                    var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();

                    var fixDate = DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null);
                    GetDB = GetDB.Where(x => x.DeleteAt != 1 && x.CreatedDate != null && x.CreatedDate.Value.Date >= fixDate.Date).ToList();

                    if (StartDate == null || StartDate == "")
                    {
                        //GetDB = GetDB.Where(x => x.DeleteAt != 1 && (x.CreatedDate.Value.Month == DateTime.Now.Month && x.CreatedDate.Value.Year == DateTime.Now.Year)).ToList();
                    }

                    List<CDD> NewCDD = new List<CDD>();
                    List<CDD> NewCDD2 = new List<CDD>();
                    var spit = userid == null ? null : userid.Split(',');
                    if (spit != null)
                    {
                        foreach (var item in spit)
                        {
                            var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.ToLower()).ToList();
                            NewCDD.AddRange(where_user);
                        }
                    }
                    else
                    {
                        if (roles == "recruiter")
                        {
                            NewCDD = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == userids.ToLower()).ToList();

                            //var db = await _unitOfWork.UserRoleRepository.GetUserRole();
                            //db = db.Where(x => x.RoleId.ToLower() == "b68ff80f-5702-4cb5-a6bd-b50ed58c8068").ToList();
                            //foreach (var item in db)
                            //{
                            //    var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.UserId.ToLower()).ToList();
                            //    NewCDD.AddRange(where_user);
                            //}
                        }
                    }

                    //if (NewCDD.Count() != 0)
                    //{
                    GetDB = NewCDD;
                    //}

                    if (roles == "project owner")
                    {
                        var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                        var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);
                        Project = where_project.Project;
                    }

                    var NewDB = GetDB.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.ReportResponseExport
                    {
                        id = x.Id,
                        userId = x.UserId,
                        name = x.NameTH,
                        nickname = x.Nikname,
                        tel = x.Tel,
                        project = x.Project,
                        kpi = x.KPI,
                        status = x.Status,
                        Date = x.CreatedDate
                    }).ToList();

                    if (Project != null && Project != -1)
                    {
                        NewDB = NewDB.Where(x => x.project == Project).ToList();
                    }

                    if (StartDate != null)
                    {
                        NewDB = NewDB.Where(x => x.Date >= Convert.ToDateTime(StartDate).Date).ToList();
                    }

                    if (EndDate != null)
                    {
                        NewDB = NewDB.Where(x => x.Date <= Convert.ToDateTime(EndDate)).ToList();
                    }

                    if (Status != null && Status != -1)
                    {
                        NewDB = NewDB.Where(x => x.status == Status).ToList();
                    }

                    var Group = NewDB.Where(x => x.userId != null).GroupBy(x => new { x.userId }).ToList();
                    var GetProject = await _unitOfWork.ProjectRepository.GetAllAsync();
                    var i = 0;

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


                    #region ตาราง
                    Sheet.Cells["A1"].Value = "#";
                    Sheet.Cells["B1"].Value = "Name";
                    Sheet.Cells["C1"].Value = "Total KPI";
                    var project = await _unitOfWork.ProjectRepository.GetAllAsync();
                    var col = 0;
                    var CheckProject = NewDB.Where(x => x.project != -1 && x.project != null).GroupBy(x => new { x.project }).ToList();
                    for (col = 0; col < CheckProject.Count(); col++)
                    {
                        Sheet.Cells[1, col + 4].Value = project.ToList()[col].Name;
                    }

                    Sheet.Cells[1, col + 4].Value = "Total All Port";

                    #endregion
                    var DBUser = await _unitOfWork.UsersRepository.GetAllAsync();
                    int index = 2;
                    foreach (var item in Group)
                    {
                        var Find = NewDB.Where(x => (x.userId != null && item.Key.userId != null) && x.userId.ToLower() == item.Key.userId.ToLower()).ToList();
                        var GetUser = DBUser.FirstOrDefault(x => (x.Id != null && item.Key.userId != null) && x.Id == item.Key.userId);
                        Sheet.Cells["A" + index].Value = (i = i + 1);
                        Sheet.Cells["B" + index].Value = GetUser.Firstname + " " + GetUser.Lastname;
                        var aa = Find.Where(x => (x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id)).ToList();
                        for (var s = 0; s <= 3; s++)
                        {
                            if (s == 0)
                            {
                                Sheet.Cells["C" + index].Value = "จำนวน KPI (เก็บเงินได้)";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    var ss = aa.Where(x => x.project == CheckProject[col].Key.project).ToList();
                                    Sheet.Cells[index, col + 4].Value = ss.Count() == 0 ? null : ss.Count();
                                }
                                Sheet.Cells[index, col + 4].Value = aa.Count() == 0 ? null : aa.Count();
                            }
                            else if (s == 1)
                            {
                                Sheet.Cells["C" + index].Value = "จำนวนที่เก็บเงินได้ แต่ไม่พ้นรีเพลส";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    var ss = aa.Where(x => x.project == CheckProject[col].Key.project && x.status != 31).ToList();
                                    Sheet.Cells[index, col + 4].Value = ss.Count();
                                }
                            }
                            else if (s == 2)
                            {
                                Sheet.Cells["C" + index].Value = "รวมทั้งหมด";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    var Address = Sheet.Cells[index - 1, col + 4];
                                    var Address2 = Sheet.Cells[index - 2, col + 4];
                                    Sheet.Cells[index, col + 4].Formula = "=" + Address + " + " + Address2 + "";
                                }
                            }
                            else if (s == 3)
                            {
                                Sheet.Cells["C" + index].Value = "% Replace";
                                for (col = 0; col < CheckProject.Count(); col++)
                                {
                                    var Address = Sheet.Cells[index - 1, col + 4];
                                    var Address2 = Sheet.Cells[index - 2, col + 4];
                                    Sheet.Cells[index, col + 4].Formula = "=" + Address + "/" + Address2 + "";
                                    Sheet.Cells[index, col + 4].Style.Numberformat.Format = "0.00";
                                }
                            }
                            index++;
                        }
                    }

                    Sheet.Cells["A:Z"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A:Z"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Sheet.Cells["A:Z"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:Z"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:Z"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:Z"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:Z"].AutoFitColumns();
                    package.SaveAs(NewPath);
                    stream.Position = 0;
                }
                return new JsonResult(new { fileUrl = FileName });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        //[HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnGetExport_TargetVsAchieved(int? Project, string? StartDate, string? EndDate, string? userid, int? Status)
        {
            try
            {
                var FileName = DateTime.Now.Ticks + ".xlsx";
                var NewPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Export/" + FileName);
                Stream stream = new MemoryStream();
                using (var package = new ExcelPackage(new FileInfo(NewPath)))
                {
                    var GetDB = await _unitOfWork.CDDRepository.GetAllAsync();

                    var fixDate = DateTime.ParseExact("22/04/2025", "dd/MM/yyyy", null);
                    GetDB = GetDB.Where(x => x.DeleteAt != 1 && x.CreatedDate != null && x.CreatedDate.Value.Date >= fixDate.Date).ToList();

                    if (StartDate == null || StartDate == "")
                    {
                        //GetDB = GetDB.Where(x => x.DeleteAt != 1 && (x.CreatedDate.Value.Month == DateTime.Now.Month && x.CreatedDate.Value.Year == DateTime.Now.Year)).ToList();
                    }

                    var NewDB = GetDB.Where(x => x.DeleteAt != 1).Select(x => new ResponseDTO.ReportResponseExport
                    {
                        id = x.Id,
                        name = x.NameTH,
                        nickname = x.Nikname,
                        tel = x.Tel,
                        project = null,
                        kpi = null,
                        status = x.Status,
                        Date = x.CreatedDate,
                        Update = x.UpdatedDate,
                        userId = x.UserId
                    }).ToList();

                    List<CDD> NewCDD = new List<CDD>();
                    var spit = userid == null ? null : userid.Split(',');
                    var (userids, roles, _, department) = User.GetUser();
                    if (spit != null)
                    {
                        foreach (var item in spit)
                        {
                            var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.ToLower()).ToList();
                            NewCDD.AddRange(where_user);
                        }
                    }
                    else
                    {
                        if (roles == "recruiter")
                        {
                            NewCDD = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == userids.ToLower()).ToList();

                            //var db = await _unitOfWork.UserRoleRepository.GetUserRole();
                            //db = db.Where(x => x.RoleId.ToLower() == "b68ff80f-5702-4cb5-a6bd-b50ed58c8068").ToList();
                            //foreach (var item in db)
                            //{
                            //    var where_user = GetDB.Where(x => x.UserId != null && x.UserId.ToLower() == item.UserId.ToLower()).ToList();
                            //    NewCDD.AddRange(where_user);
                            //}
                        }
                    }

                    //if (NewCDD.Count() != 0)
                    //{
                    //GetDB = NewCDD;
                    //}

                    if (roles == "project owner")
                    {
                        var getuser = await _unitOfWork.UsersRepository.GetAllAsync();
                        var where_project = getuser.FirstOrDefault(x => x.Id != null && x.Id.ToLower() == userids);
                        Project = where_project.Project;
                    }

                    if (Project != null && Project != -1)
                    {
                        NewCDD = NewCDD.Where(x => x.Project == Project).ToList();
                    }

                    if (StartDate != null)
                    {
                        NewCDD = NewCDD.Where(x => x.CreatedDate >= Convert.ToDateTime(StartDate).Date).ToList();
                    }

                    if (EndDate != null)
                    {
                        NewCDD = NewCDD.Where(x => x.CreatedDate <= Convert.ToDateTime(EndDate)).ToList();
                    }

                    if (Status != null && Status != -1)
                    {
                        NewCDD = NewCDD.Where(x => x.Status == Status).ToList();
                    }

                    var Group = NewCDD.Where(x => x.UserId != null && x.CreatedDate.Value.Month == DateTime.Now.Month).GroupBy(x => new { x.UserId }).ToList();
                    if (roles == "recruiter")
                    {
                        Group = Group.Where(x => x.Key.UserId == userids).ToList();
                    }

                    var GetProject = await _unitOfWork.ProjectRepository.GetAllAsync();
                    var StatusRecruit = await _unitOfWork.RecruitStatusRepository.GetAllAsync();
                    int? index = 2;
                    var i = 0;
                    var DBUser = await _unitOfWork.UsersRepository.GetAllAsync();

                    ExcelWorksheet Sheet = package.Workbook.Worksheets.Add("ตาราง Target Vs Achieved");
                    ExcelWorksheet Sheet2 = package.Workbook.Worksheets.Add("ตาราง RUN RATE");

                    #region Sheet 1

                    #region ตาราง
                    Sheet.Cells["A1"].Value = "#";
                    Sheet.Cells["B1"].Value = "Name";
                    Sheet.Cells["C1"].Value = "Target";
                    Sheet.Cells["D1"].Value = "Achieved";
                    Sheet.Cells["E1"].Value = "%";
                    Sheet.Cells["F1"].Value = "รอเก็บเงิน";
                    Sheet.Cells["G1"].Value = "รอเริ่มงาน";
                    #endregion

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

                    List<RecruitmentUser> users = new List<RecruitmentUser>();

                    if (Group.Count() == 0)
                    {
                        if (spit != null)
                        {
                            foreach (var item in spit)
                            {
                                var where_user = DBUser.Where(x => x.Id != null && x.Id.ToLower() == item.ToLower()).ToList();
                                users.AddRange(where_user);
                            }
                        }
                    }
                    else
                    {
                        users.AddRange(DBUser);
                    }

                    foreach (var item in users) //.Take(1)
                    {
                        var GetUser = DBUser.FirstOrDefault(x => (x.Id != null && item.Id != null) && x.Id.ToLower() == item.Id.ToLower());

                        //Sheet.Cells["A" + index + ":A" + (index + 4)].Merge = true;
                        Sheet.Cells["A" + index].Value = (i = i + 1);
                        for (var a = 0; a < 4; a++)
                        {
                            if (a == 0)
                            {
                                Sheet.Cells["B" + index].Value = GetUser == null ? null : GetUser.Firstname;
                                Sheet.Cells["C" + index].Value = GetUser.TargetKpi;
                                Sheet.Cells["D" + index].Value = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && ((x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id) || (x.status == 32 || (status_Exit_before_the_guarantee_expires != null && x.status == status_Exit_before_the_guarantee_expires.Id)))).ToList().Count; //TargetProject.Count();
                                Sheet.Cells["E" + index].Formula = "=C" + index + "/D" + index + "";
                                Sheet.Cells["E" + index].Style.Numberformat.Format = "0.00";
                                Sheet.Cells["F" + index].Value = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && x.status == 34).ToList().Count();
                                Sheet.Cells["G" + index].Value = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && x.status == 11).ToList().Count();
                            }
                            else if (a == 1)
                            {
                                Sheet.Cells["B" + index].Value = "Achieved";
                                var dsadsa = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && ((x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id) || (x.status == 32 || (status_Exit_before_the_guarantee_expires != null && x.status == status_Exit_before_the_guarantee_expires.Id)))).ToList();
                                foreach (var itema in dsadsa)
                                {
                                    Sheet.Cells["C" + index].Value = itema.project == null ? null : GetProject.FirstOrDefault(x => x.Id == itema.project).Name;
                                    Sheet.Cells["D" + index].Value = dsadsa.FirstOrDefault(x => x.id == itema.id).name;
                                    Sheet.Cells["E" + index].Value = itema.status == null ? null : StatusRecruit.FirstOrDefault(x => x.Id == itema.status).Name;
                                    index++;
                                }
                            }
                            else if (a == 2)
                            {
                                Sheet.Cells["B" + index].Value = "รอเก็บเงิน";
                                var dsadsa = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && (x.status == 34 || (Waiting_work != null && x.status == Waiting_work.Id))).ToList();
                                foreach (var itema in dsadsa)
                                {
                                    Sheet.Cells["C" + index].Value = itema.project == null ? null : GetProject.FirstOrDefault(x => x.Id == itema.project).Name;
                                    Sheet.Cells["D" + index].Value = dsadsa.FirstOrDefault(x => x.id == itema.id).name;
                                    Sheet.Cells["E" + index].Value = itema.status == null ? null : StatusRecruit.FirstOrDefault(x => x.Id == itema.status).Name;
                                    index++;
                                }
                            }
                            else if (a == 3)
                            {
                                Sheet.Cells["B" + index].Value = "รอเริ่มงาน";
                                var dsadsa = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && (x.status == 11 || (Com_only != null && x.status == Com_only.Id))).ToList();
                                foreach (var itema in dsadsa)
                                {
                                    Sheet.Cells["C" + index].Value = itema.project == null ? null : GetProject.FirstOrDefault(x => x.Id == itema.project).Name;
                                    Sheet.Cells["D" + index].Value = dsadsa.FirstOrDefault(x => x.id == itema.id).name;
                                    Sheet.Cells["E" + index].Value = itema.status == null ? null : StatusRecruit.FirstOrDefault(x => x.Id == itema.status).Name;
                                    index++;
                                }
                            }
                            index++;
                        }
                    }

                    Sheet.Cells["A:G"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Sheet.Cells["A:G"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    Sheet.Cells["A:G"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:G"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:G"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:G"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Sheet.Cells["A:G"].AutoFitColumns();
                    #endregion

                    #region Sheet 2
                    Sheet2.Cells["A1"].Value = "#";
                    Sheet2.Cells["B1"].Value = "ชื่อ RC หรือ ชื่อ PORT";
                    Sheet2.Cells["C1"].Value = "Target";
                    Sheet2.Cells["D1"].Value = "ผลงาน ณ วันที่";
                    Sheet2.Cells["E1"].Value = "%";
                    Sheet2.Cells["F1"].Value = "จำนวนวันที่ผ่านไปแล้ว";
                    Sheet2.Cells["G1"].Value = "จำนวนวันคงเหลือ";
                    Sheet2.Cells["H1"].Value = "RUN RATE";
                    Sheet2.Cells["I1"].Value = "% RUN RATE";
                    var ii = 0;
                    var index2 = 2;

                    DateTime today = DateTime.Today;
                    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
                    int remainingDays = daysInMonth - today.Day;

                    foreach (var item in users)
                    {
                        var GetUser = DBUser.FirstOrDefault(x => (x.Id != null && item.Id != null) && x.Id.ToLower() == item.Id.ToLower());
                        var aaasdsd = NewDB.Where(x => (x.userId != null && item.Id != null) && x.userId.ToLower() == item.Id.ToLower() && x.Update.Value.Month == DateTime.Now.Month).ToList();
                        //Sheet.Cells["A" + index + ":A" + (index + 4)].Merge = true;
                        Sheet2.Cells["A" + index2].Value = (ii = ii + 1);
                        for (var a = 0; a < 4; a++)
                        {
                            if (a == 0)
                            {
                                Sheet2.Cells["B" + index2].Value = GetUser == null ? null : GetUser.Firstname;
                                Sheet2.Cells["C" + index2].Value = GetUser.TargetKpi;
                                Sheet2.Cells["D" + index2].Value = aaasdsd.Where(x => (x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id) || (x.status == 32 || (status_Exit_before_the_guarantee_expires != null && x.status == status_Exit_before_the_guarantee_expires.Id))).Count();  //NewDB.Where(x => (x.userId != null && x.userId == item.Key.userId) && (x.status == 27 || x.status == 32)).ToList().Count; //TargetProject.Count();
                                Sheet2.Cells["E" + index2].Formula = "=C" + index2 + "/D" + index2 + "";
                                Sheet2.Cells["E" + index2].Style.Numberformat.Format = "0.00";
                                Sheet2.Cells["F" + index2].Value = today.Day;
                                Sheet2.Cells["G" + index2].Value = remainingDays;
                                Sheet2.Cells["H" + index2].Formula = "=(F" + index2 + "+((F" + index2 + "/" + today.Day + ")*G" + index2 + "))";
                                Sheet2.Cells["I" + index2].Formula = "=C" + index2 + "/H" + index2 + "";
                                Sheet2.Cells["I" + index2].Style.Numberformat.Format = "0.00";
                            }
                            else if (a == 1)
                            {
                                Sheet2.Cells["B" + index2].Value = "Achieved";
                                var dsadsa = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && ((x.status == 24 || x.status == 25 || x.status == 27 || x.status == 28 || x.status == 30 || x.status == 37 || x.status == 38) || (status_I_have_already_collected_the_money != null && x.status == status_I_have_already_collected_the_money.Id) || x.status == 32)).ToList();
                                foreach (var itema in dsadsa)
                                {
                                    Sheet2.Cells["C" + index2].Value = itema.project == null ? null : GetProject.FirstOrDefault(x => x.Id == itema.project).Name;
                                    Sheet2.Cells["D" + index2].Value = dsadsa.FirstOrDefault(x => x.id == itema.id).name;
                                    Sheet2.Cells["E" + index2].Value = itema.status == null ? null : StatusRecruit.FirstOrDefault(x => x.Id == itema.status).Name;
                                    index2++;
                                }
                            }
                            else if (a == 2)
                            {
                                Sheet2.Cells["B" + index2].Value = "รอเก็บเงิน";
                                var dsadsa = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && (x.status == 34)).ToList();
                                foreach (var itema in dsadsa)
                                {
                                    Sheet2.Cells["C" + index2].Value = itema.project == null ? null : GetProject.FirstOrDefault(x => x.Id == itema.project).Name;
                                    Sheet2.Cells["D" + index2].Value = dsadsa.FirstOrDefault(x => x.id == itema.id).name;
                                    Sheet2.Cells["E" + index2].Value = itema.status == null ? null : StatusRecruit.FirstOrDefault(x => x.Id == itema.status).Name;
                                    index2++;
                                }
                            }
                            else if (a == 3)
                            {
                                Sheet2.Cells["B" + index2].Value = "รอเริ่มงาน";
                                var dsadsa = NewDB.Where(x => (x.userId != null && x.userId == item.Id) && (x.status == 11)).ToList();
                                foreach (var itema in dsadsa)
                                {
                                    Sheet2.Cells["C" + index2].Value = itema.project == null ? null : GetProject.FirstOrDefault(x => x.Id == itema.project).Name;
                                    Sheet2.Cells["D" + index2].Value = dsadsa.FirstOrDefault(x => x.id == itema.id).name;
                                    Sheet2.Cells["E" + index2].Value = itema.status == null ? null : StatusRecruit.FirstOrDefault(x => x.Id == itema.status).Name;
                                    index2++;
                                }
                            }
                            index2++;
                        }

                        Sheet2.Cells["A:I"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        Sheet2.Cells["A:I"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        Sheet2.Cells["A:I"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        Sheet2.Cells["A:I"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        Sheet2.Cells["A:I"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        Sheet2.Cells["A:I"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        Sheet2.Cells["A:I"].AutoFitColumns();
                    }
                    #endregion
                    package.SaveAs(NewPath);
                    stream.Position = 0;
                }
                return new JsonResult(new { fileUrl = FileName });
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
