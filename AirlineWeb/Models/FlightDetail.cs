using System.ComponentModel.DataAnnotations;

namespace AirlineWeb.Models
{
    public class FlightDetail
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string FlightCode { get; set; }
        
        [Required]
        public decimal Price { get; set; }
    }
}