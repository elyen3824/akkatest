using AkkaOverview.Oanda.Library.Contracts;
using System;

namespace AkkaOverview.Oanda.Library.Factories
{
    public class StreamingSourceFactory
    {
        private static StreamingSourceFactory _instance;

        public static StreamingSourceFactory Instance => _instance ?? CreateInstance();

        private StreamingSourceFactory()
        {

        }

        private static StreamingSourceFactory CreateInstance()
        {
            _instance = new StreamingSourceFactory();
            return _instance;
        }

        public IStreamingDataService GetProvider(StreamingSource streamingSource)
        {
            switch (streamingSource)
            {
                case StreamingSource.OandaApi:
                    return new OandaStreamingService();
                default:
                    throw new ArgumentOutOfRangeException(nameof(streamingSource), streamingSource, null);
            }
        }
    }

    public enum StreamingSource
    {
        OandaApi
    }
}