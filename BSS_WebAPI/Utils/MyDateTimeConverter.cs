using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BSS_WebAPI.Utils
{
    public class MyDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            DateTime dt = DateTime.MinValue;
            if (reader.Value.ToString().Length == 10)
            {
               DateTime.TryParseExact(reader.Value.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt);
            }
            else if (reader.Value.ToString().Length == 23)
            {
                DateTime.TryParseExact(reader.Value.ToString(), "dd/MM/yyyy HH:mm:ss.fff", null, System.Globalization.DateTimeStyles.None, out dt);
            }
        else
            {
                throw new Exception("Invalid date format!");
            }
            return dt;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime) value).ToString("dd/MM/yyyy hh:mm:ss tt"));
        }
    }
}