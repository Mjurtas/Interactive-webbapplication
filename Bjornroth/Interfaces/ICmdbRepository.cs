using Bjornroth.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bjornroth.Interfaces
{
    public interface ICmdbRepository
    {

        Task<MovieDTO> GetSearchResult(string searchInput);


    }
}
