using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerless_webAPI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public string RegistrationNo { get; set; }
        public Customer Owner{ get; set; }
        public bool isConnected { get; set; }
    }
}
