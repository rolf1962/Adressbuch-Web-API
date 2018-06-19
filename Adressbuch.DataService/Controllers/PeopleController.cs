using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Adressbuch.Server.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace Adressbuch.DataService.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly AdressbuchDbContext _context;

        public PeopleController(AdressbuchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // JsonSerializerException: "self referencing loop detected with type"
            //var dbResult = await _context.Personen.Include(nameof(Person.Adressen)).ToListAsync();
            var dbResult = await _context.Personen.ToListAsync();
            return Ok(dbResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // JsonSerializerException: "self referencing loop detected with type"
            //var dbResult = _context.Personen.Include(nameof(Person.Adressen)).SingleOrDefaultAsync(p => p.Id == id);
            var dbResult = _context.Personen.SingleOrDefaultAsync(p => p.Id == id);
            return Ok(await dbResult);
        }
    }
}