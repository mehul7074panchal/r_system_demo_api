using System;
using System.Net;
using Newtonsoft.Json;

namespace rs.Helper
{
	public class HttpHelerClient : IHttpHelerClient
    {
        private static readonly string baseURl = "https://hacker-news.firebaseio.com/v0/";
        public static readonly string case_key_stories = "stories";
        private readonly HttpClient _httpClient;

        public HttpHelerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public async Task<T?> HttpGetRequest<T>(string url)
        {
            var response = await _httpClient.GetAsync($"{baseURl}/{url}.json");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");

            }
        }

    }
}

