namespace SFF.Models
{
    public class RentalDto : BaseEntity
    {
        private RentalDto()
        {
            // for EF only
        }

        public RentalDto(int movieId, int studioId)
        {
            this.MovieId = movieId;
            this.StudioId = studioId;
        }

        public int MovieId { get; set; }
        public int StudioId { get; set; }
    }
}