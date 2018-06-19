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
        IEnumerable<AdresseDto> GetAll();

        AdresseDto GetById(Guid id);

        IEnumerable<AdresseDto> Get(AdresseDto searchCriteria);

        IEnumerable<AdresseDto> GetByPersonId(Guid personId);
    }
}
