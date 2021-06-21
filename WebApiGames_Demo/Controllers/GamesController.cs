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


        //Distinct endpoints for the same action method
        //[HttpGet("first")]
        //[HttpGet("/first")]
        [HttpGet("{value:alpha:length(5)}")] //restriction, alphanumeric value, length == 5
        public ActionResult<Game> Get()
        {
            return _context.Games.FirstOrDefault();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Game>> Get2()
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
        //'param2?' == optional parameter
        //'param2=xbox' == default value
        [HttpGet("{id}/{param2=xbox}", Name = "ObtainGame")]
        public ActionResult<Game> Get3(int id, string param2)
        {
            var myParameter = param2;
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

        //restriction for route == id:int:min(10}
        //prevent unnecessary database querys
        [HttpGet("{id:int:min(1)}", Name = "ObtainGame2")]
        public ActionResult<Game> Get4(int id)
        {
            var game = _context.Games.AsNoTracking().FirstOrDefault(g => g.GameId == id);

            if (game == null)
            {
                return NotFound();
            } else
            {
                return game;
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
