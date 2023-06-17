using AutoSchedule.DAL.Repositories;
using AutoSchedule.Models.ViewModels.Load;
using AutoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

public class LoadController : Controller
{
    private readonly ILoadService _loadService;

    public LoadController(ILoadService loadService)
    {
        _loadService = loadService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLoad()
    {
        var response = await _loadService.GetLoad(User.Identity.Name);
        if(response.StatusCode == Models.Enum.StatusCode.OK){
            return View(response.Data);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> GetLoadList(IFormFile formFile)
    {
        var response = await _loadService.GetLoadList(formFile, User.Identity.Name);
        if(response.StatusCode == Models.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetLoad", "Load");
        }
        return RedirectToAction("Index", "Home");
    }
}
