using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SFF.Context;

namespace SFF.Models
{
    public class EtikettData : BaseEntity
    {
        private EtikettData()
        {
            // for EF only
        }

        public EtikettData(string filename, string location)
        {
            if (string.IsNullOrWhiteSpace(filename) || string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentException("Filename and Location are required");
            }
            this.FilmNamn = filename;
            this.Ort = location;
            this.Datum = DateTime.Now;
        }

        public string FilmNamn { get; private set; }
        public string Ort { get; private set; }
        public DateTime Datum { get; private set; }

        // foreign key property
        public int MovieId { get; set; }
        // reference property
        public Movie Movie { get; set; }

        // public async Task<ActionResult<EtikettData>> GetEtikettData(myDbContext _context, int movieId, int studioId)
        // {
        //     var movie = await _context.Movies.FindAsync(movieId);
        //     var studio = await _context.Studios.FindAsync(studioId);

        //     return this.CreateLabel(movie, studio);
        // }

        // public EtikettData CreateLabel(Movie movie, Studio studio)
        // {
        //     this.Datum = DateTime.Now;
        //     this.FilmNamn = movie.Title;
        //     this.Ort = studio.Location;

        //     return this;
        // }
    }
}