using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjornroth.Interfaces;
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
    }
}
