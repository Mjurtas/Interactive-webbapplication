using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjornroth.Interfaces;
using Bjornroth.Models.DTO;
using Bjornroth.Models.ViewModels;
using Bjornroth.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bjornroth.Controllers
{
    public class SearchResultsController : Controller
    {
        private ICmdbRepository cmdbRepository;

        public SearchResultsController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;
            
        }

        public async Task<IActionResult> Index(string searchInput)
        {
            if (searchInput != null)
            {
                var model = await cmdbRepository.GetSearchResults(searchInput);

                foreach (MovieDTO movie in model.Search)
                {
                    string imdbId = movie.ImdbId;
                    var model2 = await cmdbRepository.GetCmdbRating(imdbId);
                    if (model2 != null) {
                        movie.NumberOfLikes = model2.NumberOfLikes;
                        movie.NumberOfDislikes = model2.NumberOfDislikes;
                    }
                }

                //string imdbId = movie.ImdbId;

                //var model2 = await cmdbRepository.GetCmdbRating(imdbId);

                //MovieViewModel viewModel = new MovieViewModel(model, model2);
                SearchViewModel viewModel = new SearchViewModel(model);
                return View(viewModel);
            }
            else
            {

                var model = await cmdbRepository.GetSearchResults("Jedi");
                foreach (MovieDTO movie in model.Search)
                {
                    string imdbId = movie.ImdbId;
                    var model2 = await cmdbRepository.GetCmdbRating(imdbId);
                    if (model2 != null)
                    {
                        movie.NumberOfLikes = model2.NumberOfLikes;
                        movie.NumberOfDislikes = model2.NumberOfDislikes;
                    }
                }
                SearchViewModel viewModel = new SearchViewModel(model);
                return View(viewModel);
            }


        }
    }
}
