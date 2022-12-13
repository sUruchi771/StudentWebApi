using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Configuration;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDataBaseContext _dbContext;

        public StudentController(StudentDataBaseContext context)

        {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _dbContext.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            //var student = await _Context.Students.FindAsync(s => s.StuId == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);

        }
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _dbContext.Add(student);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutStudent(int id, Student student)
        {
            if (student == null || student.StudentId == 0)
                return BadRequest();

            var Student = await _dbContext.Students.FindAsync(student.StudentId);
            if (Student == null)
                return NotFound();
            Student.Name = student.Name;
            Student.Class = student.Class;
            Student.Address = student.Address;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if (student == null) return NotFound();
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }
    }


    
}
