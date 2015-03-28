using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using fastJSON;

namespace fastJsonLight
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
            var pathname = @"..\..\testdata.json";
            var jsonString = "";
            using (var file = new StreamReader(pathname))
            {
                jsonString = file.ReadToEnd();
            }

            var map = fastJSON.JSON.Instance.ToObject<Storymap>(jsonString);

            foreach (KeyValuePair<string, string> name in map.Names)
            {
                Console.WriteLine(string.Format("{0}:{1}", name.Key, name.Value));
            }

            foreach (KeyValuePair<string, string> text in map.Texts)
            {
                Console.WriteLine(string.Format("{0}:{1}", text.Key, text.Value));
            }

        }
    }
}
