using Bjornroth.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Models.ViewModels
{
    public class SearchViewModel
    {
        public List<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
        //public string Title { get; set; }
        //public string Poster { get; set; }
        //public string Plot { get; set; }

        public SearchViewModel(SearchDTO search)
        {
            //Movies = search.Search;
            for (int i = 0; i < 4; i++)
            {
                Movies.Add(search.Search[i]);
                //this.Title = search.Search[i].Title;
                //this.Poster = search.Search[i].Poster;
                //this.Plot = search.Search[i].Plot;

            }
            
        }

    }
}
