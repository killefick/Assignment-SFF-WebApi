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
            var studios = await _repository.GetAll();
            var result = _mapper.Map<List<StudioDto>>(studios);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudioDto>> GetStudio(int id)
        {
            var studio = await _repository.GetById(id);

            if (studio == null)
            {
                return NotFound();
            }

            return _mapper.Map<StudioDto>(studio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudio(int id, StudioDto studioDto)
        {
            if (id != studioDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var studio = _mapper.Map<Studio>(studioDto);
                await _repository.Update(studio);
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
        public async Task<ActionResult<StudioDto>> PostStudio(StudioDto studioDto)
        {
            var studio = _mapper.Map<Studio>(studioDto);
            studio = await _repository.Add(studio);
            var result = _mapper.Map<StudioDto>(studio);

            return CreatedAtAction("GetStudio", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StudioDto>> DeleteStudio(int id)
        {
            var studio = await _repository.GetById(id);

            if (studio == null)
            {
                return NotFound();
            }

            await _repository.Remove(studio);
            var result = _mapper.Map<StudioDto>(studio);
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