using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFF.Models;

namespace SFF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IAsyncRepository<Movie> _repository;
        private readonly IMapper _mapper;

        public MoviesController(IAsyncRepository<Movie> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            var movies = await _repository.GetAll();
            var result = _mapper.Map<List<MovieDto>>(movies);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            var movie = await _repository.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return _mapper.Map<MovieDto>(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDto movieDto)
        {
            if (id != movieDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var movie = _mapper.Map<Movie>(movieDto);
                await _repository.Update(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(MovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            movie = await _repository.Add(movie);
            var result = _mapper.Map<MovieDto>(movie);

            return CreatedAtAction("GetMovie", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDto>> DeleteMovie(int id)
        {
            var movie = await _repository.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            await _repository.Remove(movie);
            var result = _mapper.Map<MovieDto>(movie);
            return result;
        }

        private bool MovieExists(int id)
        {
            if (_repository.GetById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}