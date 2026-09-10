using ArtistApi.Dtos;

namespace ArtistApi.Models
{
    public class ArtistRepo : IArtistRepo
    {
        private List<Artist> _artists = new List<Artist>() {
            { new Artist("711MCceyCBcFnzjGY4Q7Un", "AC/DC") },
            { new Artist("3fMbdgg4jU18AjLCKBhRSm", "Micael Jackson") },
            { new Artist("1OTNNdgU6qLUTCwvJxcObX", "Knutsen & Ludvigsen") },
            { new Artist("12Chz98pHFMPJEknJQMWvI", "Muse") },
            { new Artist("4TrraAsitQKl821DQY42cZ", "Sigrid") },
        };
        public List<Artist> GetAllArtists()
        { 
            return _artists; 
        }

        //Lagt til feilhåndtering i ettertid for testene
        public void AddArtist(Artist artist)
        {
            if (_artists.Any(a => a.Id == artist.Id))
                throw new InvalidOperationException("Artist already exists");
           
            _artists.Add(artist);
        }

        public bool DeleteArtist(string id)
        {
            var artist = _artists.FirstOrDefault(a => a.Id == id);
            if (artist == null)
                return false;

            _artists.Remove(artist);
            return true;
        }

        //Caching for Spotify artister
        private readonly Dictionary<string, SpotifyArtistDetails> _spotifyCache
            = new Dictionary<string, SpotifyArtistDetails>();

        public SpotifyArtistDetails? GetCachedSpotifyDetails(string id)
        {
            if (_spotifyCache.TryGetValue(id, out var details))
                return details;

            return null;
        }

        public void CacheSpotifyDetails(string id, SpotifyArtistDetails details)
        {
            _spotifyCache[id] = details;
        }

    }
}
