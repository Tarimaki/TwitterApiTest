using System;
using System.Text.Json;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwitterTest
{
    class Program
    {
        private const string url = "https://api.twitter.com/2/tweets/search/recent?query=プログラミング&max_results=10";
        private const string bearer = "ここにTwitterApiのBearer Token";

        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(2);
            HttpRequestMessage reqMessage = new HttpRequestMessage(HttpMethod.Get, url);
            reqMessage.Headers.Add("Authorization", "Bearer " + bearer);

            var task = client.SendAsync(reqMessage);

            var response = task.Result;
            Console.WriteLine(response.StatusCode);

            string str = response.Content.ReadAsStringAsync().Result;

            var json = JsonSerializer.Deserialize<Datas>(str);

            foreach (var d in json.data)
            {
                Console.WriteLine(d.id + "    >    " + d.text);
            }

            reqMessage.Dispose();
            client.Dispose();
        }
    }

    public class Datas
    {
        [JsonPropertyName("data")]
        public List<Tweet> data { get; set; }
    }

    public class Tweet
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("text")]
        public string text { get; set; }
    }
}
