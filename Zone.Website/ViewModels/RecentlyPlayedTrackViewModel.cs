using System;

namespace Zone.Website.ViewModels
{
    public class RecentlyPlayedTrackViewModel
    {
        public TrackViewModel Track { get; set; }
        public DateTime PlayedAt { get; set; }
        public ContextViewModel Context { get; set; }
    }
}
