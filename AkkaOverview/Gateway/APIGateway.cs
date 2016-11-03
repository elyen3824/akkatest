using Akka.Actor;
using AkkaOverview.Streaming.ActorSystem;

namespace AkkaOverview.Gateway
{
    public class ApiGateway : IGateway
    {
        public void Candles(string instrument)
        {
            var system = ActorSystem.Create("Streaming");
            var actor = system.ActorOf<StreamActor>("stream");
            actor.Tell(new GetTickMessage(instrument));
        }
    }

    public interface IGateway
    {
        void Candles(string instrument);
    }
}
