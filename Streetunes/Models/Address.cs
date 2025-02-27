using System.ComponentModel.DataAnnotations;

namespace Streetunes.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? street { get; set; }

        public string? Country { get; set; }
    }
}
