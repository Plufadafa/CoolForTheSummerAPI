using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolForTheSummerApi.Models;
using CoolForTheSummerApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolForTheSummerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoolForTheSummerUserController : ControllerBase
    {
        private readonly CoolForTheSummerService _coolForTheSummerService;

        public CoolForTheSummerUserController(CoolForTheSummerService coolForTheSummerService)
        {
            _coolForTheSummerService = coolForTheSummerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CoolForTheSummerUser), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<CoolForTheSummerUser>> Get()
        {
            return _coolForTheSummerService.Get();
        }
    }
}
