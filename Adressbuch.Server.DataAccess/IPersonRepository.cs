using Adressbuch.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Server.DataAccess
{
    public interface IPersonRepository : IQueryPersonRepository
    {
        void AddPerson(PersonDto personDto);

        void UpdatePerson(Guid id, PersonDto personDto);

        void DeletePerson(Guid id);
    }
}
