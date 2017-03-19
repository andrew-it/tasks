using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace JsonConversion
{
    public static class JsonConverter
    {
        public static JObject ConvertV2toV3(JObject v2)
        {
            throw new NotImplementedException();
        }
    }


    [TestFixture]
    public class JsonConverter_Should
    {
        const string v2 = @"{
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

        const string v3 = @"{
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


        [TestCase(v2, v3)]
        public void ConvertV2toV3(string input, string expected)
        {
            var v2 = JObject.Parse(input);
            var expected_v3 = JObject.Parse(expected);

            var v3 = JsonConverter.ConvertV2toV3(v2);

            v3.Should().Equal(expected_v3);
        }
    }
}



