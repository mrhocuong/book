using System.Collections.Generic;
using System.IO;
using Books.Models;
using Newtonsoft.Json;

namespace Books
{
    public static class SeedDataHelper
    {
        public static List<BookSeedModel> GetSampleData()
        {
            string jsonFilePath = @"book-data.json";
            string json = File.ReadAllText(jsonFilePath);
            var myJsonObject = JsonConvert.DeserializeObject<List<BookSeedModel>>(json);
            return myJsonObject;
        }
    }
}