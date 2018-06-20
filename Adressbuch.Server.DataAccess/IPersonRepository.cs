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
        Task AddPersonAsync(PersonDto personDto);

        Task UpdatePersonAsync(Guid id, PersonDto personDto);

        Task DeletePersonAsync(Guid id);
    }
}
