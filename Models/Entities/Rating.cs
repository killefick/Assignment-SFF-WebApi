using System;

namespace SFF.Models
{
    public class Rating : BaseEntity
    {
        public Rating()
        {
            // for EF only
        }

        public Rating(double score, int movieId, int studioId)
        {
            if (!(1 <= score && score <= 5))
            {
                throw new ArgumentOutOfRangeException("Score must be between 1 and 5");
            }
        }

        public double Score { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int StudioId { get; set; }
        public Studio Studio { get; set; }
    }
}