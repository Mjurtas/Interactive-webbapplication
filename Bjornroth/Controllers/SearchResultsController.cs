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
                    var model3 = await cmdbRepository.GetSearchResultById(model.Search[0].ImdbId);
                    model.Search[0] = model3;
                    SearchViewModel viewModel = new SearchViewModel(model, searchInput);
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("PageNotFound");
                }
            }
            else
            {
                return RedirectToAction("PageNotFound");
            }
        }

        public IActionResult PageNotFound()
        {
            BaseViewModel viewModel = new BaseViewModel();
            return View(viewModel);
        }

        
    }
}
