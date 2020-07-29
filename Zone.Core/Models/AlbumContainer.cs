using System;
using System.Text.Json.Serialization;

namespace Zone.Core.Models
{
    public class AlbumContainer
    {
        [JsonPropertyName("added_at")]
        public DateTime AddedAt { get; set; }
        public AlbumDataModel Album { get; set; }
    }
}
