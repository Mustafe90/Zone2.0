using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zone.Core.Models
{
    public class UserTrackDataModelDataModel : TrackDataModel
    {
        public BasicAlbumDataModel Album { get; set; }

        public int Popularity { get; set; }

    }
}
