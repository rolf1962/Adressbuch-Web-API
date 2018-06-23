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
            this.Database.CreateIfNotExists();

            if (Personen.Count() == 0)
            {
                InitializeData();
            }
        }

        private void InitializeData()
        {
            Person rolf = new Person() { Id = Guid.NewGuid(), Name = "Jansen", Vorname = "Rolf", Geburtsdatum = new DateTime(1962, 2, 24), Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "Rolf", ModifiedBy = "Rolf" };
            Person susanne = new Person() { Id = Guid.NewGuid(), Name = "Weitzel-Jansen", Vorname = "Susanne", Geburtsdatum = new DateTime(1966, 3, 1), Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "Rolf", ModifiedBy = "Rolf" };
            Person max = new Person() { Id = Guid.NewGuid(), Name = "Jansen", Vorname = "Maximilian", Geburtsdatum = new DateTime(2000, 4, 8), Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "Rolf", ModifiedBy = "Rolf" };
            Person alex = new Person() { Id = Guid.NewGuid(), Name = "Jansen", Vorname = "Alexander", Geburtsdatum = new DateTime(2007, 9, 7), Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = "Rolf", ModifiedBy = "Rolf" };

            Personen.AddRange(new Person[] { rolf, susanne, max, alex });

            SaveChanges();
        }

        public DbSet<Person> Personen { get; set; }
        public DbSet<Adresse> Adressen { get; set; }
    }
}
