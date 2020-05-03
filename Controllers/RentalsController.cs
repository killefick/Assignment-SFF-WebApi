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
    public class RentalsController : ControllerBase
    {
        private readonly IAsyncRepository<Rental> _repository;
        private readonly IMapper _mapper;

        public RentalsController(IAsyncRepository<Rental> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentals()
        {
            var Rentals = await _repository.GetAll();
            var result = _mapper.Map<List<RentalDto>>(Rentals);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetRental(int id)
        {
            var Rental = await _repository.GetById(id);

            if (Rental == null)
            {
                return NotFound();
            }

            return _mapper.Map<RentalDto>(Rental);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, RentalDto RentalDto)
        {
            if (id != RentalDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var Rental = _mapper.Map<Rental>(RentalDto);
                await _repository.Update(Rental);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
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
        public async Task<ActionResult<RentalDto>> PostRental(RentalDto RentalDto)
        {
            var Rental = _mapper.Map<Rental>(RentalDto);
            Rental = await _repository.Add(Rental);
            var result = _mapper.Map<RentalDto>(Rental);

            return CreatedAtAction("GetRental", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RentalDto>> DeleteRental(int id)
        {
            var Rental = await _repository.GetById(id);

            if (Rental == null)
            {
                return NotFound();
            }

            await _repository.Remove(Rental);
            var result = _mapper.Map<RentalDto>(Rental);
            return result;
        }

        private bool RentalExists(int id)
        {
            if (_repository.GetById(id) != null)
            {
                return true;
            }
            return false;
        }
    }
}