using AkkaOverview.Gateway;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaOverviewTest
{
    /// <summary>
    /// Summary description for TestGateway
    /// </summary>
    [TestClass]
    public class TestGateway
    {
        private readonly ApiGateway _gateway;

        public TestGateway()
        {
            _gateway = new ApiGateway();
        }

        [TestMethod]
        public void TestMethod1()
        {
            _gateway.Candles("EUR_USD");
        }
    }
}
