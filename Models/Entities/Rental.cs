using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFF.Context;

namespace SFF.Models 
{
    public class Rental : BaseEntity
    {
        // foreign key property
        public int MovieId { get; private set; }
        // reference property
        public Movie Movie { get; private set; }

        public int StudioId { get; private set; }
        public Studio Studio { get; private set; }

        #region public Methods
        public async Task<Rental> RentMovie(myDbContext _context)
        {
            // check if studio has rented movie already
            var currentRentals = await _context.Rentals
            .Where(x => x.MovieId == MovieId)
            .Where(x => x.StudioId == StudioId).FirstOrDefaultAsync();

            if (currentRentals != null)
            {
                // not allowed to rent same film more than once
                return null;
            }

            // check availablility
            var movie = await _context.Movies
            .Where(movieToRent => movieToRent.Id == this.MovieId).FirstOrDefaultAsync();

            // return movie if available
            return movie.IsAvailable() ? this : null;
        }
        #endregion
    }
}