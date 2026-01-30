using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Recruitment.Areas.Identity.Data;
using Recruitment.Models;
using Recruitment.Repositories;
using static Recruitment.Models.ResponseDTO;

#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods
#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace Recruitment.Pages.Backend
{
    [Authorize]
    public class JobPositionModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobPositionModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTable()
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

                var GetDB = await _unitOfWork.PositionRepository.GetAllAsync();
                GetDB = GetDB.Where(x => x.DeleteAt != 1).ToList();
                List<ResponseDTO.PositionResponse> position = new List<ResponseDTO.PositionResponse>();
                var index = 1;

                GetDB = GetDB.OrderBy(o => o.TypeForm).ToList();

                foreach (var item in GetDB)
                {
                    var Group = GetDB.Where(x => x.Name == item.Name).ToList();
                    string? TypeForm = "";
                    int? loop = 0;
                    foreach (var itemp in Group)
                    {
                        if (loop == 0)
                        {
                            if (itemp.TypeForm == 1)
                            {
                                TypeForm = "ใบสมัคร A";
                            }
                            else if (itemp.TypeForm == 2)
                            {
                                TypeForm = "ใบสมัคร B";
                            }
                            else if (itemp.TypeForm == 3)
                            {
                                TypeForm = "ใบสมัคร C";
                            }
                            else if (itemp.TypeForm == 4)
                            {
                                TypeForm = "ใบสมัคร D";
                            }
                        }
                        else
                        {
                            if (itemp.TypeForm == 1)
                            {
                                TypeForm += "," + "ใบสมัคร A";
                            }
                            else if (itemp.TypeForm == 2)
                            {
                                TypeForm += "," + "ใบสมัคร B";
                            }
                            else if (itemp.TypeForm == 3)
                            {
                                TypeForm += "," + "ใบสมัคร C";
                            }
                            else if (itemp.TypeForm == 4)
                            {
                                TypeForm += "," + "ใบสมัคร D";
                            }
                        }
                        loop++;
                    }

                    if (Group.Count() != 0)
                    {
                        var Check = position.Where(x => x.Name == item.Name).ToList();
                        if (Check.Count() == 0)
                        { 
                            position.Add(new PositionResponse
                            {
                                index = index,
                                Status = item.Status,
                                Id = item.Id,
                                Name = item.Name,
                                TypeForm = TypeForm,
                            });
                            index++;
                        }                      
                    }
                    else
                    {
                        position.Add(new PositionResponse
                        {
                            index = index,
                            Status = item.Status,
                            Id = item.Id,
                            Name = item.Name,
                            TypeForm = TypeForm,
                        });
                        index++;
                    }               
                }

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    switch (sortColumn)
                    {
                        case "name":
                            position = sortColumnDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? position.OrderByDescending(x => x.Name).ToList() : position.OrderBy(x => x.Name).ToList();
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(searchValue))
                {
                    position = position.Where(x => x is { Name: not null } && (x.Name.Contains(searchValue))).ToList();
                }
                recordsTotal = position.Count();
                position = position.Skip(skip).Take(pageSize).ToList();

                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = position };
                return new JsonResult(jsonData);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAddData(RequestDTO.PositionRequest request)
        {
            var i = 0;
            try
            {
                List<Position> position = new List<Position>();
                foreach (var item in request.Form)
                {
                    position.Add(new Position
                    {
                        Name = request.Name,
                        Status = request.Status,
                        TypeForm = Convert.ToInt32(item.ToString()),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    });
                }             
                await _unitOfWork.PositionRepository.AddRangeAsync(position);
                await _unitOfWork.CompleteAsync();
                i = 1;
                return new JsonResult(i);
            }
            catch (Exception ex)
            {
                return new JsonResult("error : " + ex.Message + " inner : " + ex.InnerException);
            }
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnGetPosition(int? Id)
        {
            var db = await _unitOfWork.PositionRepository.GetByIdAsync(Id);
            return new JsonResult(db);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEditData(RequestDTO.PositionRequest request)
        {
            var i = 0;
            try
            {
                var GetPosition = await _unitOfWork.PositionRepository.GetAllAsync();
                GetPosition = GetPosition.Where(x => x.Name == request.Name).ToList();
                if (GetPosition.Count() != 0)
                {
                    var Delete = GetPosition.Select(x => new Position
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TypeForm = x.TypeForm,
                        Status = x.Status,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        DeleteAt = 1
                    } ).ToList();
                    _unitOfWork.PositionRepository.UpdateRange(Delete);
                    await _unitOfWork.CompleteAsync();
                }

                List<Position> position = new List<Position>();
                foreach (var item in request.Form)
                {
                    position.Add(new Position
                    {
                        Name = request.Name,
                        Status = request.Status,
                        TypeForm = Convert.ToInt32(item.ToString()),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    });
                }
                await _unitOfWork.PositionRepository.AddRangeAsync(position);
                await _unitOfWork.CompleteAsync();
                i = 1;
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
                var position = await _unitOfWork.PositionRepository.GetByIdAsync(Id);
                if (position is not null)
                {
                    position.DeleteAt = 1;
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
