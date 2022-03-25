using System.Net;

namespace KC.HMS.Core.Utility
{
    public static class UtilRestClient
    {
        #region POST Methods
        public static string Post(string endPointUrl, string postData = "", WebHeaderCollection headers = null,
         string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = GetClient(endPointUrl, postData, headers, HttpVerb.POST);
            return client.Response();
        }

        public static Task<string> PostAsync(string endPointUrl, string postData = "", WebHeaderCollection headers = null,
           string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = GetClient(endPointUrl, postData, headers, HttpVerb.POST);
            return client.ResponseAsync();
        }

        public static T Post<T>(string endPointUrl, string postData = "", WebHeaderCollection headers = null,
          string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = GetClient(endPointUrl, postData, headers, HttpVerb.POST);
            return client.Response<T>();
        }

        public static T PostAsync<T>(string endPointUrl, string postData = "", WebHeaderCollection headers = null,
           string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = GetClient(endPointUrl, postData, headers, HttpVerb.POST);
            return client.ResponseAsync<T>();
        }
        #endregion

        #region GET Methods 
        public static string Get(string endPointUrl, string postData = "", WebHeaderCollection headers = null,
         string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = GetClient(endPointUrl, postData, headers, HttpVerb.GET);
            return client.Response();
        }

        public static Task<string> GetAsync(string endPointUrl, string postData = "", WebHeaderCollection headers = null,
           string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = GetClient(endPointUrl, postData, headers, HttpVerb.GET);
            return client.ResponseAsync();
        }

        public static T Get<T>(string endPointUrl, string postData = "", WebHeaderCollection headers = null,
          string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = GetClient(endPointUrl, postData, headers, HttpVerb.GET);
            return client.Response<T>();
        }

        public static T GetAsync<T>(string endPointUrl, string postData = "", WebHeaderCollection headers = null,
           string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = GetClient(endPointUrl, postData, headers, HttpVerb.GET);
            return client.ResponseAsync<T>();
        }
        #endregion 

        #region Private Methods 

        private static RestClient GetClient(string endPointUrl, string postData, WebHeaderCollection headers, HttpVerb verb = HttpVerb.GET,
            string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            var client = new RestClient(endPointUrl, verb, postData, contentType, userAgent, encoding);
            if (headers != null && headers.Count > 0)
            {
                client.Headers = headers;
            }

            return client;
        }
        #endregion
    }
}
