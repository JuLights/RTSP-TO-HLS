using Microsoft.AspNetCore.Mvc;
using RTSP_TO_HLS.Models;
using System.Diagnostics;
using FFMpegCore;
using RTSP_TO_HLS.Services;

namespace RTSP_TO_HLS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StreamingService _streamingService;
        private static bool _isStreaming = false;
        public HomeController(ILogger<HomeController> logger,StreamingService streamingService)
        {
            _logger = logger;
            _streamingService = streamingService;

            GlobalFFOptions.Configure(options => options.BinaryFolder = "./bin");
        }

        public async Task<IActionResult> Index()
        {
            RedirectToAction("VideoOutput","Home");
            if (!_isStreaming)
            {
                await _streamingService.Streamer("rtsp://localhost:8554/");
                _isStreaming = true;
            }
                

            return View();
        }

        public IActionResult VideoOutput()
        {

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