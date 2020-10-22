using Bjornroth.Models.DTO;
using Bjornroth.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Interfaces
{
    public interface ICmdbRepository
    {

        Task<MovieDTO> GetSearchResult(string searchInput);

        Task<MovieDTO> GetCmdbRating(string imdb);

        Task<SearchDTO> GetSearchResults(string searchInput);
    }
}
