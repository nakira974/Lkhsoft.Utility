using Lkhsoft.Utility.Serialization;
using Microsoft.Extensions.Logging;

namespace Lkhsoft.Utility.WebServices;

public class HttpClientBase : IHttpClientBase
{
    private readonly ILogger<IHttpClientBase> _logger;
    private readonly IJsonSerializer _jsonSerializer;
    private HttpClient _httpClient;

    public HttpClientBase(ILogger<HttpClientBase> logger, IJsonSerializer jsonSerializer, HttpClient httpClient)
    {
        _logger = logger;
        _jsonSerializer = jsonSerializer;
        _httpClient = httpClient;
    }


    public async Task<HttpResponseMessage> GetEntity<T>(string entity, string uri, IDictionary<string, string>? additionalHeaders = null)
    {
        HttpResponseMessage result = null;
        try
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            if(!string.IsNullOrEmpty(entity)) 
                httpMessage.Content = new StringContent(entity);
            if(additionalHeaders is not null)
                foreach (var additionalHeader in additionalHeaders)
                    httpMessage.Headers.Add(additionalHeader.Key, additionalHeader.Value);
            result = await _httpClient.SendAsync(httpMessage);

        }
        catch (Exception e)
        {
            throw new HttpBaseRequestException("HTTP GET request message error", e);
        }

        return result;
    }

    public async Task<HttpResponseMessage> PostEntity(string entity, string uri, IDictionary<string, string>? additionalHeaders = null)
    {
        
        HttpResponseMessage result = null;
        try
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            if(!string.IsNullOrEmpty(entity)) 
                httpMessage.Content = new StringContent(entity);
            if(additionalHeaders is not null)
                foreach (var additionalHeader in additionalHeaders)
                    httpMessage.Headers.Add(additionalHeader.Key, additionalHeader.Value);
            result = await _httpClient.SendAsync(httpMessage);

        }
        catch (Exception e)
        {
            throw new HttpBaseRequestException("HTTP POST request message error", e);
        }

        return result;
        
    }

    public async Task<HttpResponseMessage> PostEntity(ByteArrayContent? byteArrayContent, string uri, IDictionary<string, string>? additionalHeaders = null)
    {
        HttpResponseMessage result = null;
        try
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            if (byteArrayContent is not null)
                httpMessage.Content = byteArrayContent;
            if(additionalHeaders is not null)
                foreach (var additionalHeader in additionalHeaders)
                    httpMessage.Headers.Add(additionalHeader.Key, additionalHeader.Value);
            result = await _httpClient.SendAsync(httpMessage);

        }
        catch (Exception e)
        {
            throw new HttpBaseRequestException("HTTP POST request message error", e);
        }

        return result;
        
    }

    public async Task<HttpResponseMessage> PatchEntity<T>(string fieldName, string fieldValue, string uri, IDictionary<string, string>? additionalHeaders = null)
    {
        HttpResponseMessage result = null;
        try
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Patch
                , uri);
            httpMessage.Content = new StringContent("{\n{"+$"{fieldName}:{fieldValue}"+"} \n}");
            if(additionalHeaders is not null)
                foreach (var additionalHeader in additionalHeaders)
                    httpMessage.Headers.Add(additionalHeader.Key, additionalHeader.Value);
            result = await _httpClient.SendAsync(httpMessage);

        }
        catch (Exception e)
        {
            throw new HttpBaseRequestException("HTTP POST request message error", e);
        }

        return result;
        
    }

    public async Task<HttpResponseMessage> PatchEntity<T>(ByteArrayContent? byteArrayContent, string uri, IDictionary<string, string>? additionalHeaders = null)
    {
        HttpResponseMessage result = null;
        try
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Patch, uri);
            if(byteArrayContent is not null) 
                httpMessage.Content = byteArrayContent;
            if(additionalHeaders is not null)
                foreach (var additionalHeader in additionalHeaders)
                    httpMessage.Headers.Add(additionalHeader.Key, additionalHeader.Value);
            result = await _httpClient.SendAsync(httpMessage);

        }
        catch (Exception e)
        {
            throw new HttpBaseRequestException("HTTP POST request message error", e);
        }

        return result;
    }

    public async Task<HttpResponseMessage> PutEntity(string entity, string uri, IDictionary<string, string>? additionalHeaders = null)
    {
        HttpResponseMessage result = null;
        try
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Put, uri);
            if(string.IsNullOrEmpty(entity)) 
                httpMessage.Content = new StringContent(entity);
            if(additionalHeaders is not null)
                foreach (var additionalHeader in additionalHeaders)
                    httpMessage.Headers.Add(additionalHeader.Key, additionalHeader.Value);
            result = await _httpClient.SendAsync(httpMessage);

        }
        catch (Exception e)
        {
            throw new HttpBaseRequestException("HTTP POST request message error", e);
        }

        return result;
        
    }

    public async Task<HttpResponseMessage> DeleteEntity(string entity, string uri, IDictionary<string, string>? additionalHeaders = null)
    {
        HttpResponseMessage result = null;
        try
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            if(string.IsNullOrEmpty(entity)) 
                httpMessage.Content = new StringContent(entity);
            if(additionalHeaders is not null)
                foreach (var additionalHeader in additionalHeaders)
                    httpMessage.Headers.Add(additionalHeader.Key, additionalHeader.Value);
            result = await _httpClient.SendAsync(httpMessage);

        }
        catch (Exception e)
        {
            throw new HttpBaseRequestException("HTTP POST request message error", e);
        }

        return result;
        
    }

    public async Task<HttpResponseMessage> DeleteEntity(ByteArrayContent? byteArrayContent, string uri, IDictionary<string, string>? additionalHeaders = null)
    {
        HttpResponseMessage result = null;
        try
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            if(byteArrayContent is not null) 
                httpMessage.Content = byteArrayContent;
            if(additionalHeaders is not null)
                foreach (var additionalHeader in additionalHeaders)
                    httpMessage.Headers.Add(additionalHeader.Key, additionalHeader.Value);
            result = await _httpClient.SendAsync(httpMessage);

        }
        catch (Exception e)
        {
            throw new HttpBaseRequestException("HTTP POST request message error", e);
        }

        return result;    
    }
}