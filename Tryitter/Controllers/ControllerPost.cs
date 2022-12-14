using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tryitter.Models;
using Tryitter.Services;
using Tryitter.Schemas;
using System.Security.Claims;

namespace Tryitter.Controllers
{
    [ApiController]
    [Route("post")]
    public class ControllerPost: ControllerBase
    {
        private readonly ServicePost _service;

        public ControllerPost(ServicePost service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddPost(NewPost newPost)
        {
            try
            {
                var claims = HttpContext.User.Identity as ClaimsIdentity;
                var id = Convert.ToInt16(claims.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                var post = new Post
                {
                    Mensagem = newPost.Mensagem,
                    Imagem = newPost.Imagem,
                    UserId = newPost.UserId,
                    Date = DateTime.Now,
                };

                var result = _service.AddPost(post, id);

                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public ActionResult DeletePost(int id)
        {
            try
            {
                var claims = HttpContext.User.Identity as ClaimsIdentity;
                var idToken = Convert.ToInt16(claims.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                _service.DeletePost(id, idToken);

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{userId}")]
        public ActionResult GetAllPosts(int userId)
        {
            try
            {
                var posts = _service.GetAllPosts(userId);

                return StatusCode(200, posts);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{userId}/last")]
        public ActionResult GetLastPost(int userId)
        {
            try
            {
                var user = _service.GetLastPost(userId);
                return StatusCode(200, user);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public ActionResult UpdatePost(UpdatePost post)
        {
            try
            {
                var claims = HttpContext.User.Identity as ClaimsIdentity;

                var id = Convert.ToInt16(claims.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                var postUpdate = new Post
                {
                    Id = post.Id,
                    UserId = post.UserId,
                    Mensagem = post.Mensagem,
                    Imagem = post.Imagem,
                    Date = DateTime.Now,
                    Editado = true
                };

                var result = _service.UpdatePost(postUpdate, id);

                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
