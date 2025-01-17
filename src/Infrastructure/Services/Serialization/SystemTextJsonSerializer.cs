using System.Text.Json;
using SoftSquare.AlAhlyClub.Application.Common.Interfaces.Serialization;

namespace SoftSquare.AlAhlyClub.Infrastructure.Services.Serialization;

internal sealed class SystemTextJsonSerializer : ISerializer
{
    public string Serialize<T>(T value) where T : class
    {
        return JsonSerializer.Serialize(value, DefaultJsonSerializerOptions.Options);
    }

    public T? Deserialize<T>(string value) where T : class
    {
        return JsonSerializer.Deserialize<T>(value, DefaultJsonSerializerOptions.Options);
    }

    public byte[] SerializeBytes<T>(T value) where T : class
    {
        return JsonSerializer.SerializeToUtf8Bytes(value, DefaultJsonSerializerOptions.Options);
    }

    public T? DeserializeBytes<T>(byte[] value) where T : class
    {
        return JsonSerializer.Deserialize<T>(value, DefaultJsonSerializerOptions.Options);
    }
}