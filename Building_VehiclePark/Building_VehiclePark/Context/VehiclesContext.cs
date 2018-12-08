using Building_VehiclePark.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Building_VehiclePark.Context
{
    public class VehiclesContext: DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}