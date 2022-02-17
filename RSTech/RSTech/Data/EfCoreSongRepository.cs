using System;
using System.Collections.Generic;
using System.Linq;
using RSTech.Models;

namespace RSTech.Data
{
    public class EfCoreSongRepository : EfCoreRepository<Song, SongContext>
    {
        public EfCoreSongRepository(SongContext context) : base(context)
        {
        }

        public Song? GetSongByName(string? name)
        {
            if (name == null)
            {
                return null;
            }
            var song = context.Songs.FirstOrDefault(x => x.Name != null && x.Name.ToLower() == name.ToLower());
            return song;
        }
        public List<Song> GetSongByGenre(string genre)
        {
            var songs = context.Songs.Where(x => x.Genre != null && x.Genre.ToLower() == x.Genre.ToLower()).ToList();
            return songs;
        }
        // We can add new methods specific to the song repository here in the future
    }
}