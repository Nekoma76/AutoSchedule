using AutoSchedule.Models.ViewModels.Schedule;
using AutoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

public class ScheduleController : Controller
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public IActionResult CreateSchedule() => View();

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(ScheduleViewModel model)
    {
        var response = await _scheduleService.Create(model, User.Identity.Name);
        if(response.StatusCode == Models.Enum.StatusCode.OK)
        {
            return View();
        }
        return RedirectToAction("Index", "Home");
    }
}