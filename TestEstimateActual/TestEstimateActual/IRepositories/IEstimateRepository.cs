using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEstimateActual.Models;

namespace TestEstimateActual.IRepositories
{
    public interface IEstimateRepository
    {
        public List<Estimate> GetRecordByStateIDs(List<int> stateIDs);
    }
}
