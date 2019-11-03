using System.Collections.Generic;
using AWSServerless_webAPI.Models;

namespace AWSServerless_webAPI.Helpers
{
    public interface IDataManager
    {
        List<Customer> GetCustomers();
        List<Vehicle> GetData();
        List<Vehicle> FilterData(dynamic filter);
        bool isConnected(int vid);
    }
}