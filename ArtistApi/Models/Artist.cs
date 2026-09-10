namespace ArtistApi.Models
{
    public class Artist
    {
        public string Id { get; set; }// this is the ID spotify uses
        public string ArtistName { get; set; }
        public Artist(string id, string artistname)
        { Id = id; ArtistName = artistname; }
    }
}
