namespace AutoSchedule.Services.Interfaces;

using System.Security.Claims;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Profile;

public interface IProfileService
{
    Task<BaseResponse<ProfileViewModel>> GetInfo(string userName);
    Task<BaseResponse<ClaimsIdentity>> Create(ProfileViewModel model, string userName);
}
