using System.Text.Json.Serialization;

namespace Zone.Core.Models
{
    public class CurrentlyPlayingDataModel
    {
        public long Timestamp { get; set; }

        [JsonPropertyName("context")]

        public ContextDataModel ContextDataModel { get; set; }

        [JsonPropertyName("progress_ms")]

        public int ProgressMs { get; set; }
        [JsonPropertyName("item")]
        public UserTrackDataModelDataModel Track { get; set; }

        [JsonPropertyName("currently_playing_type")]
        public string CurrentlyPlayingType { get; set; }

        [JsonPropertyName("actions")]
        public ActionsDataModel Actions { get; set; }

        [JsonPropertyName("is_playing")]
        public bool IsPlaying { get; set; }
    }
}
