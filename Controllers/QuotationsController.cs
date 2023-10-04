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
    public class QuotationsController : ControllerBase
    {
        private readonly TenderQuotationsContext _context;

        public QuotationsController(TenderQuotationsContext context)
        {
            _context = context;
        }

        // GET: api/Quotations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quotation>>> GetQuotations()
        {
            if (_context.Quotations == null)
            {
                return NotFound();
            }
            return await _context.Quotations.ToListAsync();
        }

        // GET: api/Quotations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quotation>> GetQuotation(int id)
        {
            if (_context.Quotations == null)
            {
                return NotFound();
            }
            var quotation = await _context.Quotations.FindAsync(id);

            if (quotation == null)
            {
                return NotFound();
            }

            return quotation;
        }

        // PUT: api/Quotations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuotation(int id, Quotation quotation)
        {
            if (id != quotation.QuotationId)
            {
                return BadRequest();
            }

            _context.Entry(quotation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuotationExists(id))
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

        // POST: api/Quotations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quotation>> PostQuotation([FromBody] Quotation quotation)
        {
            quotation.QuotedDate = DateTime.Now;
            _context.Quotations.Add(quotation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuotation", new { id = quotation.QuotationId }, quotation);
        }

        // DELETE: api/Quotations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotation(int id)
        {
            if (_context.Quotations == null)
            {
                return NotFound();
            }
            var quotation = await _context.Quotations.FindAsync(id);
            if (quotation == null)
            {
                return NotFound();
            }

            _context.Quotations.Remove(quotation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuotationExists(int id)
        {
            return (_context.Quotations?.Any(e => e.QuotationId == id)).GetValueOrDefault();
        }
    }
}
