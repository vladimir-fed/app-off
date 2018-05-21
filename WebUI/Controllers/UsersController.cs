using System;
using DAO.DTO;
using Microsoft.AspNetCore.Mvc;
using DAO.Models;
using DAO.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usersService.GetAll());
        }

        [HttpGet]
        public IActionResult GetById(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var user = _usersService.GetById(id.Value);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpDto user)
        {
            var token = _usersService.Create(user);
            if (token != null)
            {
                return Ok(token);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult SignIn([FromBody] LoginDTO user)
        {
            var token = _usersService.Login(user);
            if (token != null)
            {
                return Ok(token);
            }

            return BadRequest();
        }

        [Authorize(Roles = "Write")]
        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                _usersService.Update(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            return Ok(user);
        }

        [Authorize(Roles = "Write")]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                _usersService.Delete(id.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            return Ok();
        }
    }
}
