using Building_VehiclePark.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Building_VehiclePark.Models
{
    public class DAL
    {
        static VehiclesContext db = new VehiclesContext();

        public static IEnumerable<Vehicle> GetVehicles()
        {
            return db.Vehicles;
        }

        public static bool Delete(int id)
        {
            Vehicle[] vehicles = db.Vehicles.ToArray();

            try
            {
                db.Vehicles.Remove(vehicles.First(mod => mod.VehicleId == id));

                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Add(Vehicle vehicle)
        {
            try
            {
                db.Vehicles.Add(vehicle);

                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Edit(Vehicle vehicle)
        {
            Vehicle[] vehicles = db.Vehicles.ToArray();

            Vehicle newVehicle = vehicles.First(mod => mod.VehicleId == vehicle.VehicleId);

            newVehicle = ConvertVeh(newVehicle, vehicle);

            try
            {
                db.Entry(newVehicle).State = EntityState.Modified;

                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Vehicle ConvertVeh(Vehicle newVeh, Vehicle oldVeh)
        {
            newVeh.InGarage = oldVeh.InGarage;
            newVeh.InRepair = oldVeh.InRepair;
            newVeh.IsWork = oldVeh.IsWork;
            newVeh.Name = oldVeh.Name;
            newVeh.TypeId = oldVeh.TypeId;

            return newVeh;
        }


    }
}