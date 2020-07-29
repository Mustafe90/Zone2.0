using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Zone.Core.Models
{
    public class RecentlyPlayedDataModel
    {
        [JsonPropertyName("items")]
        public List<RecentlyPlayedTrackDataModel> RecentlyPlayedTracks { get; set; }
        public string Next { get; set; }
        public CursorsDataModel Cursors { get; set; }
        public int Limit { get; set; }
        public string Href { get; set; }
    }
}
