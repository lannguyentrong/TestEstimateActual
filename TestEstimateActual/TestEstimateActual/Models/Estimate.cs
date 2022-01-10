using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEstimateActual.Models
{
    public class Estimate
    {
        public int State { get; set; }
        public int District { get; set; }
        public int EstimatePopulation { get; set; }
        public int EstimateHouseholds { get; set; }
    }
}
