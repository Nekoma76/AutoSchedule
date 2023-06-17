using AutoSchedule.Models.Entity;
using AutoSchedule.Models.Response;
using AutoSchedule.Models.ViewModels.Teacher;
using System.Data;
using System.Security.Claims;

namespace AutoSchedule.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<BaseResponse<IEnumerable<TeacherViewModel>>> GetTeachers();
        Task<BaseResponse<Teacher>> Create(TeacherViewModel model, string userName);
        Task<BaseResponse<ClaimsIdentity>> AddTeacherList(IFormFile formFile, string userName);
    }
}
