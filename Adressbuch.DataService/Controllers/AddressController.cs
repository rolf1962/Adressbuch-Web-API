using Adressbuch.DataTransfer;
using Adressbuch.Server.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adressbuch.Server.DataService.Controllers
{
    //AddressController
    /// <summary>
    /// ASP.Net Controller to handle requests for <see cref="PersonDto"/>-Objects
    /// </summary>
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IAdresseRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">The repository for receiving and sending <see cref="AdresseDto"/>-Data</param>
        public AddressController(IAdresseRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Default Request, returns a list with all avaible <see cref="AdresseDto"/>-Objects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetAllAsync());
        }

        /// <summary>
        /// Request for a single <see cref="AdresseDto"/>-Object
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> of the requested <see cref="AdresseDto"/>-Object</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repository.GetByIdAsync(id));
        }

        ///// <summary>
        ///// Request for <see cref="PersonDto"/>-Objects, with the submitted searchcriterias
        ///// </summary>
        ///// <param name="searchCriteria">A <see cref="PersonSearchDto"/>-Object with searchcriterias.</param>
        ///// <returns></returns>
        //[HttpPost("search")]
        //public async Task<IActionResult> PostAndGetBySearchCriteria([FromBody]PersonSearchDto searchCriteria)
        //{
        //    return Ok(await _repository.GetByCriteriaAsync(searchCriteria));
        //}

        /// <summary>
        /// Delete-Request for removing a specified <see cref="PersonDto"/>-Object (from the database)
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> of the <see cref="AdresseDto"/> to delete.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAdresseAsync(id);
            return Ok();
        }
    }
}
