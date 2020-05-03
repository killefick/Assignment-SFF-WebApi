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
    public class TriviasController : ControllerBase
    {
        private readonly IAsyncRepository<Trivia> _repository;
        private readonly IMapper _mapper;

        public TriviasController(IAsyncRepository<Trivia> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TriviaDto>>> GetTrivias()
        {
            var Trivias = await _repository.GetAll();
            var result = _mapper.Map<List<TriviaDto>>(Trivias);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TriviaDto>> GetTrivia(int id)
        {
            var Trivia = await _repository.GetById(id);

            if (Trivia == null)
            {
                return NotFound();
            }

            return _mapper.Map<TriviaDto>(Trivia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrivia(int id, TriviaDto TriviaDto)
        {
            if (id != TriviaDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var Trivia = _mapper.Map<Trivia>(TriviaDto);
                await _repository.Update(Trivia);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriviaExists(id))
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
        public async Task<ActionResult<TriviaDto>> PostTrivia(TriviaDto TriviaDto)
        {
            var Trivia = _mapper.Map<Trivia>(TriviaDto);
            Trivia = await _repository.Add(Trivia);
            var result = _mapper.Map<TriviaDto>(Trivia);

            return CreatedAtAction("GetTrivia", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TriviaDto>> DeleteTrivia(int id)
        {
            var Trivia = await _repository.GetById(id);

            if (Trivia == null)
            {
                return NotFound();
            }

            await _repository.Remove(Trivia);
            var result = _mapper.Map<TriviaDto>(Trivia);
            return result;
        }

        private bool TriviaExists(int id)
        {
            if (_repository.GetById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}