using System;
using DAO.Models;
using DAO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_commentsService.GetAll());
        }

        [HttpGet]
        public IActionResult GetById(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var comment = _commentsService.GetById(id.Value);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpGet]
        public IActionResult GetByPost(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            return Ok(_commentsService.GetAllByPostId(id.Value));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Comment comment)
        {
            try
            {
                _commentsService.Create(comment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            return Ok(comment);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] Comment comment)
        {
            try
            {
                _commentsService.Update(comment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            return Ok(comment);
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
                _commentsService.Delete(id.Value);
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
