using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TestEstimateActual.IRepositories;
using TestEstimateActual.Models;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace TestEstimateActual.Controllers
{
    [ApiController]
    public class StatePopulationController : ControllerBase
    {
        readonly ILogger<StatePopulationController> _log;

        private readonly IActualRepository _actualRepository;
        private readonly IEstimateRepository _estimateRepository;

        public StatePopulationController(
            IActualRepository actualRepository, 
            IEstimateRepository estimateRepository,
            ILogger<StatePopulationController> log)
        {
            _log = log;

            this._actualRepository = actualRepository;
            this._estimateRepository = estimateRepository;
        }
        
        [Route("/population")]
        [HttpGet]
        public IActionResult GetPopulationData(string state)
        {
            try
            {
                // Logging
                this._ApiRequestLogging(HttpContext);
                // Get data
                List<Actual> actuals;
                List<Estimate> estimates;
                bool isNotFound;
                this.GetActualAndEstimateByStateIDs(state, out actuals, out estimates, out isNotFound);
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
                this._ApiRequestLogging(HttpContext);
                // Get data
                List<Actual> actuals;
                List<Estimate> estimates;
                bool isNotFound;
                this.GetActualAndEstimateByStateIDs(state, out actuals, out estimates, out isNotFound);
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

        private void GetActualAndEstimateByStateIDs(string state, out List<Actual> actuals, out List<Estimate> estimates, out bool isNotFound)
        {
            isNotFound = false;

            // Trim
            state = state.Trim();
            // Remove all redundant commas
            Regex regex = new Regex(@"\s");
            state = regex.Replace(state, "");
            // Separate
            string[] stateIDsString = state.Split(',');
            List<int> stateIDs = (from item in stateIDsString
                                  select int.Parse(item)).ToList();
            // List actuals
            actuals = this._actualRepository.GetRecordByStateIDs(stateIDs);
            List<int> actualsStateIDs = (from item in actuals
                                         select item.State).ToList();
            foreach (var item in actualsStateIDs)
            {
                stateIDs.Remove(item);
            }
            // List estimates
            estimates = this._estimateRepository.GetRecordByStateIDs(stateIDs);
            List<int> estimatesStateIDs = (from item in estimates
                                           select item.State).ToList();
            foreach (var item in estimatesStateIDs)
            {
                stateIDs.Remove(item);
            }
            if (stateIDs.Count != 0)
            {
                isNotFound = true;
            }
        }

        private void _ApiRequestLogging(HttpContext httpContext)
        {
            var path = HttpUtility.UrlDecode(HttpContext.Request.Path + HttpContext.Request.QueryString);
            _log.LogInformation($"– API endpoint called - {path}");
        }
    }
}
