using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Zone.Core.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Zone.Services
{
    public class SpotifyHttpClientService
    {
        private readonly HttpClient _client;
        private readonly ILogger<SpotifyHttpClientService> _logger;

        public SpotifyHttpClientService(HttpClient client, ILogger<SpotifyHttpClientService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<AlbumCollection> GetAlbums(string bearerToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/me/albums");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to retrieve user albums {0}, {1}", response.StatusCode, response.ReasonPhrase);

                return default;
            };

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                _logger.LogInformation("Response content is empty");

                return default;
            }

            //Todo Write Custom converter    
            var model = JsonSerializer.Deserialize<AlbumCollection>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            if (model == null)
            {
                _logger.LogError($"Unable to cast json to object of type {nameof(AlbumCollection)}");

                return default;
            }

            return model;
        }

        public async Task<RecentlyPlayedDataModel> GetRecentlyPlayedTracks(string bearerToken)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "/v1/me/player/recently-played");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to retrieve recently played tracks");

                return default;
            }

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                _logger.LogInformation("Response content is empty");

                return default;
            }

            var model = JsonSerializer.Deserialize<RecentlyPlayedDataModel>(content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            if (model == null)
            {
                _logger.LogError($"Unable to convert json to type {nameof(RecentlyPlayedDataModel)}");

                return default;
            }

            return model;
        }

        public async Task<CurrentlyPlayingDataModel> GetCurrentlyPlayingTrack(string bearerToken)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "v1/me/player/currently-playing");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Unable to retrieve user currently playing tracks");

                return default;
            }

            var stringContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(stringContent))
            {
                _logger.LogInformation("Response is empty");

                return default;
            }

            var model = JsonSerializer.Deserialize<CurrentlyPlayingDataModel>(stringContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (model == null)
            {
                _logger.LogInformation($"Unable to deserialize json to type {nameof(CurrentlyPlayingDataModel)} : Json Payload:  {stringContent}");

                return default;
            }

            return model;
        }
    }
}
