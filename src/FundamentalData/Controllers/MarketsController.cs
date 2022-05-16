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
    [Route("api/Markets")]
    [ApiController]
    public class MarketsController : ControllerBase
    {
        private readonly FundamentalDataContext _repository;
        private readonly IEventBus _eventBus;

        public MarketsController(FundamentalDataContext context, IEventBus eventBus)
        {
            _repository = context;
            _eventBus = eventBus;
        }

        // GET: api/<controller>
        [HttpGet]
        public virtual IEnumerable<Market> Get()
        {
            return _repository.GetMarkets().ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}", Name = "GetMarketById")]
        public virtual async Task<ActionResult<Market>> GetByID(int id)
        {
            var result = (await _repository.GetMarket(id)).WithoutCompanies();
            return Ok(result);
        }
        // GET api/<controller>/"US"
        [HttpGet("names/{name}", Name = "GetMarketByName")]
        public virtual async Task<ActionResult<Market>> GetByName(string name)
        {
            return (await _repository.GetMarket(name)).WithoutCompanies();
        }

        // GET: api/<controller>/Sectors
        [HttpGet("{marketId}/Sectors", Name = "GetSectorsByMarketId")]
        public async virtual Task<IEnumerable<Sector>> Get(int marketId)
        {
            return (await _repository.GetSectors(marketId)).Select(e => e.WithoutCompanies());

            
        }

        // POST api/<controller>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Post([FromBody] Market newMarket)
        {
            var result = await _repository.AddMarket(newMarket);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(Post), new { id = result.ID }, result);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] Market newMarketValues)
        {
            var result = await _repository.UpdateMarket(id, newMarketValues);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return AcceptedAtAction(nameof(Put), new { id = result.ID }, result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteMarket(id);

            return Ok();
        }
    }
}