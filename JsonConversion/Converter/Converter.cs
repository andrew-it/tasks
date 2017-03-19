using EvalTask;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

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
            var evaluator = new Evaluator();

            var count = 0;

            foreach (var productsKey in v2.products.Keys)
            {
                double price;
                if (!double.TryParse(v2.products[productsKey].price, out price))
                    price = double.Parse(evaluator.EvalStringWithVars(v2.products[productsKey].price, v2.constants));

                jsonV3.products[count] = new ProductWithId
                {
                    id = int.Parse(productsKey),
                    name = v2.products[productsKey].name,
                    price = price,
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
        private const string v2_01 = @"{
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

        private const string v3_01 = @"{
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


        [TestCase(v2_01, v3_01)]
        [TestCase(v2_02, v3_02)]
        public void ConvertV2toV3(string v2, string v3)
        {
            var expected = JsonConvert.DeserializeObject<JsonVer3>(v3);
            var version2 = JsonConvert.DeserializeObject<JsonVer2>(v2);

            var converter = new JsonConverter();
            var resolut = converter.ConvertV2toV3(version2);

            JsonConvert.SerializeObject(resolut).Should().Be(JsonConvert.SerializeObject(expected));
        }
    }
}