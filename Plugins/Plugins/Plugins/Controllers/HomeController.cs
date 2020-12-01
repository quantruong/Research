using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plugins.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Plugins.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
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

        public async Task<IActionResult> Upload(PluginInfoModel model)
        {
            long size = model.Files.Sum(f => f.Length);
            var folderPath = Path.Combine(_hostEnvironment.ContentRootPath, "Plugins", model.Name);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            foreach (var formFile in model.Files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(folderPath, formFile.FileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = model.Files.Count, size });
        }
    }

    public class PluginInfoModel
    {
        public string Name { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
