using Bjornroth.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Models.ViewModels
{
    public class BaseViewModel
    {
        public List<MovieDTO> SearchSuggestions = JsonConvert.DeserializeObject<List<MovieDTO>>(System.IO.File.ReadAllText("movies.json"));
    }
}
