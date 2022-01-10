using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComplexJoining.Models;
using ComplexJoining.DTOs;

namespace ComplexJoining
{
    public class SelfClassInput 
    {
        public decimal? Sid { get; set; }

    }
    public class SelfClassOutput
    {
        public string Sname { get; set; }
        public string Crname { get; set; }
        public string Exname { get; set; }
        public string Dname { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class Student1Controller : ControllerBase
    {
        private readonly ModelContext _context;

        public Student1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Student1
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetStudent1s()
        {
            List<Student1> student1s = await _context
                                                    .Student1s
                                                    .ToListAsync();
            if (student1s.Count <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Data is not found",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Data is found",
                Success = true,
                Payload = student1s
            });
        }
        [HttpPost("ComplexJoining")]
        public async Task<ActionResult<ResponseDto>> ComplexJoining([FromBody] SelfClassInput input)
        {
            if (input.Sid == 0) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill the Sid Field",
                    Success = false,
                    Payload = null
                });
            }
            List<SelfClassOutput> selfClassOutputs =
                await (from s in _context.Student1s
                                    .Where(i => i.Sid == input.Sid)
                       from d in _context.Departments
                                    .Where(i => s.Did == i.Did)
                       from c in _context.Courses
                                    .Where(i => d.Did == i.Did)
                       from m in _context.Monthlvls
                                    .Where(i => c.Mid == i.Mid)
                       from e in _context.Examlevels
                                    .Where(i => c.Exid == i.Exid)
                       select new SelfClassOutput 
                       {
                           Sname = s.Sname,
                           Crname = c.Crname,
                           Exname = e.Exname,
                           Dname = d.Dname
                       }).ToListAsync();

            if (selfClassOutputs.Count == 0) 
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto 
                {
                    Message = "Data is not found for joining",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto 
            {
                Message = "Joining Done",
                Success = true,
                Payload = selfClassOutputs
            });
        }



        // GET: api/Student1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student1>> GetStudent1(decimal? id)
        {
            var student1 = await _context.Student1s.FindAsync(id);

            if (student1 == null)
            {
                return NotFound();
            }

            return student1;
        }

        // PUT: api/Student1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent1(decimal? id, Student1 student1)
        {
            if (id != student1.Did)
            {
                return BadRequest();
            }

            _context.Entry(student1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Student1Exists(id))
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

        // POST: api/Student1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Insert New Students")]
        public async Task<ActionResult<ResponseDto>> PostStudent1([FromBody]Student1 input)
        {
            if (input.Sid == 0) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto 
                {
                    Message = "Please Input the ID field",
                    Success = false,
                    Payload = null
                });
            }

            var student1s = await _context.Student1s.Where(i => i.Sid == input.Sid).FirstOrDefaultAsync();
            _context.Student1s.Add(input);
            if (student1s != null) 
            {
                return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
                {
                    Message = "This ID already in the database",
                    Success = false,
                    Payload = null
                });
            }

            bool isSaved = await _context.SaveChangesAsync()> 0;
            if (isSaved == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Server has problem so it cant store data",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Data Inserted",
                Success = true,
                Payload = null
            });

        }

        // DELETE: api/Student1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent1(decimal? id)
        {
            var student1 = await _context.Student1s.FindAsync(id);
            if (student1 == null)
            {
                return NotFound();
            }

            _context.Student1s.Remove(student1);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Student1Exists(decimal? id)
        {
            return _context.Student1s.Any(e => e.Did == id);
        }
    }
}
