using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using TestEstimateActual.IRepositories;
using TestEstimateActual.Models;

namespace TestEstimateActual.Services
{
    public class PopulationHouseholdService : IPopulationHouseholdService
    {
        private readonly ILogger<PopulationHouseholdService> _log;
        private readonly IActualRepository _actualRepository;
        private readonly IEstimateRepository _estimateRepository;

        public PopulationHouseholdService(
            IActualRepository actualRepository,
            IEstimateRepository estimateRepository,
            ILogger<PopulationHouseholdService> log)
        {
            _log = log;

            this._actualRepository = actualRepository;
            this._estimateRepository = estimateRepository;
        }

        public void GetActualAndEstimateByStateIDs(string state, out List<Actual> actuals, out List<Estimate> estimates, out bool isNotFound)
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

        public void _ApiRequestLogging(HttpContext httpContext)
        {
            var path = HttpUtility.UrlDecode(httpContext.Request.Path + httpContext.Request.QueryString);
            _log.LogInformation($"– API endpoint called - {path}");
        }
    }
}
