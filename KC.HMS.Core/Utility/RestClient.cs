using Newtonsoft.Json;
using System.Net;

namespace KC.HMS.Core.Abstracts
{
    public class RestClient : IRestClient
    {

        #region Properties 

        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        public WebHeaderCollection Headers { get; set; } = new WebHeaderCollection();
        public string UserAgent { get; set; }

        public CookieContainer CookieContainer { get; private set; } = new CookieContainer();

        public string Encoding
        {
            get; set;
        }
        #endregion

        #region Constructor

        public RestClient(string endpoint, HttpVerb method = HttpVerb.GET, string postData = "",
            string contentType = "application/json", string userAgent = "", string encoding = "iso-8859-1")
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = contentType;
            PostData = postData;
            //Headers = new WebHeaderCollection();
            UserAgent = userAgent;
            //CookieContainer = new CookieContainer();
            Encoding = encoding;
        }

        #endregion

        #region Removed Methods 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Response(string parameters = "")
        {
            return GetResponse(parameters);
        }

        /// <summary>
        /// Get the Response from the URL 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">Parameters append on end of the EndPoint url if any</param>
        /// <returns></returns>
        public T Response<T>(string parameters = "")
        {
            //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string result = GetResponse(parameters);
            return JsonConvert.DeserializeObject<T>(result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Task<string> ResponseAsync(string parameters = "")
        {
            return GetResponseAsync(parameters);
        }

        /// <summary>
        /// Get the Response from the URL 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">Parameters append on end of the EndPoint url if any</param>
        /// <returns></returns>
        public T ResponseAsync<T>(string parameters = "")
        {
            //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            try
            {
                var task = GetResponseAsync(parameters);
                return JsonConvert.DeserializeObject<T>(task.Result);
            }
            catch (Exception ex)
            {
                var result = JsonConvert.SerializeObject(default(T));
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Parameters append on end of the EndPoint url 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string GetResponse(string parameters = "")
        {


            #region Code Contracts 

            // ValidateInput();

            #endregion

            var result = string.Empty;

            #region Perform Web Request 

            var request = CreateRequest(parameters);

            #endregion

            #region Get Response 

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                ValidateResponse(response);
                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }

            #endregion 
            return result;// JsonConvert.DeserializeObject<T>(result);
        }

        private HttpWebRequest CreateRequest(string parameters)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
            request.CookieContainer = CookieContainer;

            if (Headers.Count > 0)
            {
                request.Headers = Headers;
            }

            request.Method = Method.ToString();
            request.UserAgent = UserAgent;
            request.ContentLength = 0;
            request.ContentType = ContentType;
            request.Accept = ContentType;
            if (!string.IsNullOrEmpty(PostData) && (Method == HttpVerb.POST || Method == HttpVerb.POST))
            {
                //var encoding = new UTF8Encoding();
                var bytes = System.Text.Encoding.GetEncoding(Encoding).GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            return request;
        }

        private async Task<string> GetResponseAsync(string parameters = "")
        {
            //#region Code Contracts 

            //ValidateInput();

            //#endregion

            var result = string.Empty;

            #region Perform Web Request 

            var request = CreateRequest(parameters);

            #endregion


            #region Get Response 

            using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
            {
                ValidateResponse(response);

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }

            #endregion


            return result;// JsonConvert.DeserializeObject<T>(result);
        }

        //private void ValidateInput()
        //{
        //    //Contract.Requires(!string.IsNullOrEmpty(EndPoint), "EndPoint is required");
        //    //Contract.Requires(!string.IsNullOrEmpty(ContentType), "ContentType is required");
        //    //Contract.Requires(!string.IsNullOrEmpty(Encoding), "Encoding is required");
        //    //Contract.Requires(!Enum<HttpVerb>.IsDefined(Method.ToString()), "HttpVerb is required");

        //    //Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(EndPoint), "Exception!!");
        //    //Contract.Ensures(Contract.Result < !string.IsNullOrEmpty(result));
        //}

        private static void ValidateResponse(HttpWebResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                throw new ApplicationException(message);
            }
        }

        #endregion


    }
}
