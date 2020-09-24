using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebsite.Api.ViewModels.EnumTest;
using CoreWebsite.Api.ViewModels.EnumTest.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebsite.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class EnumTestController : Controller
    {
        [HttpGet]
        public MemberViewModel GetMember()
        {
            return new MemberViewModel() { Gender = GenderEnum.Man };
        }

        [HttpGet]
        public PersonViewModel GetPerson()
        {
            return new PersonViewModel() { Gender = GenderEnum.Man };
        }
    }
}