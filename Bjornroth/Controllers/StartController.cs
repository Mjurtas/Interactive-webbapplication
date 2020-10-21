using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bjornroth.Models;

using Bjornroth.Interfaces;

namespace Bjornroth.Controllers
{
    public class StartController : Controller
    {
        private readonly ILogger<StartController> _logger;
        private ICmdbRepository cmdbRepository;
        public StartController(ILogger<StartController> logger, ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
            _logger = logger;
        }
        
        public async Task <IActionResult> Index(string searchInput)
        {
            
            var model = await cmdbRepository.GetSearchResult(searchInput);
            return View(model);
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
