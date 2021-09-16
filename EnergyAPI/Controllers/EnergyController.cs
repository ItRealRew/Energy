using Microsoft.AspNetCore.Mvc;
using System;
using EnergyAPI.Models;
using EnergyAPI.DAO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        EnergyApi API = new EnergyApi();
        [HttpGet]
        public List<Measuring> Get()
        {
            return API.GetMeasurings();
        }

        [Route("api/")]
        public List<Meters> GetMeters()
        {
            return API.GetMeters();
        }

        [Route("api/{MetersId}/{DateValue}")]
        public List<ViewMeasuring> Get(int MetersId, string DateValue)
        {
            DateTime ActualDate = Convert.ToDateTime(DateValue);
            return API.GetMeasurings(MetersId, ActualDate);
        }

    }
}
