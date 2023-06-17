using System.Security.Claims;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Audience;

namespace AutoSchedule.Services.Interfaces;

public interface IAudienceService
{
    Task<BaseResponse<IEnumerable<AudienceViewModel>>> GetAudiences();
    Task<BaseResponse<Audience>> AddAudienceList(IFormFile formFile, string userName);
}
