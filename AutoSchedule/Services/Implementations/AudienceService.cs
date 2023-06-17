using AutoSchedule.DAL;
using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Enum;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Audience;
using AutoSchedule.Services.Interfaces;
using ClosedXML.Excel;

namespace AutoSchedule.Services.Implementations;

public class AudienceService : IAudienceService
{

    private readonly ApplicationDbContext _context;
    private readonly IBaseRepository<User> _userRepository;

     private readonly IBaseRepository<Audience> _audienceRepository;

    public AudienceService(ApplicationDbContext context,
        IBaseRepository<User> userRepository, IBaseRepository<Audience> audienceRepository)
    {
        _context = context;
        _audienceRepository = audienceRepository;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<Audience>> AddAudienceList(IFormFile formFile, string userName)
    {
        try
        {
            var workbook = new XLWorkbook(formFile.OpenReadStream());
            var worksheet = workbook.Worksheet(1);

            var firstRow = worksheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
            var lastRow = worksheet.LastRowUsed().RangeAddress.FirstAddress.RowNumber;

            var audiences = new List<Audience>();

            for (int i = firstRow + 1; i <= lastRow; i++)
            {
                var row = worksheet.Row(i);
                var audience = new Audience();

                audience.Number = row.Cell(1).GetString();
                audience.SeatsCount = row.Cell(2).GetString();
                audience.Lesson = row.Cell(3).GetString();
                audience.UserId = _userRepository.GetAll().Where(x => x.Email == userName).FirstOrDefault().Id;

                audiences.Add(audience);
            }

            _context.AddRange(audiences);
            await _context.SaveChangesAsync();

            return new BaseResponse<Audience>()
            {
                Description = "Список аудитории добавлен",
                StatusCode = StatusCode.OK
            };
        }
        catch(Exception ex)
        {
            return new BaseResponse<Audience>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<BaseResponse<IEnumerable<AudienceViewModel>>> GetAudiences()
    {
        try
        {
            var audiences = _audienceRepository.GetAll()
                .Select(x => new AudienceViewModel()
                {
                    Number = x.Number,
                    SeatsCount = x.SeatsCount,
                    Lesson = x.Lesson
                });

            return new BaseResponse<IEnumerable<AudienceViewModel>>()
            {
                Data = audiences,
                StatusCode = StatusCode.OK
            };
        }
        catch
        {
            throw;
        }
    }
}
