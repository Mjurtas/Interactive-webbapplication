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

        public SearchViewModel(SearchDTO search)
        {
            for (int i = 0; i < search.Search.Count; i++)
            {
                if (search.Search[i].Type == "movie")
                {
                    if (search.Search[i].Poster == "N/A")
                    {
                        search.Search[i].Poster = "../images/posterlessPoster.png";
                    }
                    if (search.Search[i].Plot == "N/A")
                    {
                        search.Search[i].Plot = "This movie doesn't have a plot";
                    }
                    Movies.Add(search.Search[i]);
                }

            }
            
        }

    }
}
