using System;
using System.ComponentModel.DataAnnotations;

namespace SFF.Models
{
    public class Trivia : BaseEntity
    {
        private Trivia()
        {
            // for EF only
        }

        public Trivia(string triviaText, int studioId, int movieId)
        {
            if (string.IsNullOrWhiteSpace(triviaText))
            {
                throw new ArgumentException("Text is required");
            }

            this.TriviaText = triviaText;
            this.StudioId = studioId;
            this.MovieId = movieId;
        }


        [Required]
        public string TriviaText { get; private set; }

        // foreign key property
        public int MovieId { get; private set; }
        // reference property
        public Movie Movie { get; private set; }

        public int StudioId { get; private set; }
        public Studio Studio { get; private set; }
    }
}