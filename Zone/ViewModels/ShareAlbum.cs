using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.ViewModels
{
    public class ShareAlbum
    {
        public string Id { get; set; }

        public bool Shared { get; set; }

        public IEnumerable<SharedTrack> Tracks { get; set; } = new List<SharedTrack>();
    }
}
