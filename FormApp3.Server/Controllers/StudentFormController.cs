using FormApp3.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormApp3.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFormsController : ControllerBase
    {
        private readonly StudentFormDbContext _context;

        public StudentFormsController(StudentFormDbContext context)
        {
            _context = context;
        }

        // GET: StudentForms
        [HttpGet]
        public async Task<IEnumerable<StudentForm>> GetStudentForms()
        {
            var studF = await _context.StudentsForms.ToListAsync();
            return studF;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentForm>> GetStudentFormById(int id)
        {
            var studF = await _context.StudentsForms.FindAsync(id);
            if (studF == null) return NotFound();
            return studF;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentForm(int id, StudentForm s)
        {
            if (id != s.Id)
                return BadRequest();
            var studF = await _context.StudentsForms.FindAsync(id);
            if (studF == null)
                return NotFound();
            return await _context.SaveChangesAsync() > 0 ? Ok(new { message = "Update success!" }) : BadRequest(new { message = "Update failed!" });
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<StudentForm>> PostStudentForm(StudentForm s)
        {
            _context.StudentsForms.Add(s);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Post success!" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentForm>> DeleteElev(int id)
        {
            var studF = await _context.StudentsForms.FindAsync(id);
            if (studF == null) return NotFound();

            _context.StudentsForms.Remove(studF);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
