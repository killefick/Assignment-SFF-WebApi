namespace SFF.Models
{
    public class RatingDto
    {
        public int Id { get; set; }
        public double Score { get; set; }
        public int MovieId { get; set; }
        public int StudioId { get; set; }
    }
}