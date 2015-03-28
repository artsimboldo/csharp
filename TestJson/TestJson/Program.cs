using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;

using Newtonsoft.Json;
using LitJson;
using ServiceStack;
using MongoDB.Bson;
using fastJSON;

namespace TestJson
{

    public class Storymap
    {
        public string Title { get; set; }
        public string Script { get; set; }
        public Dictionary<string, string> Names { get; set; }
        public Dictionary<string, string> Texts { get; set; }

        public Storymap()
        {
            Names = new Dictionary<string, string>();
            Texts = new Dictionary<string, string>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string pathname = @"..\..\testdata.json";
            string jsonString = "";
            var bench = new Dictionary<string, long>();
            var maxiter = 1000;
            var stopwatch = new Stopwatch();

            using (StreamReader file = new StreamReader(pathname))
            {
                jsonString = file.ReadToEnd();
            }

            // ----------------------------------------------------------------------
            // JSON.NET
            stopwatch.Start();
            for (var i = 0; i < maxiter; i++)
            {
                var map = JsonConvert.DeserializeObject<Storymap>(jsonString);
            }
            stopwatch.Stop();
            bench.Add("JSON.NET", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();
//            Thread.Sleep(100);

            // ----------------------------------------------------------------------
            // LitJson
            stopwatch.Start();
            for (var i = 0; i < maxiter; i++)
            {
                var map = JsonMapper.ToObject<Storymap>(jsonString);
            }
            stopwatch.Stop();
            bench.Add("LitJson", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            // ----------------------------------------------------------------------
            // .NET ServiceStack
            stopwatch.Start();
            for (var i = 0; i < maxiter; i++)
            {
                var map = ServiceStack.Text.JsonSerializer.DeserializeFromString<Storymap>(jsonString);
            }
            stopwatch.Stop();
            bench.Add("ServiceStack", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            // ----------------------------------------------------------------------
            // MongoDB Bson
            stopwatch.Start();
            for (var i = 0; i < maxiter; i++)
            {
                var map = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<Storymap>(jsonString);
            }
            stopwatch.Stop();
            bench.Add("MongoDB Bson", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            // ----------------------------------------------------------------------
            // fastJSON
            stopwatch.Start();
            for (var i = 0; i < maxiter; i++)
            {
                var map = fastJSON.JSON.Instance.ToObject<Storymap>(jsonString);
            }
            stopwatch.Stop();
            bench.Add("fastJSON", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            // ----------------------------------------------------------------------
            // Display results
            // ----------------------------------------------------------------------
            var count = 0;
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("| Rank |  Serializer  | Elapsed time |");
            Console.WriteLine("|------+--------------+--------------|");
            foreach (KeyValuePair<string, long> result in bench.OrderBy(key => key.Value))
            {
                Console.WriteLine("| {0,4} | {1,12} | {2,1}s {3,3}ms     |", ++count, result.Key, result.Value / 1000, result.Value % 1000);
            }
            Console.WriteLine("--------------------------------------");
        }
    }
}
