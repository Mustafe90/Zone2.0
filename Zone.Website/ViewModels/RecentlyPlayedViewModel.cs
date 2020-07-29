using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.ViewModels
{
    public class RecentlyPlayedViewModel
    {
        public string Href { get; set; }
        public int Limit { get; set; }
        private List<RecentlyPlayedTrackViewModel> RecentlyPlayedTracks { get; set; } = new List<RecentlyPlayedTrackViewModel>();
        public string Next { get; set; }
    }
}
