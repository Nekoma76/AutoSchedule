using System.Security.Claims;
using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Enum;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Profile;
using AutoSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoSchedule.Services.Implementations;

public class ProfileService : IProfileService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Profile> _profileRepository;

    public ProfileService(IBaseRepository<User> userRepository, IBaseRepository<Profile> profileRepository)
    {
        _userRepository = userRepository;
        _profileRepository = profileRepository;
    }

    public async Task<BaseResponse<ClaimsIdentity>> Create(ProfileViewModel model, string userName)
    {
        try
        {
            var profile = new Profile
            {
                Id = model.Id,
                Name = model.Name,
                Organization = model.Organization,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                UserId = _userRepository.GetAll().Where(x => x.Email == userName).FirstOrDefault().Id
            };

            await _profileRepository.Create(profile);

            return new BaseResponse<ClaimsIdentity>()
            {
                Description = "Профиль создан",
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

    public async Task<BaseResponse<ProfileViewModel>> GetInfo(string userName)
    {
        try
        {
            var userInfo = await _profileRepository.GetAll()
                .Select(x => new ProfileViewModel()
                {
                    Name = x.Name,
                    Organization = x.Organization,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.User.Email
                }).FirstOrDefaultAsync(x => x.UserName == userName);

            return new BaseResponse<ProfileViewModel>()
            {
                Data = userInfo,
                StatusCode = StatusCode.OK
            };
        }
        catch
        {
            throw;
        }
    }
}
