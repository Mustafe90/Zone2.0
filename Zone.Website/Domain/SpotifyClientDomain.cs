using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Spotify.OAuth;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zone.Core.Models;
using Zone.Website.Services;
using Zone.Website.ViewModels;

namespace Zone.Website.Domain
{
    public class SpotifyClientDomain
    {
        private readonly SpotifyHttpClientService _service;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMemoryCache _cache;

        public SpotifyClientDomain(SpotifyHttpClientService service, IHttpContextAccessor accessor, IMemoryCache cache)
        {
            _service = service;
            _httpContext = accessor;
            _cache = cache;
        }

        public async Task<AlbumsViewModel> GetAlbums()
        {
            var token = await _httpContext.HttpContext.GetTokenAsync(SpotifyDefaults.AuthenticationScheme, "access_token");
            var model = await _service.GetAlbums(token);

            if (model == null)
            {
                //Log
                return null;
            }

            CacheAlbums(model);

            foreach (var albumContainer in model.Albums)
            {
               CacheTracks(albumContainer); 
            }

            return AlbumsViewModel(model);
        }

        private static AlbumsViewModel AlbumsViewModel(AlbumCollection model)
        {
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

        protected void CacheAlbums(AlbumCollection model)
        {
            foreach (var albumContainer in model.Albums)
            {
                var albumCached = _cache.TryGetValue(albumContainer.Album.Id, out _);

                if (!albumCached)
                {
                    _cache.Set(albumContainer.Album.Id, albumContainer.Album, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(5),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5),
                        SlidingExpiration = TimeSpan.FromHours(1),
                    });
                }
            }
        }
        protected void CacheTracks(AlbumContainer model)
        {
            if (model.Album.Tracks?.SongsList == null)
            {
                return;
            }

            foreach (var track in model.Album.Tracks.SongsList)
            {
                var trackCached = _cache.TryGetValue(track.Id, out _);

                if (!trackCached)
                {
                    _cache.Set(track.Id, track, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(5),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5),
                        SlidingExpiration = TimeSpan.FromHours(1),
                    });
                }
            }
        }
    }
}
