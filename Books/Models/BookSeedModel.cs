using System;
using Newtonsoft.Json;

namespace Books.Models
{
    public class BookSeedModel
    {
        [JsonProperty("author")] public string Author { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        [JsonProperty("imageLink")] public string ImageLink { get; set; }

        [JsonProperty("language")] public string Language { get; set; }

        [JsonProperty("link")] public Uri Link { get; set; }

        [JsonProperty("pages")] public long Pages { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("year")] public long Year { get; set; }
    }
}