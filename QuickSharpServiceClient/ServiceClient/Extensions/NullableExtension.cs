using System.ComponentModel;

namespace Dell.Service.API.Client.Extensions
{
    public static class NullableExtension
    {
        public static T? ToNullable<T>(this string input) where T : struct
        {
            var result = new T?();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    var conv = TypeDescriptor.GetConverter(typeof (T));
                    var convertFrom = conv.ConvertFrom(input);
                    if (convertFrom != null) result = (T) convertFrom;
                }

            return result;
        }
    }

}
