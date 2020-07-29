using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.ViewModels
{
    public class TracksViewModel
    {
        public string Href { get; set; }
        public List<TrackViewModel> SongsList { get; set; } = new List<TrackViewModel>();
    }
}
