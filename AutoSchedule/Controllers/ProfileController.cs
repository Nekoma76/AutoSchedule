using AutoSchedule.Models.ViewModels.Profile;
using AutoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    public IActionResult EditProfile() => View();

    [HttpPost]
    public async Task<IActionResult> EditProfile(ProfileViewModel model)
    {
        //if(ModelState.IsValid){
            var response = await _profileService.Create(model, User.Identity.Name);
            //if(response.StatusCode == Models.Enum.StatusCode.OK)
            //{
                return RedirectToAction("Index", "Home");
            //}
          //  ModelState.AddModelError("", response.Description);
        //}
        //return View();
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        string userName = User.Identity.Name;
        var response = await _profileService.GetInfo(userName);
        if(response.StatusCode == Models.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return RedirectToAction("Index", "Home");
    }
}
