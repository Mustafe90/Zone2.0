using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Zone.Core.Models
{
    public class TrackDataModel 
    {
        public List<ArtistDataModel> Artists { get; set; }

        [JsonPropertyName("available_markets")]

        public List<string> AvailableMarkets { get; set; }

        [JsonPropertyName("disc_number")]

        public int DiscNumber { get; set; }

        [JsonPropertyName("duration_ms")]

        public int DurationMs { get; set; }
        //Convert them to json = this on is explicit
        [JsonPropertyName("explicit")]
        public bool HasExplicitEnabled { get; set; }

        [JsonPropertyName("external_ids")]

        public ExternalIdsDataModel ExternalIdsDataModel { get; set; }

        [JsonPropertyName("external_urls")]
        public ExternalUrlsDataModel ExternalUrlsDataModel { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }

        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("preview_url")]

        public string PreviewUrl { get; set; }

        [JsonPropertyName("track_number")]

        public int TrackNumber { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }

    }
}
