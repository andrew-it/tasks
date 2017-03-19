using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace ConsoleAppChallenge
{
    public interface IJsonConverter
    {
        JsonVer3 ConvertV2toV3(JsonVer2 v2);
    }



    public class JsonConverter : IJsonConverter
    {
        public JsonVer3 ConvertV2toV3(JsonVer2 v2)
        {
            throw new NotImplementedException();
        }
    }


    [TestFixture]
    public class JsonConverter_Should
    {

        const string v2_01 = @"{
                'version': '2',
                'products': {
                    '1': {
                        'name': 'Pen',
                        'price': 12,
                        'count': 100
                        },
                    '2': {
                        'name': 'Pencil',
                        'price': 8,
                        'count': 1000
                        }
                }
            }";

        const string v3_01 = @"{
	            'version': '3',
                'products': [
                {
			        'id': 1,
			        'name': 'Pen',
			        'price': 12,
			        'count': 100
                },
		        {
			        'id': 2,
			        'name': 'Pencil',
			        'price': 8,
			        'count': 1000
		        }
	            ]
             }";

        private const string v2_02 = @"{
            'version': '2',
            'constants': { 'a': 3, 'b': 4 },
            'products':
            {
                '1': {
                    'name': 'product-name',
                    'price': '3 + a + b',
                    'count': 100,
                }
            }
        }";

        private const string v3_02 = @"{
            'version': '3',
            'products': [
            {
                'id': 1,
                'name': 'product-name',
                'price': 10,
                'count': 100
            }
            ]
        }";


        [TestCase(v2_01, v2_01)]
        [TestCase(v2_02, v2_02)]
        public void ConvertV2toV3(string v2, string v3)
        {
            var expected = JsonConvert.DeserializeObject<JsonVer3>(v3);
            var version2 = JsonConvert.DeserializeObject<JsonVer2>(v2);

            var converter = new JsonConverter();
            var resolut = converter.ConvertV2toV3(version2);

            resolut.Should().Be(expected);
        }

    }
}



