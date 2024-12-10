using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RelationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnetoManyController(AppDbContext context) : ControllerBase
    {
        [HttpPost("add-blog")]
        public async Task<IActionResult> CreateBlog(Blog blog)
        {
            context.Blogs.Add(blog);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-blogs")]
        public async Task<IActionResult> GetBlog()
        {
            return Ok(await context.Blogs.Include(x => x.Posts).ToListAsync());
        }

        [HttpPost("add-post")]
        public async Task<IActionResult> CreatePost(Post post)
        {
            context.Posts.Add(post);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-posts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await context.Posts.Include(x => x.Blog).ToListAsync());
        }
    }

    public class Blog 
    {
        public int Id {get; set; }
        public string? Title {get; set; }
        public ICollection<Post>? Posts {get; set; }
    }

    public class Post
    {
        public int Id {get; set; }
        public string? Content {get; set;}
        public Blog? Blog {get; set;}
        public int BlogId {get; set; }
    }

    public partial class AppDbContext : DbContext
    {
        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<Post> Posts => Set<Post>(); 
    }
}