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
            var jsonV3 = new JsonVer3();
            jsonV3.products = new ProductWithId[v2.products.Count];

            int count = 0;
            foreach (var productsKey in v2.products.Keys)
            {
                jsonV3.products[count] = new ProductWithId
                {
                    id = int.Parse(productsKey),
                    name = v2.products[productsKey].name,
                    price = v2.products[productsKey].price,
                    count = v2.products[productsKey].count
                };
                count++;
            }

            return jsonV3;
        }
    }


    [TestFixture]
    public class JsonConverter_Should
    {

        [Test]
        public void ConvertV2toV3()
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


            var expected = JsonConvert.DeserializeObject<JsonVer3>(v3);
            var version2 = JsonConvert.DeserializeObject<JsonVer2>(v2);

 
            var converter = new JsonConverter();
            var resolut = converter.ConvertV2toV3(version2);

            JsonConvert.SerializeObject(resolut).Should().Be(JsonConvert.SerializeObject(expected));
        }
    }
}



