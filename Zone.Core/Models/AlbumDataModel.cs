using System.Collections.Generic;

namespace Zone.Core.Models
{
    public class AlbumDataModel : BasicAlbumDataModel
    {
        public List<CopyrightDataModel> Copyrights { get; set; }
        public ExternalIdsDataModel ExternalIdsDataModel { get; set; }
        public List<object> Genres { get; set; }
        public string Label { get; set; }
        public int Popularity { get; set; }
        public TracksDataModel Tracks { get; set; }
    }
}
