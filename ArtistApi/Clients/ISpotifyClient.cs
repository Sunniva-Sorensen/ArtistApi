using ArtistApi.Dtos;

namespace ArtistApi.Clients
{
    public interface ISpotifyClient
    {
        Task<SpotifyArtistDetails> GetArtist(string id);
    }

}
