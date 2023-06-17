using AutoSchedule.Controllers;
using AutoSchedule.DAL.Interfaces;
using AutoSchedule.DAL.Repositories;
using AutoSchedule.Models.Entity;
using AutoSchedule.Services.Implementations;
using AutoSchedule.Services.Interfaces;

namespace AutoSchedule
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Teacher>,  TeacherRepository>();
            services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
            services.AddScoped<IBaseRepository<Audience>, AudienceRepository>(); 
            services.AddScoped<IBaseRepository<Load>, LoadRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAudienceService, AudienceService>();
            services.AddScoped<ILoadService, LoadService>();
            services.AddScoped<IScheduleService, ScheduleService>();
        }
    }
}
