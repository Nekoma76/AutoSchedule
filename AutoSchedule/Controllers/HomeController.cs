using AutoSchedule.Models;
using AutoSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoSchedule.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProfileService _profileService;

        public HomeController(ILogger<HomeController> logger, IProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _profileService.GetInfo(User.Identity.Name);
            if(response.StatusCode == Models.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}