using System.Text;

namespace RentACarNow.Common.Infrastructure.Extensions
{
    public static class ByteConverterExtensions
    {

        public static byte[] ConvertToByteArray(this string @object)
            => Encoding.UTF8.GetBytes(@object);


        public static string ConvertToString(this byte[] @object)
          => Encoding.UTF8.GetString(@object);

        public static string ConvertToString(this ReadOnlySpan<byte> @object)
         => Encoding.UTF8.GetString(@object);






    }
}
