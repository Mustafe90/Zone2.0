using System.Collections.Generic;

namespace Zone.Website.ViewModels
{
    public class TracksViewModel
    {
        public string Href { get; set; }
        public List<TrackViewModel> SongsList { get; set; } = new List<TrackViewModel>();
    }
}
