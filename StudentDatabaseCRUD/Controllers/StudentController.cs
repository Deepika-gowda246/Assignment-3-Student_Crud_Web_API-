using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDatabaseCRUD.Models;

namespace StudentDatabaseCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentsDatabaseContext _dbContext;

        public StudentController(StudentsDatabaseContext context)
        {
            _dbContext = context;
        }

        //GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _dbContext.Students.ToListAsync();
        }

        //GET: api/Students/1 (StudentId)
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentByID(int id)
        {

            var student = await _dbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        //POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }


        

        //PUT: api/Students/2
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            _dbContext.Entry(student).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (IsStudentExist(id))
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

        //DELETE: api/Students/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();

            return student;
        }

        private bool IsStudentExist(int id)
        {
            return _dbContext.Students.Any(e => e.StudentId == id);
        }

    }
}

