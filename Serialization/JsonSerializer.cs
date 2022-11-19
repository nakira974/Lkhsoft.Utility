// ReSharper disable ConvertToUsingDeclaration

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lkhsoft.Utility.Serialization;

/// <summary>
/// <inheritdoc cref="IJsonSerializer"/>
/// </summary>
public class JsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _options = new()
                                                      {
                                                          ReferenceHandler     = ReferenceHandler.Preserve,
                                                          WriteIndented        = true,
                                                          AllowTrailingCommas  = false,
                                                          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                                                      };

    async Task<T> IJsonSerializer.DeserializeAsync<T>(string str)
    {
        //how to return bool element
        try
        {
            var mStream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            var  result  = (await System.Text.Json.JsonSerializer.DeserializeAsync<T>(mStream, _options))!;
            await mStream.DisposeAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    T IJsonSerializer.Deserialize<T>(string str)
    {
        try
        {
            var byteSpan = new ReadOnlySpan<byte>(Encoding.UTF8.GetBytes(str));
            var mStream  = new MemoryStream();
            var result   = System.Text.Json.JsonSerializer.Deserialize<T>(byteSpan, _options)!;
            mStream.Dispose();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    string IJsonSerializer.Serialize<T>(T obj)
    {
        string result;
        try
        {
            result = System.Text.Json.JsonSerializer.Serialize(obj, _options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return result;
    }

    async Task<string> IJsonSerializer.SerializeAsync<T>(T obj)
    {
        string result;
        try
        {
            await using var stream = new MemoryStream();
            if (obj != null)
                await System.Text.Json.JsonSerializer.SerializeAsync(stream, obj, obj.GetType(), _options);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            result = await reader.ReadToEndAsync();
            await stream.DisposeAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return result;
    }

    void IJsonSerializer.CreateJsonFile(in string json, in string filePath)
    {
        try
        {
            using var sourceStream = File.Open(filePath, FileMode.OpenOrCreate);
            sourceStream.Dispose();
            using (var outputWriter = File.AppendText(filePath))
            {
                outputWriter.Write(json);
                outputWriter.Dispose();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreateJsonFile<T>(in T obj, in string filePath)
    {
        try
        {
            var       json         = ((IJsonSerializer) this).Serialize(obj);
            using var sourceStream = File.Create(filePath);
            var       content      = new UTF8Encoding(true).GetBytes(json);
            sourceStream.WriteAsync(content);
            sourceStream.DisposeAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task CreateJsonFileAsync<T>(T obj, string filePath)
    {
        try
        {
            var             json         = await ((IJsonSerializer) this).SerializeAsync(obj);
            await using var sourceStream = File.Create(filePath);
            var             content      = new UTF8Encoding(true).GetBytes(json);
            await sourceStream.WriteAsync(content);
            await sourceStream.DisposeAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task CreateJsonFileAsync(string json, string filePath)
    {
        try
        {
            await using var sourceStream = File.Open(filePath, FileMode.OpenOrCreate);
            await sourceStream.DisposeAsync();

            await using (var outputWriter = File.AppendText(filePath))
            {
                await outputWriter.WriteAsync(json);
                await outputWriter.DisposeAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class BoolConverter : JsonConverter<bool>
{
    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }

    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
               {
                   JsonTokenType.True  => true,
                   JsonTokenType.False => false,
                   JsonTokenType.String => bool.TryParse(reader.GetString(), out var b)
                                               ? b
                                               : throw new JsonException(),
                   JsonTokenType.Number => reader.TryGetInt64(out var l)  ? Convert.ToBoolean(l) :
                                           reader.TryGetDouble(out var d) && Convert.ToBoolean(d),
                   _ => throw new JsonException()
               };
    }
}