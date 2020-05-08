using System.Collections.Generic;
using Trading.DataAccess;
using Trading.Entities;

namespace Trading.Business
{
    public class StockBL
    {
        public IEnumerable<Stock> GetAllStocks()
        {
            var stockDL = new StockDAL();
            return stockDL.GetAllStocks();
        }

        public IEnumerable<Stock> GetLastStocks()
        {
            var stockDL = new StockDAL();
            return stockDL.GetLastStocks();
        }
    }
}
