﻿using System.Xml.Serialization;
using FondyCoreSDK.Models;
using FondyCoreSDK.Utils;
using Newtonsoft.Json;

namespace FondyCoreSDK.Checkout
{
    /// <summary>
    /// Subscription Api
    /// </summary>
    public class Subscription
    {
        public SubscriptionResponse Post(SubscriptionRequest request)
        {
            SubscriptionResponse response;
            string defaultProtocol = Config.Protocol;
            string defaultContentType = Config.ContentType;
            Config.ContentType = "json";
            Config.Protocol = "2.0";

            request.merchant_id = Config.MerchantId;
            request.version = Config.Protocol;
            request.subscription = "Y";
            request.signature = Signature.GetRequestSignature(RequiredParams.GetHashProperties(request));

            try
            {
                response = Client.Invoke<SubscriptionRequest, SubscriptionResponse>(request, request.ActionUrl);
            }
            catch (ClientException c)
            {
                response = new SubscriptionResponse {Error = c};
            }

            if (response.data != null && Config.Protocol == "2.0")
            {
                Config.Protocol = defaultProtocol;
                Config.ContentType = defaultContentType;
                return JsonFormatter.ConvertFromJson<SubscriptionResponse>(response.data, true, "order");
            }

            return response;
        }
    }

    [XmlRoot("request")]
    [JsonObject(Title = "request")]
    public class SubscriptionRequest : CheckoutRequestModel
    {
        [JsonProperty(PropertyName = "recurring_data")]
        public ReccuringData recurring_data { get; set; }

        [JsonIgnore] [XmlIgnore] public readonly string ActionUrl = @"checkout/url/";
    }

    [XmlRoot("response")]
    [JsonObject(Title = "response")]
    public class SubscriptionResponse : CheckoutResponseModel
    {
        [JsonProperty(PropertyName = "payment_id")]
        public int payment_id { get; set; }

        [JsonProperty(PropertyName = "checkout_url")]
        public string checkout_url { get; set; }
    }
}