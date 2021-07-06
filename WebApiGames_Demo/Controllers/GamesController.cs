using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiGames_Demo.DTOs;
using WebApiGames_Demo.Filters;
using WebApiGames_Demo.Models;
using WebApiGames_Demo.Pagination;
using WebApiGames_Demo.Repository;

namespace WebApiGames_Demo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public GamesController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }

        [HttpGet("lowerscore")]
        public ActionResult<IEnumerable<GameDTO>> GetGamesScores()
        {
            var games = _uof.GameRepository.GetGameByScore().ToList();
            var gamesDTO = _mapper.Map<List<GameDTO>>(games);
            return gamesDTO;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<GameDTO>> Get([FromQuery] GamesParameters gamesParameters)
        {
            try
            {
                var games = _uof.GameRepository.GetGames(gamesParameters);

                var metadata = new
                {
                    games.TotalCount,
                    games.PageSize,
                    games.CurrentPage,
                    games.TotalPages,
                    games.HasNext,
                    games.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var gamesDTO = _mapper.Map<List<GameDTO>>(games);
                return gamesDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error while trying to get games from the database");
            }
        }


        [HttpGet("{id}")]
        public ActionResult<GameDTO> GetById(int id)
        {
            var game = _uof.GameRepository.GetById(g => g.GameId == id);
            if (game != null)
            {
                var gameDTO = _mapper.Map<GameDTO>(game);
                return gameDTO;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] GameDTO gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);
            _uof.GameRepository.Add(game);
            _uof.Commit();
            var gameDTO = _mapper.Map<GameDTO>(game);
            return new CreatedAtRouteResult("", new { id = game.GameId }, gameDTO);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] GameDTO gameDto)
        {
            if (id != gameDto.GameId)
            {
                return BadRequest();
            }
            var game = _mapper.Map<Game>(gameDto);
            _uof.GameRepository.Update(game);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<GameDTO> Delete(int id)
        {
            var game = _uof.GameRepository.GetById (g => g.GameId == id);

            if (game == null)
            {
                return NotFound();
            }

            _uof.GameRepository.Delete(game);
            _uof.Commit();

            var gameDto = _mapper.Map<GameDTO>(game);
            return gameDto;
        }

    }
}
