using System;
using System.ComponentModel.DataAnnotations;

namespace SFF.Models
{
    public class Rating : BaseEntity
    {
        private Rating()
        {
            // for EF only
        }

        public Rating(double score, int movieId, int studioId)
        {
            if (!(1 <= score && score <= 5))
            {
                throw new ArgumentOutOfRangeException("Score must be between 1 and 5");
            }

            this.Score = score;
            this.MovieId = movieId;
            this.StudioId = studioId;
        }

        [Required]
        public double Score { get; private set; }

        // foreign key property
        public int MovieId { get; private set; }
        // reference property
        public Movie Movie { get; private set; }

        public int StudioId { get; private set; }
        public Studio Studio { get; private set; }
    }
}