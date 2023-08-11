using Newtonsoft.Json.Linq;

namespace BianCore.Tools
{
    public class Json
    {
        public static JObject ToJson(string data)
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
