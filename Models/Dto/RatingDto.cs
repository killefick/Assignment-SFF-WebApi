namespace SFF.Models
{
    public class RatingDto : BaseEntity
    {
        public double Score { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int StudioId { get; set; }
        public Studio Studio { get; set; }
    }
}