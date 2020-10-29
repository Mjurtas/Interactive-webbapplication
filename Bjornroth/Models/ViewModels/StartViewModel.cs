using Bjornroth.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Models.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public List<MovieViewModel> GeneratedMovies { get; set; } = new List<MovieViewModel>();
        //public List<MovieDTO> SearchSuggestions { get; set; } = new List<MovieDTO>();

        

        //TODO: Delete this empty constructor when StartController isn't handling the like function anymore

        public StartViewModel() { }

        public StartViewModel(List<MovieViewModel> generatedMovies)
        {
            for (int i = 0; i < generatedMovies.Count; i++)
            {
                if (generatedMovies[i].Type == "movie")
                {
                    if (generatedMovies[i].Poster == "N/A")
                    {
                        generatedMovies[i].Poster = "../images/posterlessPoster.png";
                    }
                    if (generatedMovies[i].Plot == "N/A")
                    {
                        generatedMovies[i].Plot = "This movie doesn't have a plot";
                    }
                    GeneratedMovies.Add(generatedMovies[i]);
                }
            }
        }
    }
}
