using System;
using Newtonsoft.Json;

namespace Dell.Service.API.Client.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ObjectExtensions
    {
        public static string AsJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetJoinedValues(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            try
            {
                var dict = value.AsJson().FromJson<Dictionary<string, object>>();

                return string.Join(",", dict.Select(x => x.Value));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }



        /// <summary>
        /// Convert object from json using .net native libraries
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TObject FromJson<TObject>(this string data)
        {
            return JsonConvert.DeserializeObject<TObject>(data);
        }
    }
}