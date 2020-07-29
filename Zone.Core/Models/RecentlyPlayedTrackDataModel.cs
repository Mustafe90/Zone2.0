using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Zone.Core.Models
{
    public class RecentlyPlayedTrackDataModel
    {
        public UserTrackDataModelDataModel Track { get; set; }
        [JsonPropertyName("played_at")]
        public DateTime PlayedAt { get; set; }
        public ContextDataModel Context { get; set; }
    }
    public class CompareTracks : IEqualityComparer<RecentlyPlayedTrackDataModel>
    {
        public bool Equals(RecentlyPlayedTrackDataModel x, RecentlyPlayedTrackDataModel y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.Track == y.Track;
        }

        public int GetHashCode(RecentlyPlayedTrackDataModel obj)
        {
            return obj.Track.GetHashCode();
        }
    }
}
