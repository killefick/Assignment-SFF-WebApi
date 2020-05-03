using System;
using System.Collections.Generic;

namespace SFF.Models
{
    public class Movie : BaseEntity
    {

        public Movie()
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

        public string Title { get; private set; }
        public byte[] CoverPicture { get; private set; }
        public int AmountInStock { get; private set; }

        // public int StudioId { get; set; }
        // public Studio Studio { get; set; }

        // public IList<Rental> Rental { get; set; }

        public IList<Trivia> Trivia { get; set; }

        public IList<Rating> Rating { get; set; }

        public int RentalId { get; set; }
        public Rental Rental { get; set; }

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