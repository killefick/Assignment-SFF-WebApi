using System;
using System.Collections.Generic;

namespace SFF.Models
{
    public class Studio : BaseEntity
    {
        public Studio()
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

        public string Name { get; private set; }
        public string Location { get; private set; }

        public int RentalId { get; set; }
        public Rental Rental { get; set; }

        // public IList<Rental> Rental { get; set; } //collection navigation property

    }
}