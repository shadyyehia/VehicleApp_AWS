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
        public int RegistrationNo { get; set; }
        public int CustomerId{ get; set; }
        public bool isConnected { get; set; }
    }
}
