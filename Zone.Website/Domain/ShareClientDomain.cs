using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Zone.Core.Models;
using Zone.Website.ViewModels;

namespace Zone.Website.Domain
{
    public class ShareClientDomain
    {
        private readonly IMemoryCache _cache;

        public ShareClientDomain(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool ShareAlbums(ICollection<ShareAlbum> sharedAlbums)
        {

            if (sharedAlbums == null || !sharedAlbums.Any())
            {
                return false;
            }

            var includeAlbums = sharedAlbums.Where(x => x.Shared);

            var albums = GetAlbumsInMemory(includeAlbums);

            //Call the api with albums

            return true;
        }

        public bool ShareTracks(ICollection<ShareAlbum> sharedAlbums)
        {

            if (sharedAlbums == null || !sharedAlbums.Any())
            {
                return false;
            }
            var includeSharedTracks = sharedAlbums
                .Where(x => x.Tracks != null)
                .SelectMany(x => x.Tracks).Where(x => x.Shared).ToList();

            if (!includeSharedTracks.Any())
            {
                return false;
            }

            var tracks = GetTracksInMemory(includeSharedTracks);

            //call the api with tracks

            return true;
        }

        private ICollection<AlbumDataModel> GetAlbumsInMemory(IEnumerable<ShareAlbum> includeAlbums)
        {
            var albumDataModels = new List<AlbumDataModel>();

            foreach (var includeAlbum in includeAlbums)
            {
                var cachedAlbum = _cache.TryGetValue(includeAlbum.Id, out var albumObject);
                var isAlbum = albumObject is AlbumDataModel;

                if (cachedAlbum && isAlbum)
                {
                    var album = (AlbumDataModel) albumObject;

                    albumDataModels.Add(album);
                }
            }

            return albumDataModels;
        }
        private ICollection<TrackDataModel> GetTracksInMemory(IEnumerable<SharedTrack> includeAlbums)
        {
            var albumTrackDataModels = new List<TrackDataModel>();

            foreach (var includeAlbum in includeAlbums)
            {
                var cachedTrack = _cache.TryGetValue(includeAlbum.Id, out var trackObject);
                var isTrack = trackObject is TrackDataModel;

                if (cachedTrack && isTrack)
                {
                    var track = (TrackDataModel)trackObject;

                    albumTrackDataModels.Add(track);
                }
            }

            return albumTrackDataModels;
        }
    }
}
