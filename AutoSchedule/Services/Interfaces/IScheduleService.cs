using System.Security.Claims;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Load;
using AutoSchedule.Models.ViewModels.Schedule;

namespace AutoSchedule.Services.Interfaces;

public interface IScheduleService
{
    Task<BaseResponse<IEnumerable<LoadViewModel>>> Create(ScheduleViewModel model, string userName);
}
