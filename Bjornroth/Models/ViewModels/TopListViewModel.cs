using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjornroth.Models.DTO;
using Newtonsoft.Json;

namespace Bjornroth.Models.ViewModels
{
    public class TopListViewModel
    {
        public List<MovieDTO> TopList { get; set; } = new List<MovieDTO>();
        public List<MovieDTO> SearchSuggestions = JsonConvert.DeserializeObject<List<MovieDTO>>(System.IO.File.ReadAllText("movies.json"));

        public TopListViewModel(List<MovieDTO> mostVoted)
        {
            for (int i = 0; i < 4; i++)
            {
                TopList.Add(mostVoted[i]);
            }
        }
    }
}
