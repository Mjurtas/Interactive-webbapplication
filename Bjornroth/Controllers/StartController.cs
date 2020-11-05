using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;
using Bjornroth.Models;
using Bjornroth.Interfaces;
using Bjornroth.Models.ViewModels;
using Bjornroth.Models.DTO;

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Bjornroth.Controllers
{
    public class StartController : Controller
    {
        private readonly ILogger<StartController> _logger;
        private ICmdbRepository cmdbRepository;
        Random random = new Random();
        List<MovieViewModel> generatedMovies = new List<MovieViewModel>();
        List<MovieDTO> ratedMovies = new List<MovieDTO>();
        public StartController(ILogger<StartController> logger, ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
            _logger = logger;
            cmdbRepository.GetMovies();
        }

        public async Task<IActionResult> Index()
        {   
            List<MovieDTO> cmdbRatedMovies = JsonConvert.DeserializeObject<List<MovieDTO>>(System.IO.File.ReadAllText("movies.json"));
            
            //Randomizer for the carousels
            for (var i = 0; i < 3; i++)
                {
                    int number = random.Next(1, 1000000);
                int index = random.Next(cmdbRatedMovies.Count);
                    string digits = number.ToString("0000000");
                    string id = "tt" + digits;
                    var model = await cmdbRepository.GetSearchResultById(id);
                    if (model.Type == "movie")
                    {
                        var fullModel = await cmdbRepository.GetCmdbRating(id);
                        MovieViewModel completeMovie = new MovieViewModel(model, fullModel);
                        generatedMovies.Add(completeMovie);
                        ratedMovies.Add(cmdbRatedMovies[index]);

                    }
                    else
                    {
                        i--;
                    }
                }
            StartViewModel viewModel = new StartViewModel(generatedMovies, ratedMovies);
                return (IActionResult)View(viewModel);
        }
    }
}
