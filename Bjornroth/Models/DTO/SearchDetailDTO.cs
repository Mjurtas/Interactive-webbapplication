using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Models.DTO
{
    public class SearchDetailDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
