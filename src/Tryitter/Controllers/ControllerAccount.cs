using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tryitter.Models;
using Tryitter.Schemas;
using Tryitter.Services;

namespace Tryitter.Controllers
{
    [ApiController]
    [Route("account")]
    public class ControllerAccount : ControllerBase
    {
        private readonly ServiceAccount _service;

        public ControllerAccount(ServiceAccount service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterAccount(Register newUser)
        {
            try
            {
                var user = new User
                {
                    Nome = newUser.Nome,
                    Email = newUser.Email,
                    Senha = newUser.Senha,
                    Modulo = newUser.Modulo,
                    Status = newUser.Status
                };

                var student = _service.RegisterAccount(user);

                return StatusCode(201, student);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public ActionResult GetAccount(int id)
        {
            try
            {
                var result = _service.GetAccount(id);

                var student = new
                {
                    result.Id,
                    result.Nome,
                    result.Modulo,
                    result.Status,
                };

                return StatusCode(200, student);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult LoginAccount(Login user)
        {
            try
            {
                var result = _service.LoginAccount(user);
                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public ActionResult UpdateAccount(UpdateAccount user)
        {
            try
            {
                var claims = HttpContext.User.Identity as ClaimsIdentity;
                var idToken = Convert.ToInt16(claims.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                var result = _service.UpdateAccount(user, idToken);
                var student = new
                {
                    result.Id,
                    result.Nome,
                    result.Modulo,
                    result.Status,
                };

                return StatusCode(201, student);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public ActionResult DeleteAccount(int id)
        {
            try
            {
                var claims = HttpContext.User.Identity as ClaimsIdentity;

                var idToken = Convert.ToInt16(claims.Claims.FirstOrDefault(e => e.Type == "Id").Value);

                _service.DeleteAccount(id, idToken);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
