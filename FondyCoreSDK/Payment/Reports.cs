﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using FondyCoreSDK.Models;
using FondyCoreSDK.Utils;
using Newtonsoft.Json;

namespace FondyCoreSDK.Payment
{
    public class Reports
    {
        public ReportsResponse Post(ReportsRequest req)
        {
            ReportsResponse response;
            req.merchant_id = FondyConfig.MerchantId;
            req.version = FondyConfig.Protocol;
            req.signature = Signature.GetRequestSignature(RequiredParams.GetHashProperties(req));
            try
            {
                response = Client.Invoke<ReportsRequest, ReportsResponse>(req, req.ActionUrl, false);
            }
            catch (ClientException c)
            {
                response = new ReportsResponse {Error = c};
            }

            if (response.data != null && FondyConfig.Protocol == "2.0")
            {
                return JsonFormatter.ConvertFromJson<ReportsResponse>(response.data, true, "order");
            }

            return response;
        }
    }

    [XmlRoot("request")]
    [JsonObject(Title = "request")]
    public class ReportsRequest
    {
        [JsonProperty(PropertyName = "merchant_id")]
        public int merchant_id { get; set; }

        [JsonProperty(PropertyName = "signature")]
        public string signature { get; set; }

        [JsonProperty(PropertyName = "date_from")]
        public string date_from { get; set; }

        [JsonProperty(PropertyName = "date_to")]
        public string date_to { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "version")]
        public string version { get; set; }

        [JsonIgnore] [XmlIgnore] public readonly string ActionUrl = @"reports/";
    }
    [XmlRoot("response")]
    public class ReportsResponse : ResponseV2
    {
        [JsonProperty(PropertyName = "response")]
        [XmlElement("order")]
        public List<ResponseModel> response { get; set; }

        [JsonIgnore] [XmlIgnore] public ClientException Error { get; set; }
    }
}