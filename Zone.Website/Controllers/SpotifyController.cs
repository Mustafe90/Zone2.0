using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zone.Domain;
using Zone.Services;
using Zone.ViewModels;

namespace Zone.Controllers
{
    [Authorize(AuthenticationSchemes = SpotifyDefaults.AuthenticationScheme)]
    public class SpotifyController : Controller
    {
        private readonly SpotifyClientDomain _spotifyClient;

        public SpotifyController(SpotifyClientDomain spotifyHttpClientService)
        {
            _spotifyClient = spotifyHttpClientService;
        }
        public async Task<IActionResult> Library()
        {
            var albums = await _spotifyClient.GetAlbums();

            return View(albums);
        }
        [HttpPost]
        public IActionResult Share([FromForm] IEnumerable<ShareAlbum> album)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            
            return null;
        }
    }

}
