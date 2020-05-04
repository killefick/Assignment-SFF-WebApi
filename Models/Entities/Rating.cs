using System;
using System.Collections.Generic;

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

        public double Score { get; private set; }

        public Movie Movie { get; set; }
        public Studio Studio { get; set; }
    }
}