using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting;
using System.Text;

namespace OsuBeatmapDownloader.Scripts
{
    internal static class ConfigManager
    {
        private static readonly string Filename = "config.json";

        public static void FirstInitialize()
        {
            if (!File.Exists("config.json"))
            {
                File.Create("config.json").Dispose();

                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);

                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject();
                    writer.WritePropertyName("Downloaded");
                    writer.WriteStartArray();
                    writer.WriteEndArray();

                    writer.WritePropertyName("OsuPath");

                    writer.WriteEndObject();
                }
                using (StreamWriter file = File.CreateText(Filename))
                {
                    JObject JOBe = JObject.Parse(sb.ToString());
                    JsonSerializer serializer = new JsonSerializer{Formatting = Formatting.Indented};
                    serializer.Serialize(file, JOBe);
                    file.Dispose();
                }
            }
        }

        public static bool HavePath()
        {
            string json = File.ReadAllText(Filename);
            dynamic jsonobj = JsonConvert.DeserializeObject(json);
            if (jsonobj.OsuPath != null)
                return true;
            else
                return false;
        }
        public static string GetPath()
        {
            string json = File.ReadAllText(Filename);
            dynamic jsonobj = JsonConvert.DeserializeObject(json);
            return jsonobj.OsuPath;
        }

        public static void Save(string path)
        {
            string json = File.ReadAllText(Filename);
            dynamic jsonobj = JsonConvert.DeserializeObject(json);

            jsonobj.OsuPath = path;
            string output = JsonConvert.SerializeObject(jsonobj, Formatting.Indented);
            File.WriteAllText(Filename, output);
        }

        public static void SaveParentSetId(int ParentSetId)
        {
            string json = File.ReadAllText(Filename);
            dynamic jsonobj = JsonConvert.DeserializeObject(json);

            JArray Downloaded = new JArray();
            foreach (int downloads in jsonobj.Downloaded)
                Downloaded.Add(downloads);
            Downloaded.Add(ParentSetId);
            jsonobj.Downloaded = Downloaded;
            string output = JsonConvert.SerializeObject(jsonobj, Formatting.Indented);
            File.WriteAllText(Filename, output);
        }

        public static List<int> GetBeatmaps()
        {
            List<int> list = new List<int>();

            string json = File.ReadAllText(Filename);
            dynamic jsonobj = JsonConvert.DeserializeObject(json);

            foreach(int value in jsonobj.Downloaded)
                list.Add(value);
            return list;
        }
    }
}
