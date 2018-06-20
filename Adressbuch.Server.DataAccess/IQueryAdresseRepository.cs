using Adressbuch.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Server.DataAccess
{
    public interface IQueryAdresseRepository
    {
        Task< IEnumerable<AdresseDto>> GetAllAsync();

        Task<AdresseDto> GetByIdAsync(Guid id);

        Task<IEnumerable<AdresseDto>> GetByCriteriaAsync(AdresseDto searchCriteria);

        Task<IEnumerable<AdresseDto>> GetByPersonIdAsync(Guid personId);
    }
}
