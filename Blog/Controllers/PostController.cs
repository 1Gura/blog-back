using Blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        UsersContext db;
        public PostController(UsersContext context)
        {
            db = context;
            if (!db.Posts.Any())
            {
                db.Posts.Add(new Post { Title = "Пост", Text = "Текст", Author = "Илья", Date = new DateTime() });
                db.Posts.Add(new Post { Title = "Пост", Text = "Текст", Author = "Илья", Date = new DateTime() });
                db.Posts.Add(new Post { Title = "Пост", Text = "Текст", Author = "Илья", Date = new DateTime() });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            return await db.Posts.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            Post post = await db.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
                return NotFound();
            return new ObjectResult(post);
        }

        [HttpPost("createPost")]
        public ActionResult<Post> Login(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            db.Posts.Add(post);
            db.SaveChanges();
            
            return Ok(post);

        }

        [HttpPatch]
        public async Task<ActionResult<Post>> Put(Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }
            if (!db.Posts.Any(x => x.Id == post.Id))
            {
                return NotFound();
            }

            db.Update(post);
            await db.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("deletePost/{id}")]
        public ActionResult<Post> Delete(int id)
        {
            Post post = db.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();
            return Ok(post);
        }
    }
}
