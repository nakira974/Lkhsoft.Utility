namespace Lkhsoft.Utility.Serialization;

/// <summary>
/// Provides an interface for serialization and deserialization of objects to different formats.
/// </summary>
public interface ISerializer
{
    /// <summary>
    /// Deserializes a string representation of an object to the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized object.</typeparam>
    /// <param name="str">The string representation of the serialized object.</param>
    /// <returns>The deserialized object.</returns>
    T Deserialize<T>(string str);

    /// <summary>
    /// Asynchronously deserializes a string representation of an object to the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized object.</typeparam>
    /// <param name="str">The string representation of the serialized object.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the deserialized object.</returns>
    Task<T> DeserializeAsync<T>(string str);

    /// <summary>
    /// Serializes an object to a string representation using a specific format.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to be serialized.</param>
    /// <returns>The string representation of the serialized object.</returns>
    string Serialize<T>(T obj);

    /// <summary>
    /// Asynchronously serializes an object to a string representation using a specific format.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to be serialized.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the string representation of the serialized object.</returns>
    Task<string> SerializeAsync<T>(T obj);

    /// <summary>
    /// Saves a serialized object to a file on the filesystem.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize and save.</typeparam>
    /// <param name="obj">The object to be serialized and saved.</param>
    /// <param name="filePath">The path of the file to save the serialized object to.</param>
    void Save<T>(in T obj, in string filePath);

    /// <summary>
    /// Asynchronously saves a serialized object to a file on the filesystem.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize and save.</typeparam>
    /// <param name="obj">The object to be serialized and saved.</param>
    /// <param name="filepath">The path of the file to save the serialized object to.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveAsync<T>(T obj, string filepath);

    /// <summary>
    /// Loads a serialized object from a file on the filesystem and returns it as an instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized object.</typeparam>
    /// <param name="filepath">The path of the file containing the serialized object.</param>
    /// <returns>The deserialized object.</returns>
    T Load<T>(string filepath);

    /// <summary>
    /// Asynchronously loads a serialized object from a file on the filesystem and returns it as an instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the deserialized object.</typeparam>
    /// <param name="filepath">The path of the file containing the serialized object.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the deserialized object.</returns>
    Task<T> LoadAsync<T>(string filepath);
}