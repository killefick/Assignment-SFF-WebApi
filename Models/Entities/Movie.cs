using System;
using System.Collections.Generic;

namespace SFF.Models
{
    public class Movie : BaseEntity
    {

        private Movie()
        {
            // for EF only
        }

        public Movie(string title, int amountInStock)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is required");
            }

            else if (amountInStock < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid amount");
            }

            this.Title = title;
            this.AmountInStock = amountInStock;
        }

        public string Title { get; set; }
        public byte[] CoverPicture { get; set; }
        public int AmountInStock { get; set; }

        public ICollection<Trivia> Trivias { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        public bool IsAvailable()
        {
            return (this.AmountInStock > 0) ? true : false;
        }

        public Movie DecreaseAmount()
        {
            this.AmountInStock -= 1;
            return this;
        }

        public Movie IncreaseAmount()
        {
            this.AmountInStock += 1;
            return this;
        }
    }
}