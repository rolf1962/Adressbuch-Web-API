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
        IEnumerable<PersonDto> GetAll();

        PersonDto GetById(Guid id);

        IEnumerable<PersonDto> Get(PersonDto searchCriteria);

        IEnumerable<PersonDto> GetByAdresseId(Guid adresseId);
    }
}
