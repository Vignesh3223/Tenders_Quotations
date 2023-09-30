using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tenders_Quotations.Models;

namespace Tenders_Quotations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TendersTakensController : ControllerBase
    {
        private readonly TenderQuotationsContext _context;

        public TendersTakensController(TenderQuotationsContext context)
        {
            _context = context;
        }

        // GET: api/TendersTakens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TendersTaken>>> GetTendersTakens()
        {
          if (_context.TendersTakens == null)
          {
              return NotFound();
          }
            return await _context.TendersTakens.ToListAsync();
        }

        // GET: api/TendersTakens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TendersTaken>> GetTendersTaken(int id)
        {
          if (_context.TendersTakens == null)
          {
              return NotFound();
          }
            var tendersTaken = await _context.TendersTakens.FindAsync(id);

            if (tendersTaken == null)
            {
                return NotFound();
            }

            return tendersTaken;
        }

        // PUT: api/TendersTakens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTendersTaken(int id, TendersTaken tendersTaken)
        {
            if (id != tendersTaken.TakenId)
            {
                return BadRequest();
            }

            _context.Entry(tendersTaken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TendersTakenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TendersTakens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TendersTaken>> PostTendersTaken(TendersTaken tendersTaken)
        {
          if (_context.TendersTakens == null)
          {
              return Problem("Entity set 'TenderQuotationsContext.TendersTakens'  is null.");
          }
            _context.TendersTakens.Add(tendersTaken);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTendersTaken", new { id = tendersTaken.TakenId }, tendersTaken);
        }

        // DELETE: api/TendersTakens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTendersTaken(int id)
        {
            if (_context.TendersTakens == null)
            {
                return NotFound();
            }
            var tendersTaken = await _context.TendersTakens.FindAsync(id);
            if (tendersTaken == null)
            {
                return NotFound();
            }

            _context.TendersTakens.Remove(tendersTaken);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TendersTakenExists(int id)
        {
            return (_context.TendersTakens?.Any(e => e.TakenId == id)).GetValueOrDefault();
        }
    }
}
