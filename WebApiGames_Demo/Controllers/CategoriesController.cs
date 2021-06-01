using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGames_Demo.Context;
using WebApiGames_Demo.Models;

namespace WebApiGames_Demo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("games")]
        public ActionResult<IEnumerable<Category>> GetGamesCategories()
        {
            return _context.Categories.Include(x => x.Games).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                return _context.Categories.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while trying to get categories from the database");
            }
        }

        [HttpGet("{id}", Name = "ObtainCategory")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.CategoryId == id);
                if (category != null)
                {
                    return category;
                }
                else
                {
                    return NotFound($"The category with id={id} was not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error while trying to get category from the database");
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody] Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObtainGame", new { id = category.CategoryId }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error while trying to create new category");
            }

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest($"It was not possible to update the category with id={id}");
                }

                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok($"The category with id={id} was successfully updated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"Error while trying to update the category with id={id}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound($"Category with id={id} not found");
                }

                _context.Categories.Remove(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               $"Error while trying to delete the category with id={id}");
            }
           
        }

    }
}
