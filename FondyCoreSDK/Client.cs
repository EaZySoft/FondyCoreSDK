using System;
using System.IO;
using System.Net;
using System.Text;
using FondyCoreSDK.Models;
using FondyCoreSDK.Utils;

namespace FondyCoreSDK
{
    internal static class Client
    {
        private static int _statusCode;
        private static string _response;
        private static string _agent = "EaZySoft-CSharp-SDK";
        private static string _method = "POST";

        /// <summary>
        /// Basic Client
        /// </summary>
        /// <param name="request"></param>
        /// <param name="actionUrl"></param>
        /// <param name="isRoot"></param>
        /// <param name="isCredit"></param>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <returns></returns>
        /// <exception cref="ClientException"></exception>
        public static TResponse Invoke<TRequest, TResponse>(
            TRequest request,
            string actionUrl,
            bool isRoot = true,
            bool isCredit = false
        )
        {
            string data;
            if (FondyConfig.Protocol == "2.0")
            {
                // In protocol v2 Only json allowed
                if (FondyConfig.ContentType != "json")
                {
                    throw new ClientException
                    {
                        ErrorMessage = "In protocol v2 only json content allowed",
                        ErrorCode = "0"
                    };
                }

                data = RequiredParams.GetParamsV2(request, isCredit);
            }
            else
            {
                data = RequiredParams.ConvertRequestByContentType(request);
            }

            string uriString = FondyConfig.Endpoint(null) + actionUrl;

            HttpWebRequest httpRequest = WebRequest.CreateHttp(new Uri(uriString));
            httpRequest.ContentType = GetContentTypeHeader(FondyConfig.ContentType);
            httpRequest.UserAgent = _agent;
            httpRequest.Method = _method;

            byte[] requestData = Encoding.UTF8.GetBytes(data);
            var resultRequest = httpRequest.BeginGetRequestStream(null, null);
            using (Stream postStream = httpRequest.EndGetRequestStream(resultRequest))
            {
                postStream.Write(requestData, 0, requestData.Length);
                postStream.Dispose();
            }

            ExecuteRequest(httpRequest);

            if (_statusCode != 200)
            {
                throw new ClientException
                {
                    ErrorCode = "500",
                    ErrorMessage = _response,
                    RequestId = "Server is gone",
                };
            }

            ErrorResponseModel errorResponse = RequiredParams.ConvertResponseByContentType<ErrorResponseModel>(_response, isRoot);

            if (errorResponse.response_status == "failure" || errorResponse.error_message != null)
            {
                throw new ClientException
                {
                    ErrorCode = errorResponse.error_code,
                    ErrorMessage = errorResponse.error_message,
                    RequestId = errorResponse.request_id,
                };
            }

            return RequiredParams.ConvertResponseByContentType<TResponse>(_response, isRoot);
        }

        /// <summary>
        /// Executes the request
        /// </summary>
        /// <param name="request"></param>
        private static void ExecuteRequest(HttpWebRequest request)
        {
            try
            {
                using (HttpWebResponse httpResponse = request.GetResponse() as HttpWebResponse)
                {
                    _statusCode = (int) httpResponse.StatusCode;
                    StreamReader reader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8, true);
                    _response = reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                using (HttpWebResponse httpErrorResponse = (HttpWebResponse) we.Response)
                {
                    if (httpErrorResponse == null)
                    {
                        throw new NullReferenceException("Http Response is empty " + we);
                    }

                    _statusCode = (int) httpErrorResponse.StatusCode;
                    using (StreamReader reader = new StreamReader(httpErrorResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        _response = reader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Content header by type
        /// </summary>
        private static string GetContentTypeHeader(string type = null)
        {
            switch (type)
            {
                case "xml":
                    return "application/xml";
                case "form":
                    return "application/x-www-form-urlencoded";
                default:
                    return "application/json";
            }
        }
    }
}