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
            
            this.TriviaText = triviaText;
            this.StudioId = studioId;
            this.MovieId = movieId;
        }

        public string TriviaText { get; private set; }

        public int MovieId { get; private set; }
        public Movie Movie { get; private set; }

        public int StudioId { get; private set; }
        public Studio Studio { get; private set; }
    }
}