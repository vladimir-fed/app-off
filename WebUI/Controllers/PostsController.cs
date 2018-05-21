using System;
using DAO.DTO;
using Microsoft.AspNetCore.Mvc;
using DAO.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postsService.GetAll());
        }

        [HttpGet]
        public IActionResult GetById(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var post = _postsService.GetById(id.Value);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet]
        public IActionResult GetByUser(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            return Ok(_postsService.GetAllByUserId(id.Value));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] PostDto post)
        {
            try
            {
                _postsService.Create(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            return Ok(post);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] PostDto post)
        {
            try
            {
                _postsService.Update(post);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            return Ok(post);
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                _postsService.Delete(id.Value);
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