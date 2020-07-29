using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Spotify.OAuth;
using Zone.Services;
using Zone.ViewModels;

namespace Zone.Domain
{
    public class SpotifyClientDomain
    {
        private readonly SpotifyHttpClientService _service;
        private readonly IHttpContextAccessor _httpContext;

        public SpotifyClientDomain(SpotifyHttpClientService service, IHttpContextAccessor accessor)
        {
            _service = service;
            _httpContext = accessor;
        }

        public async Task<AlbumsViewModel> GetAlbums()
        {
            var token = await _httpContext.HttpContext.GetTokenAsync(SpotifyDefaults.AuthenticationScheme,"access_token");
            var model = await _service.GetAlbums(token);

            if (model == null)
            {
                //Log
                return null;
            }

            return new AlbumsViewModel
            {
                Album = model.Albums.Select(x => new AlbumViewModel
                {
                    AddedAt = x.AddedAt,
                    Name = x.Album.Name,
                    Genres = x.Album.Genres,
                    Id = x.Album.Id,
                    Uri = x.Album.Uri,
                    Label = x.Album.Label,
                    Popularity = x.Album.Popularity,
                    Image = x.Album.Images.Select(x => new Image
                    {
                        Url = x.Url,
                        Width = x.Width,
                        Height = x.Height
                    }).FirstOrDefault(),
                    Artists = x.Album.Artists.Select(s => new ArtistViewModel
                    {
                        Name = s.Name
                    }),
                    Tracks = new TracksViewModel
                    {
                        Href = x.Album.Tracks?.Href,
                        SongsList = x.Album.Tracks?.SongsList.Select(p => new TrackViewModel
                        {
                            Name = p.Name,
                            DurationMs = p.DurationMs,
                            Id = p.Id,
                            Artists = p.Artists.Select(s => new ArtistViewModel
                            {
                                Name = s.Name
                            }),
                            Uri = p.Uri
                        }).ToList(),
                    },
                    ReleaseDate = x.Album.ReleaseDate,
                    AlbumType = x.Album.AlbumType
                }).ToList()
            };
        }
    }
}
