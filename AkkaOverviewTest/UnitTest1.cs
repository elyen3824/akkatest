using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using AkkaOverview.Oanda.Library.Contracts;
using AkkaOverview.Oanda.Library.Factories;
using AkkaOverview.Oanda.Library.Models;
using AkkaOverview.Providers;
using AkkaOverview.Providers.Contracts;

namespace AkkaOverviewTest
{
    [TestClass]
    public class TickDataProviderShould
    {
        private readonly IStreamProvider _tickProvider;
        private readonly Mock<IStreamingDataService> _serviceMock;
        private readonly Candle _candle;

        public TickDataProviderShould()
        {
            _serviceMock = new Mock<IStreamingDataService>();
            _tickProvider = new StreamProvider(StreamingSource.OandaAPI);
            _candle = new Candle() { Time = "1"};
        }

        [TestMethod]
        public void Return_Data_When_Connected()
        {
            _serviceMock.Setup(s => s.Connect()).Returns(true);
            _serviceMock.Setup(s => s.GetData<Candle>(It.IsAny<string>(),It.IsAny<Action<IEnumerable<Candle>>>())).Returns(new List<Candle>() { _candle });

            //sut
            _tickProvider.GetData<Candle>("MSFT");

            _serviceMock.VerifyAll();

            CollectionAssert.Contains(data.ToList(), _candle);
        }


        [TestMethod]
        public void Return_Empty_List_When_Not_Connected()
        {
            _serviceMock.Setup(s => s.Connect()).Returns(false);

            //sut
            ITickProvider provider = new TickProvider(_serviceMock.Object);
            var data = provider.GetData<Candle>("MSFT");

            _serviceMock.VerifyAll();
            _serviceMock.Verify(s => s.GetData<Candle>(It.IsAny<string>()), Times.Never);

            CollectionAssert.AreEquivalent(data.ToList(), Enumerable.Empty<Candle>().ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(CheckoutException))]
        public void Get_Exception_When_Service_Throw_Unhandled_Exception()
        {
            _serviceMock.Setup(s => s.Connect()).Returns(true);
            _serviceMock.Setup(s => s.GetData<Candle>(It.IsAny<string>())).Throws(new CheckoutException());

            //sut
            ITickProvider provider = new TickProvider(_serviceMock.Object);
            var data = provider.GetData<Candle>("MSFT");

            _serviceMock.VerifyAll();
            _serviceMock.Verify(s => s.GetData<Candle>(It.IsAny<string>()), Times.Never);
        }
    }
}
