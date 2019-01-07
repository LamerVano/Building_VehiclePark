using System.ComponentModel.DataAnnotations;

namespace Building_VehiclePark.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}