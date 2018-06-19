using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch.Server.DbModel
{
    public class AdressbuchDbContext : DbContext
    {
        public AdressbuchDbContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Person> Personen { get; set; }
        public DbSet<Adresse> Adressen { get; set; }
    }
}
