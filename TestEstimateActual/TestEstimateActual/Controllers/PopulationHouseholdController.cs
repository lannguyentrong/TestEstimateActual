using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TestEstimateActual.IRepositories;
using TestEstimateActual.Models;
using Microsoft.AspNetCore.Http;
using System.Web;
using TestEstimateActual.Services;

namespace TestEstimateActual.Controllers
{
    [ApiController]
    public class PopulationHouseholdController : ControllerBase
    {
        private readonly IPopulationHouseholdService _populationHouseholdService;

        public PopulationHouseholdController(IPopulationHouseholdService populationHouseholdService)
        {
            this._populationHouseholdService = populationHouseholdService;
        }

        [Route("/population")]
        [HttpGet]
        public IActionResult GetPopulationData(string state)
        {
            try
            {
                // Logging
                this._populationHouseholdService._ApiRequestLogging(HttpContext);
                // Get data
                List<Actual> actuals;
                List<Estimate> estimates;
                bool isNotFound;
                this._populationHouseholdService.GetActualAndEstimateByStateIDs(state, out actuals, out estimates, out isNotFound);
                if (isNotFound)
                {
                    return NotFound();
                }
                // Return
                var result = from item in actuals
                             select new
                             {
                                 State = item.State,
                                 Population = item.ActualPopulation
                             };
                result = result.Concat(
                            (estimates
                            .GroupBy(e => e.State)
                            .Select(eg => new
                            {
                                State = eg.First().State,
                                Population = eg.Sum(e => e.EstimatePopulation),
                            }).ToList()));
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [Route("/households")]
        [HttpGet]
        public IActionResult GetHouseholdsData(string state)
        {
            try
            {
                // Logging
                this._populationHouseholdService._ApiRequestLogging(HttpContext);
                // Get data
                List<Actual> actuals;
                List<Estimate> estimates;
                bool isNotFound;
                this._populationHouseholdService.GetActualAndEstimateByStateIDs(state, out actuals, out estimates, out isNotFound);
                if (isNotFound)
                {
                    return NotFound();
                }
                // Return
                var result = from item in actuals
                             select new
                             {
                                 State = item.State,
                                 Households = item.ActualHouseholds
                             };
                result = result.Concat(
                            (estimates
                            .GroupBy(e => e.State)
                            .Select(eg => new
                            {
                                State = eg.First().State,
                                Households = eg.Sum(e => e.EstimateHouseholds),
                            }).ToList()));
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
