using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bjornroth.Models;

using Bjornroth.Interfaces;
using Bjornroth.Models.ViewModels;

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
            if (searchInput != null)
            {
                var model = await cmdbRepository.GetSearchResult(searchInput);

                string imdbId = model.ImdbId;

                var model2 = await cmdbRepository.GetCmdbRating(imdbId);

                MovieViewModel viewModel = new MovieViewModel(model, model2);
                return View(viewModel);
            }
            else
            {

                var model = await cmdbRepository.GetSearchResult("Jedi");

                string imdbId = model.ImdbId;

                var model2 = await cmdbRepository.GetCmdbRating(imdbId);

                MovieViewModel viewModel = new MovieViewModel(model, model2);
                return View(viewModel);
            }


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
