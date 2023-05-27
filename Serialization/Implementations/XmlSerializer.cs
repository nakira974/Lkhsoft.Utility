using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lkhsoft.Utility.Serialization.Implementations;

/// <summary>
/// <inheritdoc cref="IXmlSerializer"/>
/// </summary>
public class XmlSerializer : IXmlSerializer
{
    private readonly XmlSerializerNamespaces _xmlNamespace;

      public XmlSerializer(IEnumerable<KeyValuePair<string, string>>? namespaces = null)
      {
          _xmlNamespace = new XmlSerializerNamespaces();
          if (namespaces == null) return;
          foreach (var ns in namespaces)
          {
              _xmlNamespace.Add(ns.Key, ns.Value);
          }
      }

      
      public T Deserialize<T>(string str)
      {
          try
          {
              using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(str));
              var       serializer   = new System.Xml.Serialization.XmlSerializer(typeof(T));

              return (T)serializer.Deserialize(memoryStream)! ?? throw new InvalidOperationException();
          }
          catch (Exception ex)
          {
              throw new SerializerException(SerializerType.Xml, ex);
          }
      }

      public async Task<T> DeserializeAsync<T>(string str)
      {
          try
          {
              using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(str));
              var       serializer   = new System.Xml.Serialization.XmlSerializer(typeof(T));

              return await Task.FromResult((T)serializer.Deserialize(memoryStream)!) ?? throw new InvalidOperationException();
          }
          catch (Exception ex)
          {
              throw new SerializerException(SerializerType.Xml, ex);
          }
      }

      public string Serialize<T>(T obj)
      {
          try
          {
              using var memoryStream = new MemoryStream();
              var       serializer   = new System.Xml.Serialization.XmlSerializer(typeof(T));

              serializer.Serialize(memoryStream, obj, _xmlNamespace);

              return Encoding.UTF8.GetString(memoryStream.GetBuffer());
          }
          catch (Exception ex)
          {
              throw new SerializerException(SerializerType.Xml, ex);
          }
      }

      public async Task<string> SerializeAsync<T>(T obj)
      {
          try
          {
              using var memoryStream = new MemoryStream();
              var       serializer   = new System.Xml.Serialization.XmlSerializer(typeof(T));

              serializer.Serialize(memoryStream, obj, _xmlNamespace);

              return await Task.FromResult(Encoding.UTF8.GetString(memoryStream.GetBuffer()));
          }
          catch (Exception ex)
          {
              throw new SerializerException(SerializerType.Xml, ex);
          }
      }

      public void Save<T>(in T obj, in string filePath)
        {
            try
            {
                using var fileStream     = new FileStream(filePath, FileMode.Create);
                using var bufferedStream = new BufferedStream(fileStream);

                var xml = Serialize(obj);
                bufferedStream.Write(Encoding.UTF8.GetBytes(xml));
            }
            catch (Exception ex)
            {
                throw new SerializerException(SerializerType.Xml, ex);
            }
        }

        public async Task SaveAsync<T>(T obj, string filepath)
        {
            try
            {
                await using var fileStream     = new FileStream(filepath, FileMode.Create);
                await using var bufferedStream = new BufferedStream(fileStream);

                var xml = await SerializeAsync(obj);
                await bufferedStream.WriteAsync(Encoding.UTF8.GetBytes(xml));
            }
            catch (Exception ex)
            {
                throw new SerializerException(SerializerType.Xml, ex);
            }
        }

        public T Load<T>(string filepath)
        {
            try
            {
                using var fileStream     = new FileStream(filepath, FileMode.Open);
                using var bufferedStream = new BufferedStream(fileStream);

                var buffer    = new byte[fileStream.Length];
                var bytesRead = bufferedStream.Read(buffer, 0, (int)fileStream.Length);

                var xml = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                return Deserialize<T>(xml);
            }
            catch (Exception ex)
            {
                throw new SerializerException(SerializerType.Xml, ex);
            }
        }

        public async Task<T> LoadAsync<T>(string filepath)
        {
            try
            {
                await using var fileStream     = new FileStream(filepath, FileMode.Open);
                await using var bufferedStream = new BufferedStream(fileStream);

                var buffer    = new byte[fileStream.Length];
                var bytesRead = await bufferedStream.ReadAsync(buffer.AsMemory(0, buffer.Length));
                
                var xml = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                return await DeserializeAsync<T>(xml);
            }
            catch (Exception ex)
            {
                throw new SerializerException(SerializerType.Xml, ex);
            }
            
        }
}
