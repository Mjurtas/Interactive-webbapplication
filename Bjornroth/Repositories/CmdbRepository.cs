using Bjornroth.Interfaces;
using Bjornroth.Models.DTO;
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
        string baseUrl;
        public CmdbRepository(IConfiguration config)
        {
            baseUrl = config.GetValue<string>("CMDBApi:BaseUrl");
        }

        public async Task<MovieDTO> GetSearchResult(string searchInput)
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {

                string endpoint = $"{baseUrl}t={searchInput}";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDTO>(data);
                return result;
            }
        }

       
    }
}
