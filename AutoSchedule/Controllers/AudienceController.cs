using AutoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

public class AudienceController : Controller
{
    private readonly IAudienceService _audienceService;

    public AudienceController(IAudienceService audienceService)
    {
        _audienceService = audienceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAudiences()
    {
        var responce = await _audienceService.GetAudiences();
        if(responce.StatusCode == Models.Enum.StatusCode.OK)
        {
            return View(responce.Data);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<ActionResult> GetAudienceList(IFormFile formFile)
    {
        var response = await _audienceService.AddAudienceList(formFile, User.Identity.Name);
        if(response.StatusCode == Models.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAudiences", "Audience");
        }
        return RedirectToAction("Index", "Home");
    }
}