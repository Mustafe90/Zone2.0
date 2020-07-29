using System.Collections.Generic;

namespace Zone.Website.ViewModels
{
    public class ShareAlbum
    {
        public string Id { get; set; }

        public bool Shared { get; set; }

        public IEnumerable<SharedTrack> Tracks { get; set; } = new List<SharedTrack>();
    }
}
