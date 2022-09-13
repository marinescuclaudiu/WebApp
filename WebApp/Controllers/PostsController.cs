using Core.Contracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<UsersController> _logger;

        public PostsController(IPostService postService, ILogger<UsersController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts([Range(1, int.MaxValue)] int offset = 1, [Range(1, 10)] int limit = 2)
        {
            return Ok(_postService.GetAllPosts(offset, limit));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Post>> CreatePost([Required] string userName, [Required] string text)
        {

            var createdPost = await _postService.AddPost(userName, text);

            return Ok(createdPost);
        }


        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<Post>> UpdatePost([Required] int postId, [Required] string text)
        {
            var existingPost = await _postService.GetPostById(postId);

            if (existingPost == null)
            {
                return NotFound();
            }

            var updatedPost = await _postService.UpdatePost(postId, text);
            return Ok(updatedPost);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<Post>> DeletePost([Required]int postId)
        {
            var existingUser = await _postService.GetPostById(postId);

            if (existingUser == null)
            {
                return NotFound();
            }

            await _postService.DeletePost(postId);

            return NoContent();
        }
    }
}
