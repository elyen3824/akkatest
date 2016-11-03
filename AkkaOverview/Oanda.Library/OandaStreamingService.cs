using AkkaOverview.Oanda.Library.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AkkaOverview.Oanda.Library
{
    public class OandaStreamingService : IStreamingDataService
    {
        private readonly Dictionary<string, string> _parameters;
        private const string BaseUrl = @"https://api-fxpractice.oanda.com/v1/candles";

        public OandaStreamingService()
        {
            _parameters = new Dictionary<string, string>
            {
                {"count", "500"},
                {"candleFormat", "midpoint"},
                {"granularity", "D"},
                {"dailyAlignment", "0"}
            };
        }

        public bool Connect()
        {
            return true;
        }

        public void GetCandles<T>(string instrument, Action<IEnumerable<T>> callback)
        {
            try
            {
                _parameters.Add("instrument", instrument);
                GetFromService(callback);
            }
            catch (Exception e)
            {
                GetFromService(callback);
            }

        }

        private void GetFromService<T>(Action<IEnumerable<T>> callback)
        {
            var webClient = new HttpClient();
            webClient.DefaultRequestHeaders.Clear();
            webClient.DefaultRequestHeaders.Add("Authorization",
                "Bearer token");
            webClient.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
            webClient.DefaultRequestHeaders.Add("X-Accept-Datetime-Format", "UNIX");
            string request = RequestBuilder.Build(BaseUrl, _parameters);
            var response = webClient.GetAsync(request).Result;
            if (callback != null)
            {
                CallBack(callback, response);
            }
        }

        private static void CallBack<T>(Action<IEnumerable<T>> callback, HttpResponseMessage response)
        {
            var json = response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(json.Result);
            var selectToken = jObject.SelectToken("candles");
            callback(JsonConvert.DeserializeObject<IList<T>>(selectToken.ToString()));
        }
    }

    public class RequestBuilder
    {
        public static string Build(string baseUrl, Dictionary<string, string> parameters)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(baseUrl).Append('?');
            foreach (var kv in parameters)
            {
                strBuilder.Append(kv.Key).Append('=').Append(kv.Value).Append('&');
            }
            return strBuilder.ToString().TrimEnd('&');
        }
    }
}
