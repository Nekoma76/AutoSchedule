using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Account;
using System.Security.Claims;

namespace AutoSchedule.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
