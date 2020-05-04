using System;

namespace SFF.Models
{
    public class Trivia : BaseEntity
    {
        private Trivia()
        {
            // for EF only
        }

        public Trivia(string triviaText, int movieId, int studioId)
        {
            if (string.IsNullOrWhiteSpace(triviaText))
            {
                throw new ArgumentException("Text is required");
            }
        }

        public string TriviaText { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int StudioId { get; set; }
        public Studio Studio { get; set; }
    }
}