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
        public List<MovieDTO> RatedMovies { get; set; } = new List<MovieDTO>();

        public StartViewModel(List<MovieViewModel> generatedMovies, List<MovieDTO> ratedMovies)
        {
            for (int i = 0; i < generatedMovies.Count; i++)
            {
                if (generatedMovies[i].Type == "movie")
                {
                    if (generatedMovies[i].Poster == "N/A")
                    {
                        generatedMovies[i].Poster = "../images/posterlessPoster.png";
                    }
                    if (generatedMovies[i].Runtime == "N/A")
                    {
                        generatedMovies[i].Runtime = "Length unknown";
                    }
                    if (ratedMovies[i].Poster == "N/A")
                    {
                        ratedMovies[i].Poster = "../images/posterlessPoster.png";
                    }
                    if (ratedMovies[i].Runtime == "N/A")
                    {
                        ratedMovies[i].Runtime = "Length unknown";
                    }
                    GeneratedMovies.Add(generatedMovies[i]);
                    RatedMovies.Add(ratedMovies[i]);
                }
            }
        }
    }
}
