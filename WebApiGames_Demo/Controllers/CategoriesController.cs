using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiGames_Demo.DTOs;
using WebApiGames_Demo.Models;
using WebApiGames_Demo.Pagination;
using WebApiGames_Demo.Repository;
using WebApiGames_Demo.Services;

namespace WebApiGames_Demo.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors("AllowApiRequest")]
        public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public CategoriesController(IUnitOfWork context, IConfiguration config,
            ILogger<CategoriesController> logger, IMapper mapper)
        {
            _context = context;
            _configuration = config;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("author")]
        public string GetAuthor()
        {
            var author = _configuration["author"];
            var connection = _configuration["ConnectionStrings:DefaultConnection"];
            return $"Author: {author} Connection: {connection}";
        }

        [HttpGet("hello/{name}")]
        public ActionResult<string> GetWelcome([FromServices] IMyService myservice, string name)
        {
            return myservice.Hello(name);
        }

        [HttpGet("games")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetGamesCategories()
        {
            _logger.LogInformation("============ GET api/categories/games ============");

            var categories = await _context.CategoryRepository.GetCategoriesGames();
            var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDTO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get([FromQuery] CategoriesParameters categoriesParameters)
        {
            _logger.LogInformation("============ GET api/categories ============");
            try
            {
                var categories = await _context.CategoryRepository.GetCategories(categoriesParameters);

                var metadata = new
                {
                    categories.TotalCount,
                    categories.PageSize,
                    categories.CurrentPage,
                    categories.TotalPages,
                    categories.HasNext,
                    categories.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var categoriesDTO = _mapper.Map<List<CategoryDTO>>(categories);
                return categoriesDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while trying to get categories from the database");
            }
        }

        /// <summary>
        /// Obtain one category by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Objects Category</returns>

        [HttpGet("{id}", Name = "ObtainCategory")]
        //[EnableCors("AllowApiRequest")]
        [ProducesResponseType(typeof(GameDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            _logger.LogInformation($"============ GET api/categories/id = {id} ============");
            try
            {
                var category = await _context.CategoryRepository.GetById(c => c.CategoryId == id);
                if (category != null)
                {
                    var categoryDTO = _mapper.Map<CategoryDTO>(category);
                    return categoryDTO;
                }
                else
                {
                    _logger.LogInformation($"============ GET api/categories/id = {id} NOT FOUND ============");
                    return NotFound($"The category with id={id} was not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error while trying to get category from the database");
            }

        }

        /// <summary>
        /// Includes a new category
        /// </summary>
        /// <remarks>
        /// Request example:
        /// 
        /// POST api/categories
        /// {
        /// "categoryid": 1
        /// "name": category1
        /// "urlimage": "https://test.net/1.jpg"
        /// }
        /// </remarks>
        /// <param name="categoryDto">object Category</param>
        /// <returns>The included category object</returns>
        /// <remarks>Returns an included category object</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                _context.CategoryRepository.Add(category);
                await _context.Commit();
                var categoryDTO = _mapper.Map<CategoryDTO>(category);
                return new CreatedAtRouteResult("", new { id = category.CategoryId }, categoryDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error while trying to create new category");
            }

        }

        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            try
            {
                if (id != categoryDto.CategoryId)
                {
                    return BadRequest($"It was not possible to update the category with id={id}");
                }
                var category = _mapper.Map<Category>(categoryDto);
                _context.CategoryRepository.Update(category);
                await _context.Commit();
                return Ok($"The category with id={id} was successfully updated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 $"Error while trying to update the category with id={id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            try
            {
                var category = await _context.CategoryRepository.GetById(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound($"Category with id={id} not found");
                }

                _context.CategoryRepository.Delete(category);
                await _context.Commit();
                var categoryDto = _mapper.Map<CategoryDTO>(category);
                return categoryDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               $"Error while trying to delete the category with id={id}");
            }

        }

    }
}
