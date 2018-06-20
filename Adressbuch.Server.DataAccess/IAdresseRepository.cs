using Adressbuch.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Server.DataAccess
{
    public interface IAdresseRepository : IQueryAdresseRepository
    {
        Task AddAdresseAsync(AdresseDto adresseDto);

        Task UpdateAdresseAsync(Guid id, AdresseDto adresseDto);

        Task DeleteAdresseAsync(Guid id);
    }
}
