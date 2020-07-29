using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.ViewModels
{
    public class AlbumViewModel
    {
        public DateTime AddedAt { get; set; }
        public List<object> Genres { get; set; }
        public string Label { get; set; }
        public int Popularity { get; set; }
        public TracksViewModel Tracks { get; set; }
        public Image Image { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }


        public string ReleaseDate { get; set; }
        public string AlbumType { get; set; }
        public IEnumerable<ArtistViewModel> Artists { get; set; }

        public string Uri { get; set; }

        public bool Shared { get; set; }


    }
}
