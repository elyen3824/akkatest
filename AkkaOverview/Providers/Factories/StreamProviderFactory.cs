using AkkaOverview.Providers.Contracts;

namespace AkkaOverview.Providers.Factories
{
    public class StreamProviderFactory
    {
        private static StreamProviderFactory _instance;

        public static StreamProviderFactory Instance => _instance ?? CreateInstance();

        private static StreamProviderFactory CreateInstance()
        {
            _instance = new StreamProviderFactory();
            return _instance;
        }

        private StreamProviderFactory()
        {

        }
        public IStreamProvider GetStreamProvider(StreamingProvider streamingSource)
        {
            return new StreamProvider(streamingSource);
        }
    }
}