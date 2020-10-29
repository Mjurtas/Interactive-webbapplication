using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Bjornroth.Models.DTO
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public string Released { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string Runtime { get; set; }
        //public List<RatingDTO> Ratings { get; set;}
        public string imdbRating { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public int TotalRatings { get; set; }

       
        
    }
}
