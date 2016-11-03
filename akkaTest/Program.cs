using Akka.Actor;
using AkkaOverview.Gateway;
using System;

namespace akkaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var system = ActorSystem.Create("Main");
            //var actor = system.ActorOf<TestActor>("testActor");
            //actor.Tell("hello");
            //actor.Tell("world !!!");
            //actor.Tell(new Message());

            //var fsmactor = system.ActorOf<MyActor>("FSMActor");
            //fsmactor.Tell(new TestStateOne());

            ApiGateway apiGateway = new ApiGateway();
            apiGateway.Candles("EUR_USD");

            Console.ReadLine();
        }
    }

    internal class TestActor : ReceiveActor, IWithUnboundedStash
    {
        public TestActor()
        {
            Receive<string>(x => Stash.Stash());
            Receive<Message>(x =>
            {
                Stash.UnstashAll();
                Become(Writing);
            });
        }

        private void Writing(object message)
        {
            Console.WriteLine((string)message);
        }

        public IStash Stash { get; set; }
    }

    public class Message
    {
        public string Name { get; set; }
    }

    class MyActor : FSM<ITestState, ITestData>
    {
        public MyActor()
        {
            StartWith(TestStateOne.Instance, new TestData());
            When(TestStateOne.Instance, EventTestStateOne());
            When(TestStateTwo.Instance, EventTestsStateTwo());
            Initialize();
        }

        private StateFunction EventTestsStateTwo()
        {
            return @event =>
            {
                Console.WriteLine("is state two");
                return GoTo(TestStateOne.Instance);
            };
        }

        private StateFunction EventTestStateOne()
        {
            return @event =>
                            {
                                bool isfromTestStateOne = @event.FsmEvent is TestStateOne;

                                if (isfromTestStateOne)
                                {
                                    Console.WriteLine("Is in state one");
                                    return GoTo(TestStateTwo.Instance);
                                }
                                return Stay();
                            };
        }
    }

    internal interface ITestData
    {
    }

    internal interface ITestState
    {
    }

    public class TestStateOne : ITestState
    {
        public static readonly TestStateOne Instance = new TestStateOne();
    }

    class TestStateTwo : ITestState
    {
        public static readonly TestStateTwo Instance = new TestStateTwo();
    }

    public class TestData : ITestData
    {
    }
}
