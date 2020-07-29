using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Spotify.OAuth
{
    public static class SpotifyExtensions
    {
        public static AuthenticationBuilder AddSpotify(this AuthenticationBuilder builder,
            string authenticationScheme, string displayName, Action<SpotifyOptions> configurationOption)
        {
            return builder.AddOAuth<SpotifyOptions,SpotifyHandler>(authenticationScheme, displayName,configurationOption);
        }

        public static AuthenticationBuilder AddSpotify(this AuthenticationBuilder builder)
        {
            return builder.AddSpotify(SpotifyDefaults.AuthenticationScheme, 
                SpotifyDefaults.DisplayName, _ => { });
        }

        public static AuthenticationBuilder AddSpotify(this AuthenticationBuilder builder,
            Action<SpotifyOptions> configurationOptions)
        {
            return builder.AddSpotify(SpotifyDefaults.AuthenticationScheme, SpotifyDefaults.DisplayName,
                configurationOptions);
        }

        public static AuthenticationBuilder AddSpotify(this AuthenticationBuilder builder, string authenticationScheme,
             Action<SpotifyOptions> configurationOption)
        {
            return builder.AddSpotify(authenticationScheme, SpotifyDefaults.DisplayName, configurationOption);
        }
    }
}
