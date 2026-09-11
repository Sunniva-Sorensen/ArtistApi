using ArtistApi.Dtos;

namespace ArtistApi.Models
{
    public interface IArtistRepo
    { 
        public List<Artist> GetAllArtists();
        public void AddArtist(Artist artist);
        bool DeleteArtist(string id);
        SpotifyArtistDetails? GetCachedSpotifyDetails(string id);
        void CacheSpotifyDetails(string id, SpotifyArtistDetails details);
    }
}
