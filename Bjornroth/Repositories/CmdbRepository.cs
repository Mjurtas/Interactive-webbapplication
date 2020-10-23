using Bjornroth.Interfaces;
using Bjornroth.Models.DTO;
using Bjornroth.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Versioning;
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

        public async Task<MovieDTO> GetSearchResult(string searchInput)
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {

                string endpoint = $"{baseUrl1}t={searchInput}";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDTO>(data);
                return result;
            }
        }

        public async Task<MovieDTO> GetCmdbRating(string imdbId)
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {

                string endpoint = $"{baseUrl2}movie/{imdbId}";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDTO>(data);
                return result;
            }
        }

        public async Task<SearchDTO> GetSearchResults(string searchInput)
        {
            //TODO: Fixa så att koden inte upprepas
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

        public async Task<MovieDTO> GetSearchResultById(string imdbId)
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl1}i={imdbId}";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDTO>(data);
                return result;
            }
        }

        public async Task<MovieDTO> GetSearchResultByIdFullPlot(string imdbId)
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl1}i={imdbId}&plot=full";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDTO>(data);
                return result;
            }
        }

        public async Task<List<MovieDTO>> GetCurrentTopList()
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl2}toplist?type=popularity&sort=desc&count=4";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<MovieDTO>>(data);
                return result;
            }
        }

        public async Task<MovieDTO> UpdateRating(string imdbId, string newRating)
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl2}movie/{imdbId}/{newRating}";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDTO>(data);
                return result;
            }
        }
    }
}
