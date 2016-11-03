using Akka.Actor;

namespace akkaTest
{
    public class PersonActor : ReceiveActor
    {
        public PersonActor()
        {
            Receive(typeof(Wave), x =>
            {
                Context.Sender.Tell(new VocalGreeting("Hello!"));
            });
        }
    }
}
