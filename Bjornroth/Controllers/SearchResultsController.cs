using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        [Route("/SearchResult")]
        public async Task<IActionResult> Index(string searchInput)
        {
            if (searchInput != null)
            {
                string formattedString = cmdbRepository.FormatSearchString(searchInput);
                var model = await cmdbRepository.GetSearchResults(formattedString);
                if (model.Search != null)
                {
                    var model2 = await cmdbRepository.GetSearchResultById(model.Search[0].ImdbId);
                    model.Search[0] = model2;
                    foreach (MovieDTO movie in model.Search)
                    {
                        string imdbId = movie.ImdbId;
                        var model3 = await cmdbRepository.GetCmdbRating(imdbId);
                        if (model3 != null)
                        {
                            movie.NumberOfLikes = model3.NumberOfLikes;
                            movie.NumberOfDislikes = model3.NumberOfDislikes;
                        }
                    }
                    SearchViewModel viewModel = new SearchViewModel(model, searchInput);
                    if (viewModel.Movies.Count >= 1)
                    {
                        return View(viewModel);
                    }
                }
            }
            return RedirectToAction("PageNotFound");
        }

        public IActionResult PageNotFound()
        {
            BaseViewModel viewModel = new BaseViewModel();
            return View(viewModel);
        }

        
    }
}
