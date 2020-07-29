using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace Spotify.OAuth
{
    /// <summary>
    /// OAuth Heavily relies RemoteAuthentication (it is an implementation on top of it to make our lives easier)
    /// This by default creates an OauthEvents that notifies us when we are redirected to the authorization end point
    /// Configuration options for <see cref="SpotifyHandler"/>
    /// </summary>
    public class SpotifyOptions : OAuthOptions
    {
        /// <summary>
        /// Constructs a new <see cref="SpotifyOptions"/>
        /// </summary>
        public SpotifyOptions() 
        {
            //The base constructor calls base.Validate() because we inherited 
            //from OauthOptions it comes with default string null / empty check 
            //for fields such as client id 
            CallbackPath = new PathString("/signin-callback");
            AuthorizationEndpoint = SpotifyDefaults.AuthorizationEndpoint;
            TokenEndpoint = SpotifyDefaults.TokenEndpoint;
            UserInformationEndpoint = SpotifyDefaults.UserInformationEndpoint;
            //spotify auth is separate from it's open id server (most likely private/internally used only)
            //Scope.Add("openid"); //This makes it an open id connection  authentication shake

            ClaimActions.MapJsonKey($"{SpotifyDefaults.Issuer}:{ClaimTypes.Country}", "country");
            ClaimActions.MapJsonKey($"{SpotifyDefaults.Issuer}:{ClaimTypes.Name}", "display_name");
            ClaimActions.MapJsonKey($"{SpotifyDefaults.Issuer}:{ClaimTypes.Email}", "email");
            ClaimActions.MapJsonKey($"{SpotifyDefaults.Issuer}:{ClaimTypes.NameIdentifier}", "id");
            ClaimActions.MapJsonKey($"{SpotifyDefaults.Issuer}:{ClaimTypes.DateOfBirth}", "birthdate");
            ClaimActions.MapJsonKey($"{SpotifyDefaults.Issuer}:{ClaimTypes.Uri}", "uri");

            ClaimActions.MapCustomJson($"{SpotifyDefaults.Issuer}{SpotifyDefaults.ClaimType.ProfilePicture}",
                x => x.TryGetProperty($"{SpotifyDefaults.ClaimType.ProfilePicture}", out var imageArrayBlob) ? imageArrayBlob.ToString() : null);

            ClaimActions.MapCustomJson($"{SpotifyDefaults.Issuer}:{SpotifyDefaults.ClaimType.ExternalUrls}",
                x => x.TryGetProperty($"{SpotifyDefaults.ClaimType.ExternalUrls}",out var externalUrl) ? externalUrl.ToString() : null);

            ClaimActions.MapCustomJson($"{SpotifyDefaults.Issuer}{SpotifyDefaults.ClaimType.Followers}",
                x => x.TryGetProperty($"{SpotifyDefaults.ClaimType.Followers}", out var followers) ? followers.ToString() : null);

            ClaimActions.MapCustomJson($"{SpotifyDefaults.Issuer}{SpotifyDefaults.ClaimType.ExplicitContent}",
                x => x.TryGetProperty($"{SpotifyDefaults.ClaimType.ExplicitContent}", out var followers) ? followers.ToString() : null);

        }
    }
}
