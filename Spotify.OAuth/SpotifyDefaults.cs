using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.OAuth
{
    public static class SpotifyDefaults
    {
        //The user can feel free to modify this
        public const string AuthenticationScheme = "Spotify";

        public static readonly string DisplayName = "Spotify";

        public static readonly string AuthorizationEndpoint = "https://accounts.spotify.com/authorize";

        public static readonly string TokenEndpoint = "https://accounts.spotify.com/api/token";

        public static readonly string UserInformationEndpoint = "https://api.spotify.com/v1/me";

        public static readonly string Issuer = "Spotify";

        public static class ClaimType
        {
            public static readonly string ProfilePicture = "images";
            public static readonly string ExternalUrls = "external_urls";
            public static readonly string Followers = "followers";
            public static readonly string ExplicitContent = "explicit_content";
        }

        
    }
}
