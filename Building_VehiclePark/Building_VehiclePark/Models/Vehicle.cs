using System.ComponentModel.DataAnnotations;

namespace Building_VehiclePark.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        [Required]
        public string Name { get; set; }
        public string TypeId { get; set; }
        public bool InGarage { get; set; }
        public bool InRepair { get; set; }
        public bool IsWork { get; set; }

        public Vehicle()
        {
            Name = "";
        }
    }
}