using Adressbuch.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Server.DataAccess
{
    public interface IQueryPersonRepository
    {
        Task<IEnumerable<PersonDto>> GetAllAsync();

        Task<PersonDto> GetByIdAsync(Guid id);

        Task<IEnumerable<PersonDto>> GetByCriteriaAsync(PersonDto searchCriteria);

        Task<IEnumerable<PersonDto>> GetByAdresseIdAsync(Guid adresseId);
    }
}
