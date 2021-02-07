namespace FondyCoreSDK
{
    public static class FondyConfig
    {
        /// <summary>
        /// Merchant identification
        /// </summary>
        public static int MerchantId { get; set; }

        /// <summary>
        /// Merchant Secret Key
        /// </summary>
        public static string SecretKey { get; set; }

        /// <summary>
        /// Merchant Credit Key
        /// </summary>
        public static string CreditKey { get; set; }

        /// <summary>
        /// Content type (json/xml/form)
        /// </summary>
        private static string contentType = "json";

        public static string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        /// <summary>
        /// Protocol version supported (1.0/2.0)
        /// </summary>
        public static string Protocol = "1.0";

        /// <summary>
        /// Endpoint version supported (1.0/2.0)
        /// </summary>
        public static string ApiHost = "api.fondy.eu";

        /// <summary>
        /// Endpoint version supported (2.0)
        /// </summary>
        public static string ApiHost2 = "pay.fondy.eu";


        public static void Init(int merchantId, string secretKey, string contentType = "json", string creditKey = "")
        {
            MerchantId = merchantId;
            SecretKey = secretKey;
            ContentType = contentType;
            CreditKey = creditKey;
        }

        public static void Init(string merchantId, string secretKey, string contentType = "json", string creditKey = "")
        {
            MerchantId = int.Parse(merchantId);
            SecretKey = secretKey;
            ContentType = contentType;
            CreditKey = creditKey;
        }

        /// <summary>
        /// Set api endpoint
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Endpoint(string url)
        {
            string domain = @"https://{0}/api/";
            if (url == null)
            {
                url = Protocol == "2.0" ? ApiHost2 : ApiHost;                
            }

            return string.Format(domain, url);
        }
    }
}
