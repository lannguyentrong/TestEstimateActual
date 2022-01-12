using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEstimateActual.Models;

namespace TestEstimateActual.Services
{
    public interface IPopulationHouseholdService
    {
        public void GetActualAndEstimateByStateIDs(string state, out List<Actual> actuals, out List<Estimate> estimates, out bool isNotFound);
        public void _ApiRequestLogging(HttpContext httpContext);
    }
}
