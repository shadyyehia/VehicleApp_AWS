using AWSServerless_webAPI.Controllers;
using AWSServerless_webAPI.Helpers;
using AWSServerless_webAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest_VehiclesWebAPI
{
    public class VehicleControllerTest
    {
        private readonly Mock<IDataManager> _mockDataManager;
        private readonly Mock<ITimerManager> _mockTimeManager;
        private readonly Mock<IHubContext<VehicleHub,IVehicleHub>> _mockVehicleHub;
        private readonly VehicleController _controller;

        public VehicleControllerTest() {
            _mockDataManager = new Mock<IDataManager>();
            _mockTimeManager = new Mock<ITimerManager>();
            _mockVehicleHub = new Mock<IHubContext<VehicleHub, IVehicleHub>>();
            _controller = new VehicleController(_mockVehicleHub.Object, _mockDataManager.Object,_mockTimeManager.Object);
        }
        [Fact]
        public void Monitor_returnsIActionResult()
        {
           var result = _controller.Monitor();
           Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void Monitor_CheckSignalRPeriod3Seconds()
        {
            var result = _controller.Monitor();
            _mockTimeManager.Verify(m => m.Configure(It.IsAny<Action>(), It.IsAny<int>(), 3000), Times.Once());
            
        }
       


    }
}
