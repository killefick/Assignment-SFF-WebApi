using System;

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

        public double Score { get; private set; }

        //reference navigation property
        public int MovieId { get; private set; }
        public Movie Movie { get; private set; }

        public int StudioId { get; private set; }
        public Studio Studio { get; private set; }
    }
}