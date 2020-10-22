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
            var model = await cmdbRepository.GetSearchResults("star+wars");
            SearchViewModel viewModel = new SearchViewModel(model);
            return View(viewModel);
        }
    }
}
