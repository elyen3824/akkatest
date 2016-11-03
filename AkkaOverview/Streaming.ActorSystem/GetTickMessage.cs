namespace AkkaOverview.Streaming.ActorSystem
{
    public class GetTickMessage
    {
        public GetTickMessage(string instrument)
        {
            Instrument = instrument;
        }
        public string Instrument { get; }
    }
}
