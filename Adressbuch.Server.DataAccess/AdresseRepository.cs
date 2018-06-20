using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Adressbuch.DataTransfer;
using Adressbuch.Server.DbModel;

namespace Adressbuch.Server.DataAccess
{
    public class AdresseRepository : IAdresseRepository
    {
        private AdressbuchDbContext _adressbuchDbContext;

        public AdresseRepository(AdressbuchDbContext adressbuchDbContext)
        {
            _adressbuchDbContext = adressbuchDbContext;
        }


        public async Task AddAdresseAsync(AdresseDto AdresseDto)
        {
            _adressbuchDbContext.Adressen.Add(CopyDtoToDbModel(AdresseDto));
            await _adressbuchDbContext.SaveChangesAsync();
        }

        public async Task DeleteAdresseAsync(Guid id)
        {
            _adressbuchDbContext.Adressen.Remove(await _adressbuchDbContext.Adressen.SingleOrDefaultAsync(p => p.Id == id));
            await _adressbuchDbContext.SaveChangesAsync();
        }

        public async Task UpdateAdresseAsync(Guid id, AdresseDto adresseDto)
        {
            Adresse Adresse = await _adressbuchDbContext.Adressen.SingleOrDefaultAsync(a => a.Id == id);
            if (null != Adresse)
            {
                Adresse.Plz = adresseDto.Plz;
                Adresse.Ort = adresseDto.Ort;
                Adresse.Strasse = adresseDto.Strasse;
                Adresse.Hausnr = adresseDto.Hausnr;
                Adresse.Id = adresseDto.Id;
                Adresse.Created = adresseDto.Created;
                Adresse.CreatedBy = Adresse.CreatedBy;
                Adresse.Modified = adresseDto.Modified;
                Adresse.ModifiedBy = adresseDto.ModifiedBy;
            }
            await _adressbuchDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdresseDto>> GetAllAsync()
        {
            var dbResult = await _adressbuchDbContext.Adressen.ToListAsync();
            return dbResult.Select(CopyDbModelToDto).ToList();
        }

        public async Task<IEnumerable<AdresseDto>> GetByPersonIdAsync(Guid personId)
        {
            var dbResult = await _adressbuchDbContext.Personen.SingleOrDefaultAsync(a => a.Id == personId);
            return dbResult?.Adressen.Select(CopyDbModelToDto);
        }

        public async Task<AdresseDto> GetByIdAsync(Guid id)
        {
            return CopyDbModelToDto(await _adressbuchDbContext.Adressen.SingleOrDefaultAsync(p => p.Id == id));
        }

        public async Task< IEnumerable<AdresseDto>> GetByCriteriaAsync(AdresseDto searchCriteria)
        {
            var filter = PredicateBuilder.True<Adresse>();

            if (!string.IsNullOrWhiteSpace(searchCriteria.Plz))
            {
                if (searchCriteria.Plz.StartsWith("%") && searchCriteria.Plz.EndsWith("%"))
                {
                    filter = filter.And(p => p.Plz.Contains(searchCriteria.Plz.Replace("%", "")));
                }
                else if (searchCriteria.Plz.StartsWith("%"))
                {
                    filter = filter.And(p => p.Plz.EndsWith(searchCriteria.Plz.Replace("%", "")));
                }
                else if (searchCriteria.Plz.EndsWith("%"))
                {
                    filter = filter.And(p => p.Plz.StartsWith(searchCriteria.Plz.Replace("%", "")));
                }
                else
                {
                    filter = filter.And(p => 0 == string.Compare(p.Plz, searchCriteria.Plz.Replace("%", ""), true));
                }
            }

            if (!string.IsNullOrWhiteSpace(searchCriteria.Ort))
            {
                if (searchCriteria.Ort.StartsWith("%") && searchCriteria.Ort.EndsWith("%"))
                {
                    filter = filter.And(p => p.Ort.Contains(searchCriteria.Ort.Replace("%", "")));
                }
                else if (searchCriteria.Ort.StartsWith("%"))
                {
                    filter = filter.And(p => p.Ort.EndsWith(searchCriteria.Ort.Replace("%", "")));
                }
                else if (searchCriteria.Ort.EndsWith("%"))
                {
                    filter = filter.And(p => p.Ort.StartsWith(searchCriteria.Ort.Replace("%", "")));
                }
                else
                {
                    filter = filter.And(p => 0 == string.Compare(p.Ort, searchCriteria.Ort.Replace("%", ""), true));
                }
            }

            var dbResult = await Task.Run(() => _adressbuchDbContext.Adressen.Where(filter));
            return dbResult.Select(CopyDbModelToDto);
        }

        private AdresseDto CopyDbModelToDto(Adresse Adresse)
        {
            AdresseDto adresseDto = new AdresseDto(
                plz: Adresse.Plz,
                ort: Adresse.Ort,
                strasse: Adresse.Strasse,
                hausnr: Adresse.Hausnr,
                id: Adresse.Id,
                createdBy: Adresse.CreatedBy,
                created: Adresse.Created,
                modifiedBy: Adresse.ModifiedBy,
                modified: Adresse.Modified);

            adresseDto.Personen = new List<PersonDto>(Adresse.Personen.Select(p => new PersonDto(
                    name: p.Name,
                    vorname: p.Vorname,
                    geburtsdatum: p.Geburtsdatum,
                    id: p.Id,
                    created: p.Created,
                    createdBy: p.CreatedBy,
                    modified: p.Modified,
                    modifiedBy: p.ModifiedBy)));

            return adresseDto;
        }

        private Adresse CopyDtoToDbModel(AdresseDto adresseDto)
        {
            Adresse Adresse = new Adresse()
            {
                Plz = adresseDto.Plz,
                Ort = adresseDto.Ort,
                Strasse = adresseDto.Strasse,
                Hausnr = adresseDto.Hausnr,
                Id = adresseDto.Id,
                Created = adresseDto.Created,
                CreatedBy = adresseDto.CreatedBy,
                Modified = adresseDto.Modified,
                ModifiedBy = adresseDto.ModifiedBy
            };

            foreach (PersonDto personDto in adresseDto.Personen)
            {
                Person person = _adressbuchDbContext.Personen.SingleOrDefault(a => a.Id == adresseDto.Id);

                if (null == person)
                {
                    person = new Person()
                    {
                        Name = personDto.Name,
                        Vorname = personDto.Vorname,
                        Geburtsdatum = personDto.Geburtsdatum,
                        Id = personDto.Id,
                        Created = personDto.Created,
                        CreatedBy = personDto.CreatedBy,
                        Modified = personDto.Modified,
                        ModifiedBy = personDto.ModifiedBy
                    };
                }

                Adresse.Personen.Add(person);
            }

            return Adresse;
        }
    }
}
