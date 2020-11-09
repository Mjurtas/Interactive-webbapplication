using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Models.DTO
{
    public class SearchDTO
    {
        public List<MovieDTO> Search { get; set; }

        public int TotalResults { get; set; }

        public bool Response { get; set; }
    }
}
