using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

public class HttpClientWrapper
{
    private readonly HttpClient _httpClient;

    public HttpClientWrapper()
    {
        _httpClient = new HttpClient();
    }

    public (bool success, Dictionary<string, object> data) post(string url, IDictionary<string, string> headers, string data)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        foreach (var header in headers)
        {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        request.Content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = _httpClient.SendAsync(request).Result;

        if (response.IsSuccessStatusCode)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return (true, JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent));
        }

        return (false, new Dictionary<string, object> { { "error", $"Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}" } });
    }
}