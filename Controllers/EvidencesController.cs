using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace covid_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvidencesController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetCountries()
        {
            return new string[] { };
        }

        [HttpGet("{countryCode}")]
        public ActionResult<string> GetByCountry(string countryCode)
        {
            return "CountryCode: " + countryCode;
        }

        [HttpPost]
        public void HelpConfirmedCase(
            [FromBody]
            CovidCase c)
        {
            // 
        }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
    }
}
