using System;
using FondyCoreSDK;
using FondyCoreSDK.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FondyCoreSDKTest
{
    [TestClass]
    public class PsidssTest : BaseTest
    {
        [TestMethod]
        public void PcidssStepOne()
        {
            Config.Init(MerchantId, SecretKey, ContentType);
            Config.Endpoint(Endpoint);

            string orderId = Guid.NewGuid().ToString();

            var req = new StepOneRequest
            {
                order_id = orderId,
                amount = 10000,
                order_desc = "Pcidss Step One Test",
                currency = "EUR",
                card_number = card_number,
                cvv2 = cvv2,
                expiry_date = expiry_date
            };
            var resp = new Pcidss().StepOne(req);

            Assert.IsNotNull(resp);
            Assert.AreEqual("approved", resp.order_status);
            Assert.AreEqual(orderId, resp.order_id);
            Assert.IsNotNull(resp.order_id);
        }

        [TestMethod]
        public void PcidssStepTwo()
        {
            Config.Init(MerchantId, SecretKey, "form");
            Config.Endpoint(Endpoint);

            var req = new StepOneRequest()
            {
                order_id = Guid.NewGuid().ToString(),
                amount = 10000,
                order_desc = "Pcidss Step Two Test",
                currency = "EUR",
                card_number = card_number_3ds,
                cvv2 = cvv2,
                expiry_date = expiry_date
            };
            var resp = new Pcidss().StepOne(req);

            Assert.IsNotNull(resp);
            Assert.IsNotNull(resp.md);
            Assert.IsNotNull(resp.pareq);
            Assert.IsNull(resp.order_id);
        }
    }
}