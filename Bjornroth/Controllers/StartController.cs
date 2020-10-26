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
using Bjornroth.Models.DTO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bjornroth.Controllers
{
    public class StartController : Controller
    {
        private readonly ILogger<StartController> _logger;
        private ICmdbRepository cmdbRepository;
        Random random = new Random();
        List<MovieViewModel> generatedMovies = new List<MovieViewModel>();
        public StartController(ILogger<StartController> logger, ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
            _logger = logger;
        }
        
        //public async Task <IActionResult> Index()
        //{
        //    for (var i = 0; i < 3; i++) {
        //        int number = random.Next(1, 1000000);
        //        string digits = number.ToString("0000000");
        //        string imdbId = "tt" + digits;
        //        var model = await cmdbRepository.GetSearchResultById(imdbId);
        //        var fullModel = await cmdbRepository.GetCmdbRating(imdbId);
        //        MovieViewModel completeMovie = new MovieViewModel(model, fullModel);
        //        generatedMovies.Add(completeMovie);
        //    }
        //    StartViewModel viewModel = new StartViewModel(generatedMovies);
        //    return View(viewModel);

        //}

        public async Task<IActionResult> Index(string imdbId, string newRating)
        {
            if (imdbId != null && newRating != null)
            {
                //TODO: Move this to Javascript so the page doesn't have to load when the user updates a movie's rating
                await cmdbRepository.UpdateRating(imdbId, newRating);
                StartViewModel viewModel = new StartViewModel();
                return View(viewModel);
            }
            else
            {
                //TODO: Filter to only show movies and filter away movies that doesn't have posters
                for (var i = 0; i < 3; i++)
                {
                    int number = random.Next(1, 1000000);
                    string digits = number.ToString("0000000");
                    string id = "tt" + digits;
                    var model = await cmdbRepository.GetSearchResultById(id);
                    if (model.Type == "movie") 
                    {
                        var fullModel = await cmdbRepository.GetCmdbRating(id);
                        MovieViewModel completeMovie = new MovieViewModel(model, fullModel);
                        generatedMovies.Add(completeMovie);
                    }
                    else
                    {
                        i--;
                    }
                }
                StartViewModel viewModel = new StartViewModel(generatedMovies);
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
