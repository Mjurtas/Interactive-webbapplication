using Bjornroth.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Models.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; set; }

        public string Poster { get; set; }
        [Display(Name = "Number Of Likes")]
        public int NumberOfLikes { get; set; } = 0;

        [Display(Name = "Number Of Dislikes")]
        public int NumberOfDislikes { get; set; } = 0;

        public MovieViewModel(MovieDTO movie, MovieDTO movie2)
        {
            Title = movie.Title;
            Poster = movie.Poster;
            if (movie2 != null)
            {
                NumberOfLikes = movie2.NumberOfLikes;
                NumberOfDislikes = movie2.NumberOfDislikes;
            }
        }
    }
}
