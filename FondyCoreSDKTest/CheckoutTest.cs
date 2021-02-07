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
            FondyConfig.Init(MerchantId, SecretKey, ContentType);
            FondyConfig.Endpoint(Endpoint);

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
            FondyConfig.Init(MerchantId, SecretKey, "xml");
            FondyConfig.Endpoint(Endpoint);

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
            FondyConfig.Init(MerchantId, SecretKey, ContentType);
            FondyConfig.Endpoint(Endpoint);
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
            FondyConfig.Init(MerchantId, SecretKey, "xml");
            FondyConfig.Endpoint(Endpoint);

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
            FondyConfig.Init(MerchantId, SecretKey, "form");
            FondyConfig.Endpoint(Endpoint);

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