using System;
using System.Collections.Generic;

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

        public string Name { get;  set; }
        public string Location { get;  set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}