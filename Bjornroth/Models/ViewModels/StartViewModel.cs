using Bjornroth.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Models.ViewModels
{
    public class StartViewModel
    {
        public List<MovieViewModel> GeneratedMovies { get; set; } = new List<MovieViewModel>();

        //TODO: Delete this empty constructor when StartController isn't handling the like function anymore

        public StartViewModel() { }

        public StartViewModel(List <MovieViewModel> generatedMovies)
        {
            for (int i = 0; i < 3; i++)
            {
                GeneratedMovies.Add(generatedMovies[i]);
            }
        }
    }
}
