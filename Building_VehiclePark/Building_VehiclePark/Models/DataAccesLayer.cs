using Building_VehiclePark.Context;
using Building_VehiclePark.Migrations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Building_VehiclePark.Models
{
    public class DataAccesLayer
    {
        static VehiclesContext db = new VehiclesContext();

        static DataAccesLayer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VehiclesContext, Configuration>());
        }

        public static IEnumerable<Vehicle> GetVehicles()
        {
            return db.Vehicles;
        }

        public static bool AddUser(User user)
        {
            try
            {
                db.Users.Add(user);

                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool LogIn(string name, string password)
        {
            User[] users = db.Users.ToArray();

            foreach(User i in users)
            {
                if(i.Name == name)
                {
                    if(i.Password == password)
                    {
                        return true;
                    }
                }
            }
            return false;
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