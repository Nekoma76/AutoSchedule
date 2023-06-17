using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Load;

namespace AutoSchedule.Services.Interfaces;

public interface ILoadService
{
    Task<BaseResponse<IEnumerable<LoadViewModel>>> GetLoad(string userName);
    Task<BaseResponse<Load>> GetLoadList(IFormFile formFile, string userName);
}
