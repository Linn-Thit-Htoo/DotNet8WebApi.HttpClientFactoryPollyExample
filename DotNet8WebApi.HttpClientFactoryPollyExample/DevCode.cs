using Newtonsoft.Json;

namespace DotNet8WebApi.HttpClientFactoryPollyExample
{
    public static class DevCode
    {
        public static T DeserializeObject<T>(this string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr)!;
        }
    }
}
