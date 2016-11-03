using Akka.Actor;
using Akka.Util.Internal;
using AkkaOverview.Providers.Factories;
using AkkaOverview.Streaming.ActorSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AkkaOverview.Streaming.ActorSystem
{
    public class StreamActor : ReceiveActor
    {
        private static Converter<Providers.Models.Candle, Candle> Converter => input => (Candle)input;
        public StreamActor()
        {
            var streamProvider = StreamProviderFactory.Instance.GetStreamProvider(StreamingProvider.OandaApi);

            IEnumerable<Candle> listCandles = Enumerable.Empty<Candle>();

            Receive<GetTickMessage>(getTickMessage =>
            {
                streamProvider.GetData(getTickMessage.Instrument, candles => { listCandles = ConvertCandles(candles); });
                listCandles.ForEach(candle => Console.WriteLine(candle.ToString()));
            });
        }

        private static IEnumerable<Candle> ConvertCandles(IEnumerable<Providers.Models.Candle> candles)
        {
            IEnumerable<Candle> listCandles = candles.ToList().ConvertAll(Converter);
            return listCandles;
        }
    }
}