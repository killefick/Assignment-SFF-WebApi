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
            var rentals = await _repository.GetAll();
            var result = _mapper.Map<List<RentalDto>>(rentals);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetRental(int id)
        {
            var rental = await _repository.GetById(id);

            if (rental == null)
            {
                return NotFound();
            }

            return _mapper.Map<RentalDto>(rental);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, RentalDto rentalDto)
        {
            if (id != rentalDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var rental = _mapper.Map<Rental>(rentalDto);
                await _repository.Update(rental);
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
        public async Task<ActionResult<RentalDto>> PostRental(RentalDto rentalDto)
        {
            var rental = _mapper.Map<Rental>(rentalDto);
            rental = await _repository.Add(rental);
            var result = _mapper.Map<RentalDto>(rental);

            return CreatedAtAction("GetRental", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RentalDto>> DeleteRental(int id)
        {
            var rental = await _repository.GetById(id);

            if (rental == null)
            {
                return NotFound();
            }

            await _repository.Remove(rental);
            var result = _mapper.Map<RentalDto>(rental);
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