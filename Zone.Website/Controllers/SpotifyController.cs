using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.OAuth;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zone.Website.Domain;
using Zone.Website.ViewModels;

namespace Zone.Website.Controllers
{
    [Authorize(AuthenticationSchemes = SpotifyDefaults.AuthenticationScheme)]
    public class SpotifyController : Controller
    {
        private readonly SpotifyClientDomain _spotifyClient;
        private readonly ShareClientDomain _clientDomain;

        public SpotifyController(SpotifyClientDomain spotifyHttpClientService, ShareClientDomain clientDomain)
        {
            _spotifyClient = spotifyHttpClientService;
            _clientDomain = clientDomain;
        }
        public async Task<IActionResult> Library()
        {
            var albums = await _spotifyClient.GetAlbums();

            return View(albums);
        }
        [HttpPost]
        public IActionResult Share([FromForm] ICollection<ShareAlbum> album)
        {
            if (!ModelState.IsValid)
            {
                //Todo: Implement an error page that is helpful
                return null;
            }

            _clientDomain.ShareAlbums(album);
            _clientDomain.ShareTracks(album);

            return null;
        }
    }

}
