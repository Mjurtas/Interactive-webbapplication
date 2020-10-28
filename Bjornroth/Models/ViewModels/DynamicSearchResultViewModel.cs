using Bjornroth.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Models.ViewModels
{
    public class DynamicSearchResultViewModel
    {
        public List<MovieDTO> MovieList { get; set; }

        public DynamicSearchResultViewModel(List<MovieDTO> listOfMovies)
        {
            MovieList = listOfMovies;
        }
   
    }
}
