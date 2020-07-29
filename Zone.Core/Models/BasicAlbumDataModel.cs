using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Zone.Core.Models
{
    public class BasicAlbumDataModel 
    {
        public  BasicAlbumDataModel()  { }

        [JsonPropertyName("album_type")]
        public string AlbumType { get; set; }

        public List<ArtistDataModel> Artists { get; set; }

        [JsonPropertyName("available_markets")]
        public List<string> AvailableMarkets { get; set; }

        [JsonPropertyName("external_urls")]

        public ExternalUrlsDataModel ExternalUrlsDataModel { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public List<ImageDataModel> Images { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [JsonPropertyName("total_tracks")]
        public int TotalTracks { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}
