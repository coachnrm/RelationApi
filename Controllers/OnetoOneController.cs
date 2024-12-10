using System.Runtime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RelationApi.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnetoOnecontroller(AppDbContext context) : ControllerBase
    {
        [HttpPost("add-user")]
        public async Task<IActionResult> CreateUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok(); 
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await context.Users.Include(x => x.Profile).ToListAsync());
        } 

        [HttpPost("add-profile")]
        public async Task<IActionResult> CreateProfile(Profile profile)
        {
            context.Profiles.Add(profile);
            await context. SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-profiles")]
        public async Task<IActionResult> GetProfiles()
        {
            return Ok(await context.Profiles.Include(x => x.User).ToListAsync());
        }
    }

    public class User 
    {
        public int Id {get; set;}
        public string? Username {get; set;}
        public Profile? Profile{get; set;}
    }

    public class Profile 
    {
        public int Id {get; set;}
        public string? Bio {get; set;}
        public User? User {get; set;}
        public int UserId {get; set; }
    }

    public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) 
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Profile> Profiles => Set<Profile>(); 
    }
}