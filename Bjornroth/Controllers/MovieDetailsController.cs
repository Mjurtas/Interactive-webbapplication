﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bjornroth.Controllers
{
    public class MovieDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}