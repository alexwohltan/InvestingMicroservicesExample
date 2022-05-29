using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataStructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundamentalData
{
    [Route("api/Sectors")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly FundamentalDataContext _repository;
        private readonly IEventBus _eventBus;

        public SectorsController(FundamentalDataContext context, IEventBus eventBus)
        {
            _repository = context;
            _eventBus = eventBus;
        }


        // GET api/<controller>/5
        [HttpGet("ids", Name = "GetSectorById")]
        public virtual async Task<ActionResult<Sector>> GetByID(int id)
        {
            return (await _repository.GetSector(id)).WithoutCompanies();
        }
        // GET api/<controller>/"Industrials"
        [HttpGet("names/", Name = "GetSectorByNameAndMarketId")]
        public virtual async Task<ActionResult<Sector>> GetByName(string name, int marketId)
        {
            if (name == null)
                name = "";
            var sector = (await _repository.GetSector(marketId, name));
            return sector.WithoutCompanies();
        }
        // GET api/<controller>/"Industrials"
        [HttpGet("marketNames/", Name = "GetSectorByNameAndMarketName")]
        public virtual async Task<ActionResult<Sector>> GetByMarketName(string marketName, string sectorName)
        {
            if (sectorName == null)
                sectorName = "";
            var sector = (await _repository.GetSector(marketName, sectorName));
            return sector.WithoutCompanies();
        }

        // GET: api/<controller>/5/Industries
        [HttpGet("{id}/Industries", Name = "GetIndustriesBySectorId")]
        public async virtual Task<IEnumerable<Industry>> Get(int sectorId)
        {
            return (await _repository.GetIndustries(sectorId)).Select(e => e.WithoutCompanies());
        }

        // POST api/<controller>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Post([FromBody] Sector newSector, int marketId)
        {
            var result = await _repository.AddSector(newSector, marketId);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Post), new { id = result.ID }, result);
        }
        // POST api/<controller>
        [HttpPost("names/")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Post([FromBody] Sector newSector, string marketName)
        {
            var result = await _repository.AddSector(newSector, marketName);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Post), new { id = result.ID }, result);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Sector newSectorValues)
        {
            var result = await _repository.UpdateSector(id, newSectorValues);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return AcceptedAtAction(nameof(Put), new { id = result.ID }, result);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteSector(id);

            return Ok();
        }
    }
}