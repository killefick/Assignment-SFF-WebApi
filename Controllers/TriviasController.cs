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
            var trivias = await _repository.GetAll();
            var result = _mapper.Map<List<TriviaDto>>(trivias);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TriviaDto>> GetTrivia(int id)
        {
            var trivia = await _repository.GetById(id);

            if (trivia == null)
            {
                return NotFound();
            }

            return _mapper.Map<TriviaDto>(trivia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrivia(int id, TriviaDto triviaDto)
        {
            if (id != triviaDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var trivia = _mapper.Map<Trivia>(triviaDto);
                await _repository.Update(trivia);
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
        public async Task<ActionResult<TriviaDto>> PostTrivia(TriviaDto triviaDto)
        {
            var trivia = _mapper.Map<Trivia>(triviaDto);
            trivia = await _repository.Add(trivia);
            var result = _mapper.Map<TriviaDto>(trivia);

            return CreatedAtAction("GetTrivia", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TriviaDto>> DeleteTrivia(int id)
        {
            var trivia = await _repository.GetById(id);

            if (trivia == null)
            {
                return NotFound();
            }

            await _repository.Remove(trivia);
            var result = _mapper.Map<TriviaDto>(trivia);
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