using AutoSchedule.DAL;
using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Enum;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Teacher;
using AutoSchedule.Services.Interfaces;
using ClosedXML.Excel;
using System.Data;
using System.Security.Claims;

namespace AutoSchedule.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBaseRepository<User> _userRepository;

        private readonly IBaseRepository<Teacher> _teacherRepository;

        public TeacherService(ApplicationDbContext context,
                              IBaseRepository<User> userRepository,
                              IBaseRepository<Teacher> teacherRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> AddTeacherList(IFormFile formFile, string userName)
        {
            try
            {
                var workbook = new XLWorkbook(formFile.OpenReadStream());
                var worksheet = workbook.Worksheet(1);

                var firstRow = worksheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
                var lastRow = worksheet.LastRowUsed().RangeAddress.FirstAddress.RowNumber;

                var teachers = new List<Teacher>();

                for (int i = firstRow + 1; i <= lastRow; i++)
                {
                    var row = worksheet.Row(i);
                    var teacher = new Teacher();

                    teacher.Name = row.Cell(1).GetString();
                    teacher.Lesson = row.Cell(2).GetString();
                    teacher.UserId = _userRepository.GetAll().Where(x => x.Email == userName).FirstOrDefault().Id;

                    teachers.Add(teacher);
                }

                _context.AddRange(teachers);
                await _context.SaveChangesAsync();

                return new BaseResponse<ClaimsIdentity>(){
                    Description = "Объекты добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>(){
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Teacher>> Create(TeacherViewModel model, string userName)
        {
            try
            {
                var teacher = _teacherRepository.GetAll().Where(x => x.Name == model.Name).FirstOrDefault();
                if(teacher != null)
                {
                    return new BaseResponse<Teacher>()
                    {
                        Description = "Учитель с таким ФИО уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }

                teacher = new Teacher()
                {
                    Name = model.Name,
                    Lesson = model.Lesson,
                    UserId = _userRepository.GetAll().Where(x => x.Email == userName).FirstOrDefault().Id
                };

                await _teacherRepository.Create(teacher);

                return new BaseResponse<Teacher>()
                {
                    Description = "Учитель добавлен",
                    StatusCode = StatusCode.OK
                };
            } 
            catch(Exception ex)
            {
                return new BaseResponse<Teacher>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<TeacherViewModel>>> GetTeachers()
        {
            try
            {
                var teachers = _teacherRepository.GetAll()
                    .Select(x => new TeacherViewModel()
                    {
                       Name = x.Name,
                       Lesson = x.Lesson
                    });

                    return new BaseResponse<IEnumerable<TeacherViewModel>>()
                    {
                        Data = teachers,
                        StatusCode = StatusCode.OK
                    };
            } 
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
