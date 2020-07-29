using System.Collections.Generic;

namespace Zone.Website.ViewModels
{
    public class RecentlyPlayedViewModel
    {
        public string Href { get; set; }
        public int Limit { get; set; }
        private List<RecentlyPlayedTrackViewModel> RecentlyPlayedTracks { get; set; } = new List<RecentlyPlayedTrackViewModel>();
        public string Next { get; set; }
    }
}
