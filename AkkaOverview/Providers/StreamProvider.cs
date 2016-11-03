using AkkaOverview.Oanda.Library.Contracts;
using AkkaOverview.Oanda.Library.Factories;
using AkkaOverview.Providers.Contracts;
using AkkaOverview.Providers.Factories;
using AkkaOverview.Providers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AkkaOverview.Providers
{
    public class StreamProvider : IStreamProvider
    {
        private readonly IStreamingDataService _service;
        private bool _isConnected;
        public StreamProvider(StreamingProvider streamingSource)
        {
            _service = StreamingSourceFactory.Instance.GetProvider(streamingSource.ConvertTo<StreamingSource>());

        }

        public void GetData(string instrument, Action<IEnumerable<Candle>> callback)
        {
            Connect();
            if (_isConnected)
            {
                _service.GetCandles<Oanda.Library.Models.Candle>(instrument,
                    enumerable =>
                    {
                        if (callback != null)
                        {
                            List<Candle> dataList = enumerable.ToList().ConvertAll(input => (Candle)input);
                            callback(dataList);
                        }
                    });
            }
        }

        private void Connect()
        {
            _isConnected = _service.Connect();
        }
    }
}