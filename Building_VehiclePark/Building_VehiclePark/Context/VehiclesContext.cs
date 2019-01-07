using Building_VehiclePark.Models;
using System.Data.Entity;

namespace Building_VehiclePark.Context
{
    public class VehiclesContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}