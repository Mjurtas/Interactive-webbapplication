using Bjornroth.Interfaces;
using Bjornroth.Models.DTO;
using Bjornroth.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bjornroth.Repositories
{



    public class CmdbRepository : ICmdbRepository
    {
        string baseUrl1;
        string baseUrl2;
        public CmdbRepository(IConfiguration config)
        {
            baseUrl1 = config.GetValue<string>("CMDBApi:BaseUrl1");
            baseUrl2 = config.GetValue<string>("CMDBApi:BaseUrl2");
        }

        private async Task<MovieDTO> Connect(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDTO>(data);
                return result;
            }
        }


        public async Task<MovieDTO> GetSearchResult(string searchInput)
        {
            string endpoint = $"{baseUrl1}t={searchInput}";
            return await Connect(endpoint);
        }

        //returns a MovieObject with from CMDb with title, likes, dislikes.
        public async Task<MovieDTO> GetCmdbRating(string imdbId)
        {
            string endpoint = $"{baseUrl2}movie/{imdbId}";
            return await Connect(endpoint);
        }

        /*This Method returns a SearchDTO - which holds a List<MovieDTO> Search*/
        public async Task<SearchDTO> GetSearchResults(string searchInput)
        {
            
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl1}s={searchInput}";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SearchDTO>(data);
                return result;
            }
        }

        // This method returns a movie object, and is used by the randomizer on the startpage. 
        public async Task<MovieDTO> GetSearchResultById(string imdbId)
        {
            string endpoint = $"{baseUrl1}i={imdbId}";
            return await Connect(endpoint);
        }

        //This method returns a detailed movie-object with full plot
        public async Task<MovieDTO> GetSearchResultByIdFullPlot(string imdbId)
        {
            string endpoint = $"{baseUrl1}i={imdbId}&plot=full";
            return await Connect(endpoint);
        }

        public async Task<List<MovieDTO>> GetCurrentTopList(string sort)
        {
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl2}toplist?type={sort}&sort=desc&count=4";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<MovieDTO>>(data);
                return result;
            }
        }

        public async Task<MovieDTO> UpdateRating(string imdbId, string newRating)
        {
            string endpoint = $"{baseUrl2}movie/{imdbId}/{newRating}";
            return await Connect(endpoint);
        }

        public async void GetMovies()
        {
           
                using (HttpClient client = new HttpClient())
                {
                    string endpoint = $"{baseUrl2}movie"; // Endpoint for full list of movies in CMDB api
                    var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();
                if (response.StatusCode.ToString() == "200")
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<MovieDTO>>(data);
                    for (int i = 0; i < result.Count; i++)  //For every movie in CMDB....
                    {
                        string endpoint2 = $"{baseUrl1}i={result[i].ImdbId}"; //...set the likes/dislikes for every movie in OMDB
                        var result2 = await Connect(endpoint2);
                        result2.NumberOfLikes = result[i].NumberOfLikes;
                        result2.NumberOfDislikes = result[i].NumberOfDislikes;
                        result.RemoveAt(i);
                        result.Insert(i, result2);
                    }
                    var sortedList = result.OrderByDescending(x => x.TotalRatings = (x.NumberOfLikes + x.NumberOfDislikes));
                    //Convert the movie list to a string formatted as json
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(sortedList);
                    //Creates a file with the json string
                    System.IO.File.WriteAllText("movies.json", jsonString);
                    ////Sets  the json file to the movie list
                    //JsonConvert.DeserializeObject<List<MovieDTO>>(System.IO.File.ReadAllText("movies.json"));
                }
                

              
                }
            






        }

        public string FormatSearchString(string searchInput)
        {
            string newString = Regex.Replace(searchInput, "[^a-zA-Z0-9_.]+", " ");
            return newString;
        }

    }
}
