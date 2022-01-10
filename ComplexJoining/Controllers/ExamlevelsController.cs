using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComplexJoining.Models;

namespace ComplexJoining
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamlevelsController : ControllerBase
    {
        private readonly ModelContext _context;

        public ExamlevelsController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Examlevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examlevel>>> GetExamlevels()
        {
            return await _context.Examlevels.ToListAsync();
        }

        // GET: api/Examlevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Examlevel>> GetExamlevel(decimal? id)
        {
            var examlevel = await _context.Examlevels.FindAsync(id);

            if (examlevel == null)
            {
                return NotFound();
            }

            return examlevel;
        }

        // PUT: api/Examlevels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamlevel(decimal? id, Examlevel examlevel)
        {
            if (id != examlevel.Exid)
            {
                return BadRequest();
            }

            _context.Entry(examlevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamlevelExists(id))
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

        // POST: api/Examlevels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Examlevel>> PostExamlevel(Examlevel examlevel)
        {
            _context.Examlevels.Add(examlevel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExamlevelExists(examlevel.Exid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExamlevel", new { id = examlevel.Exid }, examlevel);
        }

        // DELETE: api/Examlevels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamlevel(decimal? id)
        {
            var examlevel = await _context.Examlevels.FindAsync(id);
            if (examlevel == null)
            {
                return NotFound();
            }

            _context.Examlevels.Remove(examlevel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExamlevelExists(decimal? id)
        {
            return _context.Examlevels.Any(e => e.Exid == id);
        }
    }
}
