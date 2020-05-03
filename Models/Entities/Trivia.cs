using System;
using System.Collections.Generic;

namespace SFF.Models
{
    public class Trivia : BaseEntity
    {
        public Trivia()
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

        public string TriviaText { get; private set; }

        public int MovieId { get; private set; }
        public Movie Movie { get; private set; }

        // public int StudioId { get; private set; }
        // public List<Studio> Studio { get; private set; }
    }
}