using System;
using System.Runtime.Serialization;

[Serializable]
internal class MaterialNotFoundException : Exception
{
    private IndexOutOfRangeException exception;

    public MaterialNotFoundException()
    {
    }

    public MaterialNotFoundException(string message) : base(message)
    {
    }

    public MaterialNotFoundException(IndexOutOfRangeException exception)
    {
        this.exception = exception;
    }

    public MaterialNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected MaterialNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}