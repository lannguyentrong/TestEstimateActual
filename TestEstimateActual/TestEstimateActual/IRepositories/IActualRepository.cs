using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEstimateActual.Models;

namespace TestEstimateActual.IRepositories
{
    public interface IActualRepository
    {
        public List<Actual> GetRecordByStateIDs(List<int> stateIDs);
    }
}
