using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWSServerless_webAPI.Helpers;
using AWSServerless_webAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AWSServerless_webAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IHubContext<VehicleHub, IVehicleHub> hubContext;
        ITimerManager currentTimeManager;
        IDataManager datamanager;
        
        public VehicleController(IHubContext<VehicleHub, IVehicleHub> vehicleHub, IDataManager datamanager,ITimerManager timermanager )
        {
            this.datamanager = datamanager;
            this.hubContext = vehicleHub;
            currentTimeManager = timermanager;


        }
        // Post: api/Vehicles
        [HttpGet]
        [ActionName("monitor")]
        public IActionResult Monitor()
        {
            currentTimeManager.Configure(() =>
            hubContext.Clients.All.VehicleStatusChange(this.datamanager.GetData()),500,3000);
            return Ok(new { Message = "Request Completed" });
        }

        [HttpPost]
        [ActionName("filterData")]
        public List<Vehicle> FilterData([FromBody]dynamic filter)
        {
            return this.datamanager.FilterData(filter);
            
        }

        // GET: api/Vehicles
        [HttpGet]
        [ActionName("getCustomers")]
        public IEnumerable<Customer> getCustomers()
        {
            return datamanager.GetCustomers();
        }


        /// <summary>
        /// Ping vehicle to check it's connection state
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("ping")]
        public bool IsConnected(int id)
        {
            return this.datamanager.isConnected(id);
        }
        //[HttpGet]
        //[ActionName("updateV")]
        //public void updateV()
        //{
        //    callClient().Wait();
        //}
        //public async Task callClient() {
        //    await this.hubContext.Clients.All.VehicleStatusChange(1, true);
        //}

    }
}