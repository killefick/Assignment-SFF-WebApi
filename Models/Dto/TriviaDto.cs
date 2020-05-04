namespace SFF.Models
{
    public class TriviaDto : BaseEntity
    {
        public string TriviaText { get; set; }
        public int MovieId { get; set; }
        public int StudioId { get; set; }
    }
}