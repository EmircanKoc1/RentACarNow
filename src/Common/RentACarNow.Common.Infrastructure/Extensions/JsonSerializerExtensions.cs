using System.Text.Json;

namespace RentACarNow.Common.Infrastructure.Extensions
{
    public static class JsonSerializerExtensions
    {

        public static string? Serialize<T>(this T @object)
            => JsonSerializer.Serialize(@object);


        public static T? Deseralize<T>(this string @object)
            => JsonSerializer.Deserialize<T>(@object);

    }
}
