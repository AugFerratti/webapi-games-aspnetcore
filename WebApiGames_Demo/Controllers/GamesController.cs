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
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult<IEnumerable<Game>> Get()
        {
            try
            {
                return _context.Games.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error while trying to get games from the database");
            }
            
        }

        [HttpGet("{id}", Name = "ObtainGame")]
        public ActionResult<Game> Get(int id)
        {
            var game = _context.Games.AsNoTracking().FirstOrDefault(g => g.GameId == id);
            if (game != null)
            {
                return game;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Game game)
        {
            // if(!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }
            _context.Games.Add(game);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObtainGame", new { id = game.GameId }, game);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Game game)
        {
            // if(!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            if (id != game.GameId)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Game> Delete(int id)
        {
            var game = _context.Games.FirstOrDefault(g => g.GameId == id);

            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            _context.SaveChanges();
            return game;
        }

    }
}
