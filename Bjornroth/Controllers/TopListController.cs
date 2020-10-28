using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjornroth.Interfaces;
using Bjornroth.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bjornroth.Controllers
{
    public class TopListController : Controller
    {
        private ICmdbRepository cmdbRepository;

        public TopListController(ICmdbRepository cmdbRepository)
        {
            this.cmdbRepository = cmdbRepository;

        }

        //public async Task<IActionResult> Index()
        //{
        //    var model = await cmdbRepository.GetSearchResults("star+wars");
        //    SearchViewModel viewModel = new SearchViewModel(model);
        //    return View(viewModel);
        //}

        public async Task<IActionResult> Index()
        {
            var model = await cmdbRepository.GetCurrentTopList();

            for (var i = 0; i < model.Count; i++) 
            {
                var movie = await cmdbRepository.GetSearchResultById(model[i].ImdbId);
                movie.NumberOfLikes = model[i].NumberOfLikes;
                movie.NumberOfDislikes = model[i].NumberOfDislikes;
                model.RemoveAt(i);
                model.Insert(i, movie);
            }
            //var model2 = await cmdbRepository.GetCmdbRating(model[i].ImdbId);
            TopListViewModel viewModel = new TopListViewModel(model);
            return View(viewModel);
        }
    }
}
