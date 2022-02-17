using RSTech.Models;

namespace RSTech.Data
{
    public class EfCoreArtistRepository : EfCoreRepository<Artist, ArtistContext>
    {
        public EfCoreArtistRepository(ArtistContext context) : base(context)
        {

        }
        // We can add new methods specific to the song repository here in the future
    }
}