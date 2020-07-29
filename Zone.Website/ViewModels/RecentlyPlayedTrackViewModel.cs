using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.ViewModels
{
    public class RecentlyPlayedTrackViewModel
    {
        public TrackViewModel Track { get; set; }
        public DateTime PlayedAt { get; set; }
        public ContextViewModel Context { get; set; }
    }
}
