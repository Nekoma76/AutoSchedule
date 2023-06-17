using AutoSchedule.DAL;
using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;
using AutoSchedule.Models.ViewModels.Teacher;
using AutoSchedule.Services.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;

namespace AutoSchedule.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly IBaseRepository<Teacher> _teacherRepository;

        public TeacherController(ITeacherService teacherService, IBaseRepository<Teacher> teacherRepository)
        {
            _teacherService = teacherService;
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var response = await _teacherService.GetTeachers();
            if(response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AddTeacher() => View();

        [HttpPost]
        public async Task<IActionResult> AddTeacher(TeacherViewModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _teacherService.Create(model, User.Identity.Name);
                if(response.StatusCode == Models.Enum.StatusCode.OK){
                    return RedirectToAction("GetTeachers", "Teacher");
                }
                ModelState.AddModelError("", response.Description);
            }
            return RedirectToAction("Index", "Home");
        }

       [HttpPost]
        public async Task<IActionResult> AddTeacherList(IFormFile formFile)
        {
            var response = await _teacherService.AddTeacherList(formFile, User.Identity.Name);
            if(response.StatusCode == Models.Enum.StatusCode.OK){
                return RedirectToAction("GetTeachers", "Teacher");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
