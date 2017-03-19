﻿using System;
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
            return queries.Select(q => $"{q} = {GetResultByQuery(q, data)}");
        }

        public static string GetResultByQuery(string query, JObject data)
        {
            var steps = query.Split('.');
            var result = data[steps[0]];
            for (var i = 1; i < steps.Length; i++)
            {
                try
                {
                    result = result[steps[i]];
                }
                catch (Exception e)
                {
                    return null;
                }
                
            }
            return result?.ToString(Formatting.Indented);
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

        private const string jsonData2 =
            @"{""data"":{""empty"":{},""ab"":0,""x1"":1,""x2"":2,""y1"":{""y2"":{""y3"":3}}},
                ""queries"":[""empty"",""xyz"",""x1.x2"",""y1.y2.z"",""empty.foobar""]}";

        [TestCase(jsonData, new[] {"a.b.c = 15", "z = 42", "a.x = 3.14"}, TestName = "good")]
        [TestCase(jsonData2, new[] {"empty = ", "xyz = null", "x1.x2 = null", "y1.y2.z = null", "empty.foobar = null" }, TestName = "bad")]
        public void GetValue_ByQuery(string query, IEnumerable<string> result)
        {
            SimQLProgram.ExecuteQueries(query)
                .ShouldBeEquivalentTo(result);
        }

        [TestCase("empty", "{'empty':{},'ab':0,'x1':1,'x2':2,'y1':{'y2':{'y3':3}}}", ExpectedResult = "{}")]
        [TestCase("xyz", "{'empty':{},'ab':0,'x1':1,'x2':2,'y1':{'y2':{'y3':3}}}", ExpectedResult = null)]
        [TestCase("y1.y2.z", "{'empty':{},'ab':0,'x1':1,'x2':2,'y1':{'y2':{'y3':3}}}", ExpectedResult = null)]
        [TestCase("empty.foobar", "{'empty':{},'ab':0,'x1':1,'x2':2,'y1':{'y2':{'y3':3}}}", ExpectedResult = null)]
        public string GetValue_ByErrorQuery(string query, string data)
        {
            var d = JObject.Parse(data);
            return SimQLProgram.GetResultByQuery(query, d);
        }

        [TestCase("sum(a.b.c)", "{'a':{'x':3.14, 'b':[{'c':15}, {'c':9}]}, 'z':[2.65, 35]}", ExpectedResult = 24) ]
        public void GetValue_ByAgregateQuery(string query, string data)
        {
            var d = JObject.Parse(data);
            
        }
    }
}