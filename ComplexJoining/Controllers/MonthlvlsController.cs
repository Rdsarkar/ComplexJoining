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
    public class MonthlvlsController : ControllerBase
    {
        private readonly ModelContext _context;

        public MonthlvlsController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Monthlvls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monthlvl>>> GetMonthlvls()
        {
            return await _context.Monthlvls.ToListAsync();
        }

        // GET: api/Monthlvls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monthlvl>> GetMonthlvl(decimal? id)
        {
            var monthlvl = await _context.Monthlvls.FindAsync(id);

            if (monthlvl == null)
            {
                return NotFound();
            }

            return monthlvl;
        }

        // PUT: api/Monthlvls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonthlvl(decimal? id, Monthlvl monthlvl)
        {
            if (id != monthlvl.Mid)
            {
                return BadRequest();
            }

            _context.Entry(monthlvl).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonthlvlExists(id))
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

        // POST: api/Monthlvls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Monthlvl>> PostMonthlvl(Monthlvl monthlvl)
        {
            _context.Monthlvls.Add(monthlvl);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MonthlvlExists(monthlvl.Mid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMonthlvl", new { id = monthlvl.Mid }, monthlvl);
        }

        // DELETE: api/Monthlvls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonthlvl(decimal? id)
        {
            var monthlvl = await _context.Monthlvls.FindAsync(id);
            if (monthlvl == null)
            {
                return NotFound();
            }

            _context.Monthlvls.Remove(monthlvl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonthlvlExists(decimal? id)
        {
            return _context.Monthlvls.Any(e => e.Mid == id);
        }
    }
}
