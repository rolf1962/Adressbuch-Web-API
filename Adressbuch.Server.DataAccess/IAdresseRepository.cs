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
        void AddAdresse(AdresseDto adresseDto);

        void UpdateAdresse(Guid id, AdresseDto adresseDto);

        void DeleteAdresse(Guid id);
    }
}
