﻿using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FondyCoreSDK.Models
{
    /// <summary>
    /// Receiver Model
    /// </summary>
    [JsonObject(Title = "receiver")]
    [XmlInclude(typeof(Merchant))]
    [XmlInclude(typeof(Card))]
    [XmlInclude(typeof(BankAccount))]
    [XmlRoot("receiver")]
    public class ReceiverModel
    {
        [JsonProperty(PropertyName = "requisites")]
        public dynamic requisites { get; set; }

        [JsonProperty(PropertyName = "type")] public string type { get; set; }
    }
}