using Newtonsoft.Json;

namespace RB444.Core.ServiceHelper
{
    public static class jsonParser
    {        
        public static T ParsJson<T>(this string _json)
        {            
            T odata = JsonConvert.DeserializeObject<T>(_json);
            return odata;
        }
    }
}
