using System.Security.Claims;
using Aspose.Cells;
using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Enum;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Load;
using AutoSchedule.Models.ViewModels.Schedule;
using AutoSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.Services.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Load> _loadRepository;
    private readonly IBaseRepository<Audience> _audienceRepository;
    private readonly IBaseRepository<Teacher> _teacherRepository;

    public ScheduleService(IBaseRepository<User> userRepository,
                           IBaseRepository<Load> loadRepository,
                           IBaseRepository<Audience> audienceRepository,
                           IBaseRepository<Teacher> teacherRepository)
    {
        _loadRepository = loadRepository;
        _userRepository = userRepository;
        _audienceRepository = audienceRepository;
        _teacherRepository = teacherRepository;
    }

    public async Task<BaseResponse<IEnumerable<LoadViewModel>>> Create(ScheduleViewModel model, string userName)
    {
        Random random = new Random();
        Workbook workBook = new Workbook();
        const int DAY_OF_THE_WEEK = 7;
        const int MINUTE_IN_HOUR = 60;

        var classList = new List<string>(){};
        List<string> lessonsList;

        var userId = _userRepository.GetAll().Where(x => x.Email == userName).FirstOrDefault().Id;
        var load = _loadRepository.GetAll().Where(x => x.UserId == userId);
        var audience = _audienceRepository.GetAll().Where(x => x.UserId == userId);
        var teacher = _teacherRepository.GetAll().Where(x => x.UserId == userId);
        var currentClass = load.Select(x => x.ClassName).FirstOrDefault();
        var classCount = _loadRepository.GetAll().Where(x => x.UserId == userId);
        string[,] schedule = new string[model.LessonsCount, model.SchoolDays];

        for(int i = 0; i < 2; i++)
        {

            if(!classList.Contains(currentClass))
            {
                load = _loadRepository.GetAll().Where(x => x.UserId == userId && x.ClassName == currentClass);
                if(load.FirstOrDefault().Shift == 1)
                {
                    var lessons = _loadRepository.GetAll()
                        .Where(x => x.ClassName == currentClass && x.UserId == userId)
                        .Select(x => x.Lesson).Count();

                    var lesson = _loadRepository.GetAll()
                        .Where(x => x.ClassName == currentClass && x.UserId == userId)
                        .FirstOrDefault().Lesson;
                    lessonsList = new List<string>(){};
                    List<string> AllLessons = new List<string>();
                    AllLessons.AddRange(_loadRepository.GetAll()
                        .Where(x => x.ClassName == currentClass && x.UserId == userId)
                        .Select(x => x.Lesson));
                    for(int j = 0; j < lessons; j++)
                    {
                        if(!lessonsList.Contains(lesson))
                        {
                            var hours = _loadRepository.GetAll()
                                .Where(x => x.ClassName == currentClass && x.UserId == userId && x.Lesson == lesson)
                                .FirstOrDefault().Hours;

                            int lessonsCount = hours / DAY_OF_THE_WEEK / model.SchoolDays * MINUTE_IN_HOUR / 40;

                            for(int k = 0; k < lessonsCount; k++)
                            {
                                int dayCount = random.Next(model.SchoolDays);
                                int lessonCount = random.Next(model.LessonsCount);

                                if(schedule[lessonCount, dayCount] != null)
                                {
                                    k -= 1;
                                    continue;
                                }
                                schedule[lessonCount, dayCount] = lesson;       
                            }
                            lessonsList.Add(lesson);
                            if(lessonsList.Count() == lessons)
                            {
                                break;
                            }
                        }
                        else
                        {
                            j -= 1;
                            lesson = AllLessons.Except(lessonsList).FirstOrDefault();
                            continue;
                        }
                    }
                    classList.Add(currentClass);
                }
            }
            else
            {
                currentClass = _loadRepository.GetAll()
                    .Where(x => x.UserId == userId).FirstOrDefault(x => x.ClassName != currentClass).ClassName;
                continue;
            }
            Worksheet worksheet = workBook.Worksheets[0];
            worksheet.Cells.ImportArray(schedule, 0, 0);
        }
        workBook.Save("Schedule.xlsx");

        return new BaseResponse<IEnumerable<LoadViewModel>>()
        {
            StatusCode = StatusCode.OK
        };
    }
}

//45(часы) * 60(минуты в час)  / 4(недели в месяце) / 7(неделя) / 40(минут урока)??
//175(часы в год) / 7(дни недель) / 5(сколько дней в неделю учаться)
//175 / 7 / 5 * 60 / 40 (минуты) = 7.5