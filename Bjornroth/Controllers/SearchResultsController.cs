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
        [Route("/SearchResult/")]
        public async Task<IActionResult> Index(string searchInput)
        {
            if (searchInput != null)
                
            {    //Formats string to avoid bugs where "-" and such returns the wrong search results
                string formattedString = cmdbRepository.FormatSearchString(searchInput);

                /* This method returns a SearchDTO which holds a List<MovieDTO> */
                var model = await cmdbRepository.GetSearchResults(formattedString);
                if (model.Search != null)
                {
                    for (int i = 0; i < model.Search.Count - 1; i++)
                    {
                        var model1 = await cmdbRepository.GetSearchResultById(model.Search[i].ImdbId);
                        
                        // If the movie exists in the CMDb, it assigns the likes/dislikes to the movie.
                        string imdbId = model1.ImdbId;
                        var model3 = await cmdbRepository.GetCmdbRating(imdbId);
                        if (model3 != null)
                        {
                            model1.NumberOfLikes = model3.NumberOfLikes;
                            model1.NumberOfDislikes = model3.NumberOfDislikes;
                        }
                        model.Search.RemoveAt(i);
                        model.Search.Insert(i, model1);
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
