using System.Collections;

namespace Lkhsoft.Utility.Serialization;

public class SerializerException : Exception
{
    public SerializerException(SerializerType serializerSerializationType, Exception source)
    {
        Message           = source.Message;
        Source            = source.Source;
        Data              = source.Data;
        HelpLink          = source.HelpLink;
        StackTrace        = source.StackTrace;
        SerializationType = serializerSerializationType;
        Type             = source.GetType();
    }

    public sealed override IDictionary    Data              { get; }
    public sealed override string?        HelpLink          { get; set; }
    public sealed override string         Message           { get; }
    public sealed override string?        Source            { get; set; }
    public sealed override string?        StackTrace        { get; }
    public                 SerializerType SerializationType { get; init; }
    public                 Type           @Type             { get; init; }
}