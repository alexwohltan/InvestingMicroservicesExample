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
    public abstract class CRUDController<T> : ControllerBase
        where T : class, IIdentifier
    {
        internal readonly FundamentalDataContext _context;
        internal readonly DbSet<T> _table;

        public CRUDController(FundamentalDataContext context, DbSet<T> table)
        {
            _context = context;
            _table = table;
        }

        // GET: api/<controller>
        [HttpGet]
        public virtual ActionResult<IEnumerable<T>> Get()
        {
            return _table;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetByID(int id)
        {
            return await _table.FindAsync(id);
        }

        // POST api/<controller>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult> Post([FromBody] T newItem)
        {
            await _context.AddAsync(newItem);
            await _context.SaveChangesAsync();

            var result = await _table.FindAsync(newItem.ID);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            NewValueEvent(result);

            return CreatedAtAction(nameof(GetByID), new { id = result.ID }, result);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] T newVal)
        {
            if (id != newVal.ID)
                return BadRequest();

            var oldVal = await _table.FindAsync(id);

            if (oldVal == null)
                return NotFound();

            var oldValForUpdate = (T)_context.Entry(oldVal).CurrentValues.ToObject();

            _context.Entry(oldVal).CurrentValues.SetValues(newVal);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_table.Find(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var result = await _table.FindAsync(id);

            UpdateValueEvent(result, oldValForUpdate);

            return AcceptedAtAction(nameof(GetByID), new { id = result.ID }, result);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _table.FindAsync(id);

            if (item == null)
                return NotFound();

            _table.Remove(item);
            await _context.SaveChangesAsync();

            DeleteValueEvent(item);

            return Ok();
        }

        [NonAction]
        public virtual void NewValueEvent(T newValue)
        {

        }
        [NonAction]
        public virtual void UpdateValueEvent(T newValue, T oldValue)
        {

        }
        [NonAction]
        public virtual void DeleteValueEvent(T oldValue)
        {

        }
    }
}
