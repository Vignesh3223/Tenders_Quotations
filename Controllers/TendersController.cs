using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tenders_Quotations.Models;


namespace Tenders_Quotations.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TendersController : ControllerBase
    {
        private readonly TenderQuotationsContext _context;

        public TendersController(TenderQuotationsContext context)
        {
            _context = context;
        }

        // GET: api/Tenders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tender>>> GetTenders()
        {
          if (_context.Tenders == null)
          {
              return NotFound();
          }
            return await _context.Tenders.ToListAsync();
        }

        // GET: api/Tenders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tender>> GetTender(int id)
        {
          if (_context.Tenders == null)
          {
              return NotFound();
          }
            var tender = await _context.Tenders.FindAsync(id);

            if (tender == null)
            {
                return NotFound();
            }

            return tender;
        }

        // PUT: api/Tenders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTender(int id, Tender tender)
        {
            if (id != tender.TenderId)
            {
                return BadRequest();
            }

            _context.Entry(tender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TenderExists(id))
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

        // POST: api/Tenders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tender>> PostTender(Tender tender)
        {
            int? categoryId = (int?) tender.CategoryId;
            tender.TenderOpeningDate = DateTime.Now;
            tender.TenderClosingDate = DateTime.Now.AddDays(30);
            _context.Tenders.Add(tender);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTender", new { id = tender.TenderId }, tender);
        }

        // DELETE: api/Tenders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTender(int id)
        {
            if (_context.Tenders == null)
            {
                return NotFound();
            }
            var tender = await _context.Tenders.FindAsync(id);
            if (tender == null)
            {
                return NotFound();
            }

            _context.Tenders.Remove(tender);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TenderExists(int id)
        {
            return (_context.Tenders?.Any(e => e.TenderId == id)).GetValueOrDefault();
        }
    }
}
