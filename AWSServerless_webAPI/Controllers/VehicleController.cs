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
     

        public VehicleController(IHubContext<VehicleHub, IVehicleHub> vehicleHub)
        {
            this.hubContext = vehicleHub;
          
        }
        // GET: api/Vehicles
        [HttpGet]
        [ActionName("getVehiclesList")]
        public IActionResult GetVehicles()
        {
            //delay for a second first load , then update each 3 seconds.
            var timerManager = new TimerManager(() =>
            hubContext.Clients.All.VehicleStatusChange(DataManager.GetData()),1000,60000);
            return Ok(new { Message = "Request Completed" });
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
            return DataManager.isConnected(id);
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