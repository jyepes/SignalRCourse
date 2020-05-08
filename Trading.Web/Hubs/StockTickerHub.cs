using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Trading.Entities;

namespace Trading.Web.Hub
{
    public class StockTickerHub : Microsoft.AspNet.SignalR.Hub
    {
        private readonly StockTicker _stockTicker;

        public StockTickerHub() :
            this(StockTicker.Instance)
        {
        }

        public StockTickerHub(StockTicker stockTicker)
        {
            _stockTicker = stockTicker;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _stockTicker.GetAllStocks();
        }

        public string GetMarketState()
        {
            return _stockTicker.MarketState.ToString();
        }

        public void OpenMarket()
        {
            _stockTicker.OpenMarket();
        }

        public void CloseMarket()
        {
            _stockTicker.CloseMarket();
        }        
    }
}