namespace SFF.Models
{
    public class RatingDto : BaseEntity
    {
        public double Score { get; set; }
        public int MovieId { get; set; }
        public int StudioId { get; set; }
    }
}