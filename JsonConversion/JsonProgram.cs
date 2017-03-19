﻿using Newtonsoft.Json.Linq;
using System;
using ConsoleAppChallenge;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using JsonConverter = ConsoleAppChallenge.JsonConverter;

namespace JsonConversion
{
	class JsonProgram
	{
		static void Main()
		{
			string json = Console.In.ReadToEnd();

		    var v2 = JsonConvert.DeserializeObject<JsonVer2>(json);

		    var converter = new JsonConverter();
		    var v3 = converter.ConvertV2toV3(v2);

			Console.Write(JsonConvert.SerializeObject(v3));
		}
	}
}
