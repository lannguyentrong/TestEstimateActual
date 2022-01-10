using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEstimateActual.IRepositories;
using TestEstimateActual.Models;

namespace TestEstimateActual.Repositories
{
    public class EstimateRepository : IEstimateRepository
    {
        private readonly DatabaseContext _dbContext;

        public EstimateRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Estimate> GetRecordByStateIDs(List<int> stateIDs)
        {
            stateIDs = stateIDs.Distinct().ToList();
            return (from item in this._dbContext.Estimates
                    where stateIDs.Contains(item.State)
                    select item).ToList();
        }
    }
}
