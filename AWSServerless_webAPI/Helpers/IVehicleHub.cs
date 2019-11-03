using AWSServerless_webAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerless_webAPI.Helpers
{
    public interface IVehicleHub
    {
        
        Task VehicleStatusChange(List<Vehicle> vehicles);
    }
}
