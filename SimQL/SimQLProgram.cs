using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


namespace SimQLTask
{
    internal class SimQLProgram
    {
        private static void Main(string[] args)
        {
            var json = Console.In.ReadToEnd();
            foreach (var result in ExecuteQueries(json))
                Console.WriteLine(result);
        }

        public static IEnumerable<string> ExecuteQueries(string json)
        {
            var jObject = JObject.Parse(json);
            var data = (JObject) jObject["data"];
            var queries = jObject["queries"].ToObject<string[]>();
            return queries.Select(q => GetResultByQuery(q, data));
        }

        public static string GetResultByQuery(string query, JObject data)
        {
            var steps = query.Split('.');
            var result = data[steps[0]];
            for (int i = 1; i < steps.Length; i++)
            {
                result = result[steps[i]];
            }
            return $"{query} = {result.ToString(Formatting.Indented)}";
        }
    }

    [TestFixture]
    public class SimQLTester
    {
        private const string jsonData = @"{
                                                'data': {
                                                    'a': {
                                                        'x':3.14, 
                                                        'b': {'c':15}, 
                                                        'c': {'c':9}
                                                    }, 
                                                    'z':42
                                                },
                                                'queries': [
                                                    'a.b.c',
                                                    'z',
                                                    'a.x'
                                                ]
                                            }";

        [TestCase(jsonData, new[] {"a.b.c = 15", "z = 42", "a.x = 3.14"})]
        public void GetValue_ByQuery(string query, IEnumerable<string> result)
        {
            SimQLProgram.ExecuteQueries(query)
                .ShouldBeEquivalentTo(result);
        }

        [Test]
        public void T()
        {
            var input = @"{'data': {a:1}, 'queries' : ['b']}";
            var actual = SimQLProgram.ExecuteQueries(input);

        }
    }
}