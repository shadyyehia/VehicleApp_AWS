using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SocietyManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {

        public MemberController()
        {

        }
        // GET: api/Members
        [HttpGet]
        [ActionName("getMembersList")]
        public IEnumerable<MembersViewModel> GetMembers()
        {
            var listMembers = new List<MembersViewModel>() {
                new MembersViewModel() { firstName="shady",lastName="yehia",address="NasrCity",occupier=1,mntncPaidFreq=5,maintenance=4,memberId="355xc4r4"},
            new MembersViewModel() { firstName = "shady", lastName = "yehia", address = "NasrCity", occupier = 1, mntncPaidFreq = 5, maintenance = 4, memberId = "355xc4r4" },
            new MembersViewModel() { firstName = "shady", lastName = "yehia", address = "NasrCity", occupier = 1, mntncPaidFreq = 5, maintenance = 4, memberId = "355xc4r4" }

            };
            return listMembers;
        }

    

        public class MembersViewModel
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string address { get; set; }
            public int gender { get; set; }
            public int occupier { get; set; }
            public int mntncPaidFreq { get; set; }
            public int maintenance { get; set; }
            public string memberId { get; set; }
          
        }
    }
}