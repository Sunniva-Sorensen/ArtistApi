namespace ArtistApi.Dtos
{
    public class ArtistWithDetails
    {
        public string? Id { get; set; }
        public string? ArtistName { get; set; }
        public int Popularity { get; set; }
        public List<string>? Genres { get; set; }
    }

}
