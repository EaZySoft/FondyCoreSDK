using FondyCoreSDK;
using FondyCoreSDK.Checkout;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FondyCoreSDKTest
{
    [TestClass]
    public class CheckoutTest : BaseTest
    {
        [TestMethod]
        public void TestCheckout()
        {
            Config.Init(MerchantId, SecretKey, ContentType);
            Config.Endpoint(Endpoint);

            string orderId = Guid.NewGuid().ToString();

            var req = new CheckoutRequest
            {
                order_id = orderId,
                amount = 10000,
                order_desc = "Checkout test json demo",
                currency = "USD"
            };
            var resp = new Url().Post(req);

            Assert.IsNotNull(resp);
            Assert.AreEqual("success", resp.response_status);
            Assert.IsNotNull(resp.payment_id);
        }

        [TestMethod]
        public void TestCheckoutXml()
        {
            Config.Init(MerchantId, SecretKey, "xml");
            Config.Endpoint(Endpoint);

            string orderId = Guid.NewGuid().ToString();

            var req = new CheckoutRequest
            {
                order_id = orderId,
                amount = 10000,
                order_desc = "Checkout test xml demo",
                currency = "USD"
            };
            var resp = new Url().Post(req);

            Assert.IsNotNull(resp);
            Assert.AreEqual("success", resp.response_status);
            Assert.IsNotNull(resp.payment_id);
        }

        [TestMethod]
        public void TestToken()
        {
            Config.Init(MerchantId, SecretKey, ContentType);
            Config.Endpoint(Endpoint);
            var req = new TokenRequest
            {
                order_id = Guid.NewGuid().ToString(),
                amount = 10500,
                order_desc = "Checkout test demo token",
                currency = "USD"
            };
            var resp = new Token().Post(req);

            Assert.IsNotNull(resp);
            Assert.AreEqual("success", resp.response_status);
            Assert.IsNotNull(resp.token);
        }

        [TestMethod]
        public void TestXmlToken()
        {
            Config.Init(MerchantId, SecretKey, "xml");
            Config.Endpoint(Endpoint);

            var req = new TokenRequest
            {
                order_id = Guid.NewGuid().ToString(),
                amount = 10500,
                order_desc = "Checkout test demo xml token",
                currency = "USD"
            };
            var resp = new Token().Post(req);

            Assert.IsNotNull(resp);
            Assert.AreEqual("success", resp.response_status);
            Assert.IsNotNull(resp.token);
        }

        [TestMethod]
        public void TestFormToken()
        {
            Config.Init(MerchantId, SecretKey, "form");
            Config.Endpoint(Endpoint);

            var req = new TokenRequest
            {
                order_id = Guid.NewGuid().ToString(),
                amount = 10500,
                order_desc = "Checkout test demo form token",
                currency = "USD"
            };
            var resp = new Token().Post(req);

            Assert.IsNotNull(resp);
            Assert.AreEqual("success", resp.response_status);
            Assert.IsNotNull(resp.token);
        }
    }
}