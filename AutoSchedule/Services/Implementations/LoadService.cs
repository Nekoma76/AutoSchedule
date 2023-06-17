using AutoSchedule.DAL;
using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Enum;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Load;
using AutoSchedule.Services.Interfaces;
using ClosedXML.Excel;

namespace AutoSchedule.Services.Implementations;

public class LoadService : ILoadService
{
    private readonly ApplicationDbContext _context;
    private readonly IBaseRepository<Load> _loadRepository;
    private readonly IBaseRepository<User> _userRepository;

    public LoadService(ApplicationDbContext context, IBaseRepository<Load> loadRepository, IBaseRepository<User> userRepository)
    {
        _context = context;
        _loadRepository = loadRepository;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<IEnumerable<LoadViewModel>>> GetLoad(string userName)
    {
        try
        {
            var userId = _userRepository.GetAll().Where(x => x.Email == userName).FirstOrDefault().Id;
            var loads = _loadRepository.GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => new LoadViewModel
                {
                    ClassName = x.ClassName,
                    Shift = x.Shift,
                    Hours = x.Hours,
                    Lesson = x.Lesson,
                    TeacherName = x.TeacherName
                });
            
            return new BaseResponse<IEnumerable<LoadViewModel>>()
            {
                Data = loads,
                StatusCode = Models.Enum.StatusCode.OK
            };
        } 
        catch
        {
            throw;
        }
    }

    public async Task<BaseResponse<Load>> GetLoadList(IFormFile formFile, string userName)
    {
        try
        {
            var workbook = new XLWorkbook(formFile.OpenReadStream());
                var worksheet = workbook.Worksheet(1);

                var firstRow = worksheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
                var lastRow = worksheet.LastRowUsed().RangeAddress.FirstAddress.RowNumber;

                var loads = new List<Load>();

                for (int i = firstRow + 1; i <= lastRow; i++)
                {
                    var row = worksheet.Row(i);
                    var load = new Load();

                    load.ClassName = row.Cell(1).GetString();
                    load.Shift = row.Cell(2).GetValue<int>();
                    load.Hours = row.Cell(3).GetValue<int>();
                    load.Lesson = row.Cell(4).GetString();
                    load.TeacherName = row.Cell(5).GetString();
                    load.UserId = _userRepository.GetAll().Where(x => x.Email == userName).FirstOrDefault().Id;

                    loads.Add(load);
                }

                _context.AddRange(loads);
                await _context.SaveChangesAsync();

                return new BaseResponse<Load>()
                {
                    Description = "Список нагрузки добавлен",
                    StatusCode = StatusCode.OK
                };
        }
        catch(Exception ex)
        {
            return new BaseResponse<Load>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
