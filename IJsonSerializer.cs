namespace Lkhsoft.Utility;

using System.Threading.Tasks;

/// <summary>
/// Provide an access to JSON serialization/deserialization service
/// </summary>
public interface IJsonSerializer
{
    /// <summary>
    /// Deserialize a JSON string object to an instance of a T object asynchronously
    /// </summary>
    /// <param name="str">Serialized object</param>
    /// <typeparam name="T">Target type</typeparam>
    /// <returns>An instance of the deserialized object</returns>
    public Task<T> DeserializeAsync<T>(string str);

    /// <summary>
    /// Deserialize a JSON string object to an instance of a T object
    /// </summary>
    /// <param name="str">Serialized object</param>
    /// <typeparam name="T">Target type</typeparam>
    /// <returns>An instance of the deserialized object</returns>
    public T Deserialize<T>(string str);

    /// <summary>
    /// Serialize as JSON string object, an object of T type
    /// </summary>
    /// <param name="obj">Object to serialize</param>
    /// <typeparam name="T">Object's type</typeparam>
    /// <returns>A serialized json object as string</returns>
    public string Serialize<T>(T obj);

    /// <summary>
    /// Serialize as JSON string object, an object of T type asynchronously
    /// </summary>
    /// <param name="obj">Object to serialize</param>
    /// <typeparam name="T">Object's type</typeparam>
    /// <returns>A serialized json object as string</returns>
    public Task<string> SerializeAsync<T>(T obj);

    public void CreateJsonFile(in string json, in string filePath);

    /// <summary>
    /// Create a JSON file from an input object at a specified path
    /// </summary>
    /// <param name="obj">Object to serialize and write into a file</param>
    /// <param name="filePath">Path where to save the file</param>
    /// <typeparam name="T">Object type</typeparam>
    public void CreateJsonFile<T>(in T obj, in string filePath);

    /// <summary>
    /// Create a JSON file from an input object at a specified path asynchronously
    /// </summary>
    /// <param name="obj">Object to serialize and write into a file</param>
    /// <param name="filePath">Path where to save the file</param>
    /// <typeparam name="T">Object type</typeparam>
    public Task CreateJsonFileAsync<T>(T obj, string filePath);

    /// <summary>
    /// Create a JSON file from an input object at a specified path asynchronously
    /// </summary>
    /// <param name="json">JSON string object to write into a file</param>
    /// <param name="filePath">Path where to save the file</param>
    public Task CreateJsonFileAsync(string json, string filePath);
}