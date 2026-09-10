namespace ArtistApi.Models
{
    public interface IArtistRepo
    { 
        public List<Artist> GetAllArtists();
        public void AddArtist(Artist artist);
        bool DeleteArtist(string id);
        object GetCachedSpotifyDetails(string id);
    }
}
