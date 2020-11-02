using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bjornroth.Models.DTO;
using Newtonsoft.Json;

namespace Bjornroth.Models.ViewModels
{
    public class TopListViewModel : BaseViewModel
    {
        public List<MovieDTO> TopList { get; set; } = new List<MovieDTO>();


        public TopListViewModel(List<MovieDTO> mostVoted)
        {
            for (int i = 0; i < 4; i++)
            {
                DateTime date;

                if (DateTime.TryParse(mostVoted[i].Released, out date))
                {
                    mostVoted[i].Released = date.ToString("yyyy");
                }

                if (mostVoted[i].Poster == "N/A")
                {
                    mostVoted[i].Poster = "../images/posterlessPoster.png";
                }
                if(mostVoted[i].Plot == "N/A")
                {
                    mostVoted[i].Plot = "This movie doesn't have a plot.";
                }
                TopList.Add(mostVoted[i]);
            }
        }
    }
}
