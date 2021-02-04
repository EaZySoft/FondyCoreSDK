using System;
using System.Collections.Generic;
using System.Text;

namespace FondyCoreSDKTest
{
    public class BaseTest
    {
        public int MerchantId = 1396424;
        public string SecretKey = "test";
        public string CreditKey = "testcredit";

        public string ContentType = "json";
        public string Endpoint = "pay.fondy.eu";

        public string card_number = "4444555511116666";
        public string card_number_3ds = "4444555566661111";
        public string cvv2 = "111";
        public string expiry_date = "0130";
    }
}
