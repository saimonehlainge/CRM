using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Models;
using Recruitment.Repositories;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
namespace Recruitment.Pages.Backend
{
    public class TeamModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamModel(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTableTeam()
        {
            try
            {
                string? draw = Request.Form["draw"];
                string? start = Request.Form["start"];
                string? length = Request.Form["length"];
                string? sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
                string? sortColumnDirection = Request.Form["order[0][dir]"];
                string? searchValue = Request.Form["search[value]"];
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int? recordsTotal = 0;

                var GetDB = await _unitOfWork.TeamRepository.GetAllAsync();
                GetDB = GetDB.Where(x => x.DeleteAt != 1).ToList();

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "name":

                            GetDB = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? GetDB.OrderByDescending(x => x.Name).ToList() : GetDB.OrderBy(x => x.Name).ToList();
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    GetDB = GetDB.Where(x => x is { Name: not null } && (x.Name.Contains(searchValue))).ToList();
                }

                recordsTotal = GetDB.Count();
                GetDB = GetDB.Skip(skip).Take(pageSize).ToList();

                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = GetDB };
                return new JsonResult(jsonData);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTeamSubmit(RequestDTO.TeamRequest request)
        {
            var i = 0;
            try
            {
                Team team = new Team();
                team.Name = request.Name;
                team.Status = request.Status;
                team.CreatedDate = DateTime.Now;
                team.UpdatedDate = DateTime.Now;

                await _unitOfWork.TeamRepository.AddAsync(team);
                await _unitOfWork.CompleteAsync();
                i = 1;
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        public async Task<IActionResult> OnGetTeam(int? Id)
        {
            var DB = await _unitOfWork.TeamRepository.GetByIdAsync(Id);
            return new JsonResult(DB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTeamEditSubmit(RequestDTO.TeamRequest request)
        {
            var i = 0;
            try
            {
                var team = await _unitOfWork.TeamRepository.GetByIdAsync(request.Id);
                if (team != null)
                {
                    team.Name = request.Name;
                    team.Status = request.Status;
                    team.CreatedDate = DateTime.Now;
                    team.UpdatedDate = DateTime.Now;

                    _unitOfWork.TeamRepository.Update(team);
                    await _unitOfWork.CompleteAsync();
                    i = 1;
                }
                else
                {
                    return BadRequest("ไม่มีข้อมูล");
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
        public async Task<IActionResult> OnGetDeleteData(int? Id)
        {
            var i = 0;
            try
            {
                var historyCovid = await _unitOfWork.TeamRepository.GetByIdAsync(Id);
                if (historyCovid is not null)
                {
                    historyCovid.DeleteAt = 1;
                    await _unitOfWork.CompleteAsync();
                    i = 1;
                }
                else
                {
                    i = 2;
                }
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }
    }
}
