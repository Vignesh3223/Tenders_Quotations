using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tenders_Quotations.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace Tenders_Quotations.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public async Task<ActionResult<TendersTaken>> PostTendersTaken([FromBody]TendersTaken tendersTaken)
        {
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

        [HttpPost("SendMail")]
        public ActionResult SendMail([FromBody] EmailResponse emailresponse)
        {
            if (ModelState.IsValid)
            {
                if (emailresponse == null)
                {
                    return BadRequest("Invalid request");
                }
                try
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("VIGNESH", "20bsca150vigneshr@skacas.ac.in"));
                    message.To.Add(MailboxAddress.Parse(emailresponse.Email));
                    message.Subject = "TENDER CONFIRMATION";
                    var text = new TextPart("plain")
                    {
                        Text = $@"Congratulations {emailresponse.CompanyName} , the tender {emailresponse.TenderName} has been granted to you..
                        Tender Details:
                        Tender Name : {emailresponse.TenderName}
                        Location : {emailresponse.Location}
                        Authority : {emailresponse.Authority}
                        Project Value : {emailresponse.ProjectValue}
                        Project Start Date : {emailresponse.ProjectStartDate}
                        Project End Date : {emailresponse.ProjectEndDate}
                        We expect a great determination and hard work towards the project , All the Best...",
                    };
                    var multipart = new Multipart("mixed")
                    {
                        text
                    };
                    message.Body = multipart;
                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("20bsca150vigneshr@skacas.ac.in", "welcome123");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                    return new JsonResult(new { message = "Email sent Successfully" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Content("Send Email method executed");
        }
    }
}
