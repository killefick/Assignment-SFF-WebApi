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
    public class StudiosController : ControllerBase
    {
        private readonly IAsyncRepository<Studio> _repository;
        private readonly IMapper _mapper;

        public StudiosController(IAsyncRepository<Studio> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudioDto>>> GetStudios()
        {
            var Studios = await _repository.GetAll();
            var result = _mapper.Map<List<StudioDto>>(Studios);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudioDto>> GetStudio(int id)
        {
            var Studio = await _repository.GetById(id);

            if (Studio == null)
            {
                return NotFound();
            }

            return _mapper.Map<StudioDto>(Studio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudio(int id, StudioDto StudioDto)
        {
            if (id != StudioDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var Studio = _mapper.Map<Studio>(StudioDto);
                await _repository.Update(Studio);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudioExists(id))
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
        public async Task<ActionResult<StudioDto>> PostStudio(StudioDto StudioDto)
        {
            var Studio = _mapper.Map<Studio>(StudioDto);
            Studio = await _repository.Add(Studio);
            var result = _mapper.Map<StudioDto>(Studio);

            return CreatedAtAction("GetStudio", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StudioDto>> DeleteStudio(int id)
        {
            var Studio = await _repository.GetById(id);

            if (Studio == null)
            {
                return NotFound();
            }

            await _repository.Remove(Studio);
            var result = _mapper.Map<StudioDto>(Studio);
            return result;
        }

        private bool StudioExists(int id)
        {
            if (_repository.GetById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}