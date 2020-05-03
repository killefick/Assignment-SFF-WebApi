namespace SFF.Models
{
    public class TriviaDto
    {
        public int Id { get; set; }
        public string TriviaText { get; set; }
        public int MovieId { get; set; }
        public int StudioId { get; set; }
    }
}