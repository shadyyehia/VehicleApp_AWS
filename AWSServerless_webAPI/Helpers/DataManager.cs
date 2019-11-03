using AWSServerless_webAPI.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerless_webAPI.Helpers
{
    public static class DataManager
    {
        public static List<Vehicle> vehicles;
        static DataManager()
        {

            vehicles = new List<Vehicle> {
              new Vehicle {
            Id = 1,
            CustomerId = 1,isConnected =false,RegistrationNo="ABC123",VIN="YS2R4X20005399401" },
              new Vehicle {
            Id = 2,
            CustomerId = 1,isConnected =false,RegistrationNo="DEF456",VIN="VLUR4X20009093588" },
              new Vehicle {
            Id = 3,
            CustomerId = 1,isConnected =false,RegistrationNo="GHI789",VIN="VLUR4X20009048066" },
              new Vehicle
              {
                  Id = 4,
                  CustomerId = 2,
                  isConnected = false,
                  RegistrationNo = "JKL012",
                  VIN = "YS2R4X20005388011"
              },
              new Vehicle
              {
                  Id = 5,
                  CustomerId = 2,
                  isConnected = false,
                  RegistrationNo = "MNO345",
                  VIN = "YS2R4X20005387949"
              },
              new Vehicle
              {
                  Id = 6,
                  CustomerId = 3,
                  isConnected = false,
                  RegistrationNo = "PQR678",
                  VIN = "VLUR4X20009048066"
              },
              new Vehicle
              {
                  Id = 7,
                  CustomerId = 3,
                  isConnected = false,
                  RegistrationNo = "STU901",
                  VIN = "YS2R4X20005387055"
              } };
        }

        public static  List<Vehicle> GetData()
        {
            //update the status of the vehicles randomly before sending them
            randomUpdate();
            return vehicles;
        }
        static void randomUpdate()
        {
            var random = new Random();
          
            vehicles.ForEach(x => x.isConnected = random.Next(2) == 1);
        }
        /// <summary>
        /// returns false if the collection is empty, if the vehicle is not connected or 
        /// if the vehicle doesn't exist in the connection
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        public static bool isConnected(int vid)
        {
            if (vehicles == null) return false;
            var Vehicle = vehicles.FirstOrDefault(v => v.Id == vid);
            if (Vehicle != null) {
                return Vehicle.isConnected;
            } ;
            return false;
        }
    }

}
