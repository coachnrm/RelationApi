using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RelationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManytoManyController(AppDbContext context) : ControllerBase
    {
        [HttpPost("add-student")]
        public async Task<IActionResult> CreateBlog(Student student)
        {
            context.Students.Add(student);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-students")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await context.Students.Include(x => x.CourseStudents).ToListAsync());
        }

        [HttpPost("add-course")]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            context.Courses.Add(course);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-courses")]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await context.Courses.Include(x => x.CourseStudents).ToListAsync());
        }

        [HttpPost("add-course-student")]
        public async Task<IActionResult> CreateStudentCourse(CourseStudent studentCourse)
        {
            context.CoursesStudents.Add(studentCourse);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-courses-students")]
        public async Task<IActionResult> GetStudentsCourses()
        {
            return Ok(await context.CoursesStudents
                .Include(x => x.Course)
                .Include(x => x.Student).ToListAsync());
        }
    }

    public class Student 
    {
        public int Id {get; set;}
        public string? Title {get; set;}
        public ICollection<CourseStudent>? CourseStudents {get; set;}
    }

    public class Course
    {
        public int Id {get; set;}
        public string? Title {get; set;}
        public ICollection<CourseStudent>? CourseStudents {get; set;}
    }

    public class CourseStudent 
    {
        public int Id {get; set;}
        public int StudentId {get; set;}
        public int CourseId {get; set;}
        public Student? Student {get; set;}
        public Course? Course {get; set;}
    }

    public partial class AppDbContext: DbContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseStudent> CoursesStudents => Set<CourseStudent>();
    }
}