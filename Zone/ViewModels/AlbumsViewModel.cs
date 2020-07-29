using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.ViewModels
{
    public class AlbumsViewModel
    {
        public string Href { get; set; }

        public List<AlbumViewModel> Album { get; set; } = new List<AlbumViewModel>();

        public int Limit { get; set; }

        public object Next { get; set; }

        public int Offset { get; set; }
    }
}
