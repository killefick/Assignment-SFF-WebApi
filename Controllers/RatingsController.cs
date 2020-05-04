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
    public class RatingsController : ControllerBase
    {
        private readonly IAsyncRepository<Rating> _repository;
        private readonly IMapper _mapper;

        public RatingsController(IAsyncRepository<Rating> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatings()
        {
            var ratings = await _repository.GetAll();
            var result = _mapper.Map<List<RatingDto>>(ratings);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDto>> GetRating(int id)
        {
            var rating = await _repository.GetById(id);

            if (rating == null)
            {
                return NotFound();
            }

            return _mapper.Map<RatingDto>(rating);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, RatingDto ratingDto)
        {
            if (id != ratingDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var rating = _mapper.Map<Rating>(ratingDto);
                await _repository.Update(rating);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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
        public async Task<ActionResult<RatingDto>> PostRating(RatingDto ratingDto)
        {
            var rating = _mapper.Map<Rating>(ratingDto);

            rating.MovieId = ratingDto.MovieId;
            rating.StudioId = ratingDto.StudioId;
            
            rating = await _repository.Add(rating);
            var result = _mapper.Map<RatingDto>(rating);

            return CreatedAtAction("GetRating", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RatingDto>> DeleteRating(int id)
        {
            var rating = await _repository.GetById(id);

            if (rating == null)
            {
                return NotFound();
            }

            await _repository.Remove(rating);
            var result = _mapper.Map<RatingDto>(rating);
            return result;
        }

        private bool RatingExists(int id)
        {
            if (_repository.GetById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}