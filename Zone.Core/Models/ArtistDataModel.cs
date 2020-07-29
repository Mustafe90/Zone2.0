using System.Text.Json.Serialization;

namespace Zone.Core.Models
{
    public class ArtistDataModel
    {
        [JsonPropertyName("external_urls")]
        public ExternalUrlsDataModel ExternalUrlsDataModel { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }

    }
}
