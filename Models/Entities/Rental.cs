using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFF.Context;

namespace SFF.Models
{
    public class Rental : BaseEntity
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int StudioId { get; set; }
        public Studio Studio { get; set; }

        public async Task<Rental> RentMovie(myDbContext _context)
        {
            // kolla aktuella uthyrningar
            var currentRentals = await _context.Rentals
            .Where(x => x.MovieId == MovieId)
            .Where(x => x.StudioId == StudioId).FirstOrDefaultAsync();

            // är filmen rendan hyrd av aktuell studio?
            if (currentRentals != null)
            {
                // går bara att hyra 1 gång!
                return null;
            }

            // annars titta om filmen finns i lager
            var movie = await _context.Movies
            .Where(movieToRent => movieToRent.Id == this.MovieId).FirstOrDefaultAsync();

            // returnera null om det inte finns lediga filmer annars returnera filmen
            return movie.IsAvailable() ? this : null;
        }
    }
}