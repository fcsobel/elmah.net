using System;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace Elmah.Net
{
	public static class SerializationHelper
	{
		public static string ToJson(this object obj, bool ignoreDefaultAndNullValues = false)
		{
			if (ignoreDefaultAndNullValues)
			{
				return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None,
					new JsonSerializerSettings() {
						NullValueHandling = NullValueHandling.Ignore, 
						DefaultValueHandling = DefaultValueHandling.Ignore }
					);
			}
			else
			{
				return JsonConvert.SerializeObject(obj);
			}
		}

        public static T FromJson<T>(this string json, bool silent = false) where T : new()
        {
            if (string.IsNullOrWhiteSpace(json)) { return new T(); }

            if (silent)
            {
                try
                {
                    var item = JsonConvert.DeserializeObject<T>(json);
                    return item != null ? item : new T();
                }
                catch
                {
                    return new T();
                }
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
	}
}