using System.Text.Json.Serialization;

namespace Zone.Core.Models
{
    
    public class ContextDataModel
    {
        [JsonPropertyName("external_urls")]

        public ExternalUrlsDataModel ExternalUrlsDataModel { get; set; }
        public string Href { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}
