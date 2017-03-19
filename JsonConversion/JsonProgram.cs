using System;
using ConsoleAppChallenge;
using Newtonsoft.Json;
using JsonConverter = ConsoleAppChallenge.JsonConverter;

namespace JsonConversion
{
    public class JsonProgram
    {
        public static void Main()
        {
            var json = Console.In.ReadToEnd();

            var v2 = JsonConvert.DeserializeObject<JsonVer2>(json);
            var converter = new JsonConverter();
            var v3 = converter.ConvertV2toV3(v2);

            Console.Write(JsonConvert.SerializeObject(v3));
        }
    }
}