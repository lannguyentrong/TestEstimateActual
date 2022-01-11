using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestEstimateActual.Models
{
    public class Actual
    {
        [Key]
        public int State { get; set; }
        public int ActualPopulation { get; set; }
        public int ActualHouseholds { get; set; }
    }
}
