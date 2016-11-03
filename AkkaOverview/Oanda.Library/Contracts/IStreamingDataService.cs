using System;
using System.Collections.Generic;

namespace AkkaOverview.Oanda.Library.Contracts
{
    public interface IStreamingDataService
    {
        bool Connect();
        void GetCandles<T>(string instrument, Action<IEnumerable<T>> callback);
    }
}