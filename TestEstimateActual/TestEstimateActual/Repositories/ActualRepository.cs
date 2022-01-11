using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEstimateActual.IRepositories;
using TestEstimateActual.Models;

namespace TestEstimateActual.Repositories
{
    public class ActualRepository : IActualRepository
    {
        private readonly DatabaseContext _dbContext;

        public ActualRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Actual> GetRecordByStateIDs(List<int> stateIDs)
        {
            stateIDs = stateIDs.Distinct().ToList();
            return (from item in this._dbContext.Actuals
                    where stateIDs.Contains(item.State)
                    select item).ToList();
        }
    }
}
