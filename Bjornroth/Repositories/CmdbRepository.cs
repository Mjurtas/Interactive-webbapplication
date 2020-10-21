using Bjornroth.Interfaces;
using Bjornroth.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<IEnumerable<MovieDTO>> GetSearchResult()
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                string endpoint = $"{baseUrl}t=Star+wars";
                var response = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<MovieDTO>>(data);
                return result;
            }
        }

       
    }
}
