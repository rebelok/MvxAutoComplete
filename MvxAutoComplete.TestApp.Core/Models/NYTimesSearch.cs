using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace MvxAutoComplete.TestApp.Core.Models
{
    public class NYTimesSearch
    {
        const string UrlTemplate = "http://api.nytimes.com/svc/search/v2/articlesearch.json?q={0}&fl=snippet,web_url,pub_date&api-key=0160952707e982ff0507fd134f5f4b63:11:69369605";
        private readonly string _searchTerm;
        private readonly Action<IEnumerable<Article>> _success;
        private readonly Action<Exception> _error;
        private readonly int _take;

        private NYTimesSearch(string searchTerm, Action<IEnumerable<Article>> success, Action<Exception> error, int take)
        {
            _searchTerm = searchTerm;
            _success = success;
            _error = error;
            _take = take;
        }
       
        public static void StartAsyncSearch(string searchTerm, int take, Action<IEnumerable<Article>> success, Action<Exception> error)
        {
            var search = new NYTimesSearch(searchTerm, success, error, take);
            search.StartSearch();
        }

        private void StartSearch()
        {
            try
            {
                var uri = string.Format(UrlTemplate, _searchTerm);
                var request = WebRequest.Create(new Uri(uri));
                request.BeginGetResponse(ReadCallback, request);
            }
            catch (Exception exception)
            {
                _error(exception);
            }
        }

        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                var request = (HttpWebRequest)asynchronousResult.AsyncState;
                var response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var resultString = streamReader.ReadToEnd();
                    HandleResponse(resultString);
                }
            }
            catch (Exception exception)
            {
                _error(exception);
            }
        }

        private void HandleResponse(string resultString)
        {
            var deserializeObject = JsonConvert.DeserializeObject<RootObject>(resultString);
            _success(deserializeObject.Response.Articles.Take(_take));
        }
    }


    public class Meta
    {
        public int Hits { get; set; }
        public int Time { get; set; }
        public int Offset { get; set; }
    }

    [JsonObject(Title = "Doc")]
    public class Article
    {
        public string Snippet { get; set; }
        [JsonProperty(PropertyName = "web_url")]
        public string WebUrl { get; set; }
        [JsonProperty(PropertyName = "pub_date")]
        public string PublicationDate { get; set; }
    }

    public class Response
    {
        public Meta Meta { get; set; }
        [JsonProperty(PropertyName = "docs")]
        public List<Article> Articles { get; set; }
    }

    public class RootObject
    {
        public Response Response { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
    }
}
