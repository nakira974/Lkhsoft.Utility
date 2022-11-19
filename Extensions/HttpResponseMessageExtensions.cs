using System.Web.Mvc;
using Lkhsoft.Utility.Serialization;
using Microsoft.Extensions.Logging;
// ReSharper disable InconsistentNaming
#pragma warning disable CS0649

namespace Lkhsoft.Utility.Extensions;

public static class HttpResponseMessageExtensions
{
    private static IJsonSerializer _jsonSerializer =>
        // DependencyResolver could be any DI container here.
        DependencyResolver.Current.GetService<IJsonSerializer>();

    public static async Task<T> DeserializeEntityAsync<T>(this HttpResponseMessage responseMessage, IJsonSerializer jsonSerializer)
    {
        
        T? result = default;
        try
        {
            result = await _jsonSerializer.DeserializeAsync<T>(responseMessage.Content.ToString() ??
                                                               throw new InvalidOperationException());
        }
        catch (Exception e)
        {
            throw new Exception("Deserialization error", e);
        }

        return result;
    }
    
    public static T DeserializeEntity<T>(this HttpResponseMessage responseMessage)
    {
        T? result = default;
        try
        {
            result =  _jsonSerializer.Deserialize<T>(responseMessage.Content.ToString() ??
                                                               throw new InvalidOperationException());
        }
        catch (Exception e)
        {
            throw new Exception("Deserialization error", e);
        }

        return result;
    }
}