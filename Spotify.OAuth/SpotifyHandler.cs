using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spotify.OAuth
{
    public class SpotifyHandler : OAuthHandler<SpotifyOptions>
    {
        public SpotifyHandler(
            IOptionsMonitor<SpotifyOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        //Todo: be more regerious
        //We could intercept this request (before .net core just fires off) and validate the state first
        //and see if it is exactly the same as the state we encrypted earlier
        //for added measure of security.
        protected override async Task<AuthenticationTicket> CreateTicketAsync
            (ClaimsIdentity identity,
            AuthenticationProperties properties,
            OAuthTokenResponse tokens)
        {
            // Get the Spotify user
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);

            request.Headers.Authorization = new AuthenticationHeaderValue
                ("Bearer", tokens.AccessToken);

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogError("An error occured in trying to a retrieve user data from spotify." +
                                $"Code status {response.StatusCode} Headers: {response.Headers} " +
                                $"Payload {await response.Content.ReadAsStringAsync()}");

                //check error query parameter to see if user denied access
                throw new HttpRequestException
                    ($"Unable to retrieve user info ({response.StatusCode})");
            }

            using var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

            //user session + claimIdentity
            var context = new OAuthCreatingTicketContext
                (new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens,payload.RootElement
            );

            context.RunClaimActions();
            await Events.CreatingTicket(context);

            //return identity info and authentication state
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }
        //Build the challenge url
        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            return base.BuildChallengeUrl(properties, redirectUri);
        }
        //Responsible for redirecting the authorization end point 
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            return base.HandleChallengeAsync(properties);
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return base.HandleAuthenticateAsync();
        }

        //This gets called by HandleAuthenticateAsync

    }
}
