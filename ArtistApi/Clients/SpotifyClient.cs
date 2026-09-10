using ArtistApi.Dtos;
using System.Text.Json;

namespace ArtistApi.Clients
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly HttpClient _http;

        public SpotifyClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<SpotifyArtistDetails> GetArtist(string id)
        {
            var response = await _http.GetAsync($"https://api.spotify.com/v1/artists/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var spotify = JsonSerializer.Deserialize<SpotifyArtistDetails>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return spotify;
        }
    }

}
