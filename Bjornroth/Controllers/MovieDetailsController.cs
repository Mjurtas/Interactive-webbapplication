using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjornroth.Interfaces;
using Bjornroth.Models.DTO;
using Bjornroth.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bjornroth.Controllers
{
    public class MovieDetailsController : Controller
    {
        private ICmdbRepository cmdbRepository;
        //public <MovieDTO> movies;

        public MovieDetailsController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
            //this.movies = Startup.Movies;
        }

        public async Task<IActionResult> Index(string imdbId)
        {
            
            
                var model = await cmdbRepository.GetSearchResultByIdFullPlot(imdbId);
                string id = model.ImdbId;
            
                var model2 = await cmdbRepository.GetCmdbRating(id);


                MovieViewModel viewModel = new MovieViewModel(model, model2);
                return View(viewModel);
            
            
        }
    }
}
