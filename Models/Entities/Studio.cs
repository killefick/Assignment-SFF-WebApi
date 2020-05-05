using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SFF.Models
{
    public class Studio : BaseEntity
    {
        private Studio()
        {
            // for EF only
        }

        public Studio(string name, string location)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentException("Name and location are required");
            }

            this.Name = name;
            this.Location = location;
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Location { get; private set; }

        // public ICollection<Rental> Rentals { get; private set; }
    }
}