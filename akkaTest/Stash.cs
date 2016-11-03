using Akka.Actor;

namespace akkaTest
{
    public class StashingActor : IWithUnboundedStash
    {
        public IStash Stash { get; set; }
    }
}
