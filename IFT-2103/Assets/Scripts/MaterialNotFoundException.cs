using System;
using System.Runtime.Serialization;

[Serializable]
internal class MaterialNotFoundException : Exception
{
    public MaterialNotFoundException()
    {
    }

    public MaterialNotFoundException(string message) : base(message)
    {
    }

    public MaterialNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected MaterialNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}