using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BianCore.Tools;

public class HttpUtils : IDisposable
{
    private readonly HttpClient HttpClient;
    private bool disposedValue;

    public HttpUtils()
    {
        HttpClient = new HttpClient();
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
    }

    public async Task<HttpResponseMessage> HttpGetAsync(string url, string content_type = "application/json", Dictionary<string, string> headers = null)
    {
        using HttpRequestMessage message = new(HttpMethod.Get, url) { Content = new StringContent("") { Headers = { ContentType = new MediaTypeHeaderValue(content_type) } } };
        if (headers != null) foreach (var pair in headers) message.Headers.Add(pair.Key, pair.Value);
        return await HttpClient.SendAsync(message);
    }

    public async Task<HttpResponseMessage> HttpPostAsync(string url, HttpContent content, Dictionary<string, string> headers = null)
    {
        using var message = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
        if (headers != null) foreach (var pair in headers) message.Headers.Add(pair.Key, pair.Value);
        return await HttpClient.SendAsync(message);
    }

    public async Task<HttpResponseMessage> HttpPostAsync(string url, string content, string content_type = "application/json", Dictionary<string, string> headers = null)
        => await HttpPostAsync(url, new StringContent(content) { Headers = { ContentType = new MediaTypeHeaderValue(content_type) } }, headers);

    public HttpResponseMessage HttpGet(string url, string content_type = "application/json", Dictionary<string, string> headers = null)
        => HttpGetAsync(url, content_type, headers).Result;

    public HttpResponseMessage HttpPost(string url, HttpContent content, Dictionary<string, string> headers = null)
        => HttpPostAsync(url, content, headers).Result;

    public HttpResponseMessage HttpPost(string url, string content, string content_type = "application/json", Dictionary<string, string> headers = null)
        => HttpPostAsync(url, content, content_type, headers).Result;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing) HttpClient?.Dispose();
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

public static class HttpExtension
{
    public static string ReadAsString(this HttpContent content)
    {
        using var stream = content.ReadAsStreamAsync().Result;
        using var streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }
}