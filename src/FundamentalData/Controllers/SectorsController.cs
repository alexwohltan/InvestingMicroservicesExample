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
    public class SectorsController : CRUDController<Sector>
    {
        public SectorsController(FundamentalDataContext context)
            : base(context, context.Sectors)
        {

        }

        // POST api/Sectors
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public override async Task<ActionResult> Post([FromBody] Sector sector)
        {
            if (_table.Where(e => e.Name == sector.Name).Count() > 0)
                return BadRequest();

            await _context.AddAsync(sector);
            await _context.SaveChangesAsync();

            var result = _table.Where(e => e.Name == sector.Name).FirstOrDefault();

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return CreatedAtAction(nameof(GetByID), new { id = result.ID }, result);
        }
    }
}