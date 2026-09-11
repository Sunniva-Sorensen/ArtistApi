using ArtistApi.Clients;
using ArtistApi.Dtos;
using ArtistApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ArtistApi.Controllers
{
    [ApiController]
    [Route("artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepo _repo;
        private readonly ISpotifyClient _spotifyClient;

        public ArtistController(IArtistRepo repo, ISpotifyClient spotifyClient)
        {
            _repo = repo;
            _spotifyClient = spotifyClient;
        }
        
        [HttpGet]
        public ActionResult<List<Artist>> GetAllArtists()
        {
            return _repo.GetAllArtists();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetArtistById(string id, [FromQuery] bool includeDetails = false)
        {
            var artist = _repo.GetAllArtists()
                              .FirstOrDefault(a => a.Id == id);

            if (artist == null)
                return NotFound();

            if (!includeDetails)
                return Ok(artist);

            var cached = _repo.GetCachedSpotifyDetails(id);
            SpotifyArtistDetails spotifyDetails;

            if (cached != null)
            {
                spotifyDetails = cached;
            }
            else
            {
                spotifyDetails = await _spotifyClient.GetArtist(id);
                _repo.CacheSpotifyDetails(id, spotifyDetails);
            }

            var result = new ArtistWithDetails
            {
                Id = artist.Id,
                ArtistName = artist.ArtistName,
                Popularity = spotifyDetails.Popularity,
                Genres = spotifyDetails.Genres
            };

            return Ok(result);
        }

        //Lagt til feilhåndtering i etterkant for testene
        [HttpPost]
        public ActionResult AddArtist(Artist artist)
        {
            if (string.IsNullOrWhiteSpace(artist.Id) ||
                   string.IsNullOrWhiteSpace(artist.ArtistName))
            {
                return BadRequest("Artist must have id and name.");
            }

            try
            {
                _repo.AddArtist(artist);
            }
            catch (InvalidOperationException)
            {
                return Conflict("Artist with this ID already exists.");
            }

            return Created($"/artists/{artist.Id}", artist);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteArtist(string id)
        {
            var deleted = _repo.DeleteArtist(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}
