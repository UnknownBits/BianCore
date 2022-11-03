using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Tools
{
    public class Json
    {
        public static JObject Str_to_Json(string data)
        { 
            try
            {
                return JObject.Parse(data);
            }
            catch
            {
                return null;
            }
        }
    }
}
