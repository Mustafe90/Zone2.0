using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.ViewModels
{
    public class TrackViewModel
    {
        public TrackViewModel()
        {
            Artists = new List<ArtistViewModel>();
        }
        public string Name { get; set; }
        public string Id { get; set; }

        public string Uri { get; set; }

        public int DurationMs { get; set; }

        public IEnumerable<ArtistViewModel> Artists { get; set; }

        public Image Image { get; set; }

        public bool Shared { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(TrackViewModel trackX, TrackViewModel trackY)
        {
            if (trackY != null && (trackX != null && trackX.Id == trackY.Id))
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(TrackViewModel trackX, TrackViewModel trackY)
        {
            return !(trackX == trackY);
        }
    }
}
