using AkkaOverview.Providers.Models;
using System;
using System.Collections.Generic;

namespace AkkaOverview.Providers.Contracts
{
    public interface IStreamProvider
    {
        void GetData(string instrument, Action<IEnumerable<Candle>> callback);
    }
}