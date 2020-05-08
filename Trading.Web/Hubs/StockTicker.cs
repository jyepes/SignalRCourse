using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Threading;
using Trading.Business;
using Trading.Entities;

namespace Trading.Web.Hubs
{
    public class StockTicker
    {

        private IHubConnectionContext<dynamic> Clients { get; set; }

        // Instancia Singleton
        private readonly static Lazy<StockTicker> _instance = new Lazy<StockTicker>(
            () => new StockTicker(GlobalHost.ConnectionManager.GetHubContext<StockTickerHub>().Clients));

        private readonly object _marketStateLock = new object();
        private readonly object _updateStockPricesLock = new object();
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(2000);
        private Timer _timer;
        private volatile bool _updatingStockPrices;
        private volatile MarketState _marketState;

        private StockTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        

        public static StockTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public MarketState MarketState
        {
            get { return _marketState; }
            private set { _marketState = value; }
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            var stockBL = new StockBL();
            return stockBL.GetAllStocks();            
        }

        public void OpenMarket()
        {
            lock (_marketStateLock)
            {
                if (MarketState != MarketState.Open)
                {
                    _timer = new Timer(UpdateStockPrices, null, _updateInterval, _updateInterval);

                    MarketState = MarketState.Open;

                    BroadcastMarketStateChange(MarketState.Open);
                }
            }
        }

        public void CloseMarket()
        {
            lock (_marketStateLock)
            {
                if (MarketState == MarketState.Open)
                {
                    if (_timer != null)
                    {
                        _timer.Dispose();
                    }

                    MarketState = MarketState.Closed;

                    BroadcastMarketStateChange(MarketState.Closed);
                }
            }
        }        

        private void UpdateStockPrices(object state)
        {
            // Función que se ejecuta repetidamente según el intervalo definido en el Timer.
            lock (_updateStockPricesLock)
            {
                if (!_updatingStockPrices)
                {
                    _updatingStockPrices = true;

                    var stockBL = new StockBL();
                    var stocks = stockBL.GetLastStocks();

                    foreach (var stock in stocks)
                    {                        
                        BroadcastStockPrice(stock);
                    }

                    _updatingStockPrices = false;
                }
            }
        }        

        private void BroadcastMarketStateChange(MarketState marketState)
        {
            switch (marketState)
            {
                case MarketState.Open:
                    Clients.All.marketOpened();
                    break;
                case MarketState.Closed:
                    Clients.All.marketClosed();
                    break;
                default:
                    break;
            }
        }

        private void BroadcastStockPrice(Stock stock)
        {
            Clients.All.updateStockPrice(stock);
        }        
    }
}