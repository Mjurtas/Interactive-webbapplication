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
       
        public async Task<IActionResult> Index()
        {
            try { 
            var model = await cmdbRepository.GetCurrentTopList("popularity");
            var model2 = await cmdbRepository.GetCurrentTopList("rating");

            for (var i = 0; i < model.Count; i++) 
            {
                var movie = await cmdbRepository.GetSearchResultById(model[i].ImdbId);
                movie.NumberOfLikes = model[i].NumberOfLikes;
                movie.NumberOfDislikes = model[i].NumberOfDislikes;
                model.RemoveAt(i);
                model.Insert(i, movie);
            }

            for (var i = 0; i < model2.Count; i++)
            {
                var movie = await cmdbRepository.GetSearchResultById(model2[i].ImdbId);
                movie.NumberOfLikes = model2[i].NumberOfLikes;
                movie.NumberOfDislikes = model2[i].NumberOfDislikes;
                model2.RemoveAt(i);
                model2.Insert(i, movie);
            }


            TopListViewModel viewModel = new TopListViewModel(model, model2);
            return View(viewModel);
            }

            catch
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
