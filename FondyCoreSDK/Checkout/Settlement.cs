using FondyCoreSDK.Utils;
using Newtonsoft.Json;

namespace FondyCoreSDK.Checkout
{
    /// <summary>
    /// Settlement url Api
    /// </summary>
    public class Settlement
    {
        public SettlementResponse Post(SettlementRequest req)
        {
            SettlementResponse response;
            string defaultProtocol = FondyConfig.Protocol;
            string defaultContentType = FondyConfig.ContentType;
            FondyConfig.ContentType = "json";
            FondyConfig.Protocol = "2.0";
            req.merchant_id = FondyConfig.MerchantId;
            req.order_type = "settlement";
            try
            {
                response = Client.Invoke<SettlementRequest, SettlementResponse>(req, req.ActionUrl);
            }
            catch (ClientException c)
            {
                response = new SettlementResponse {Error = c};
            }

            if (response.data != null && FondyConfig.Protocol == "2.0")
            {
                FondyConfig.Protocol = defaultProtocol;
                FondyConfig.ContentType = defaultContentType;
                return JsonFormatter.ConvertFromJson<SettlementResponse>(response.data, true, "order");
            }

            return response;
        }
    }

    [JsonObject(Title = "request")]
    public class SettlementRequest : Models.CheckoutRequestModel
    {
        [JsonIgnore] public readonly string ActionUrl = @"settlement/";
    }

    [JsonObject(Title = "response")]
    public class SettlementResponse : Models.ResponseModel
    {
    }
}