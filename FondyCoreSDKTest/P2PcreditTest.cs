using System;
using FondyCoreSDK;
using FondyCoreSDK.P2pcredit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FondyCoreSDKTest
{
    [TestClass]
    public class P2PcreditTest : BaseTest
    {        
        public new string ContentType = "form";

        [TestMethod]
        public void P2PTest()
        {
            FondyConfig.Init(MerchantId, SecretKey, ContentType, CreditKey);
            FondyConfig.Endpoint(Endpoint);

            string orderId = Guid.NewGuid().ToString();

            var req = new P2PcreditRequest()
            {
                order_id = orderId,
                amount = 10000,
                order_desc = "P2P Credit test request",
                currency = "USD",
                receiver_card_number = card_number
            };

            var resp = new P2Pcredit().Post(req);

            Assert.IsNotNull(resp);
            Assert.AreEqual(orderId, resp.order_id);
            Assert.AreEqual("declined", resp.order_status);
        }
    }
}