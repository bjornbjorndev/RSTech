#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RSTech.Data;
using RSTech.Models;

namespace RSTech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : MyBaseController<Song, EfCoreSongRepository>
    {
        public SongsController(EfCoreSongRepository repository) : base(repository)
        {
        }

        // GET: api/[controller]/5
        /// <summary>
        /// Name has prio over Genre If song is found with name we return the list
        /// If no song has been found with name or name is empty we return all songs with genre
        /// </summary>
        /// <param name="name">Name of the song</param>
        /// <param name="genre">Name of the genre</param>
        /// <returns>List of songs matched</returns>
        [HttpGet("read")]
        public async Task<ActionResult<List<Song>>> Read(string name = "", string genre = "")
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(genre))
            {
                return BadRequest("please fill in one of the optional parameters to find a song");
            }

            var songs = new List<Song>();

            var entity = repository.GetSongByName(name);
            if (entity != null)
            {
                songs.Add(entity);
                return songs;
            }

            var songsByGenre = repository.GetSongByGenre(genre);
            if (songsByGenre.Count > 0)
            {
                return songsByGenre;
            }

            return NotFound($"couldnt find song with name {name} and genre {genre}");
        }

        //POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            //Song already exists
            if (repository.GetSongByName(song.Name) != null)
            {
                return Conflict("Song with same name already exists");
            }

            await repository.Add(song);

            return CreatedAtAction("Get", new {id = song.Id}, song);
        }
    }

    // [Route("api/[controller]")]
    // [ApiController]
    // public class SongsController : ControllerBase
    // {
    //     private readonly SongContext _context;
    //
    //     public SongsController(SongContext context)
    //     {
    //         _context = context;
    //     }
    //     
    //     // // GET: api/SeedSongs
    //     // [HttpGet("/api/SeedSongs")]
    //     // public async Task<ActionResult<IEnumerable<Song>>> SeedSongs()
    //     // {
    //     //     var json = new WebClient().DownloadString("https://www.teamrockstars.nl/sites/default/files/songs.json");
    //     //     var songs = JsonConvert.DeserializeObject<Song[]>(json);
    //     //     if (songs == null)
    //     //     {
    //     //         return BadRequest("no songs found");
    //     //     }
    //     //     
    //     //     _context.Songs.AddRange(songs);
    //     //     await _context.SaveChangesAsync();
    //     //     
    //     //     return await _context.Songs.ToListAsync();
    //     // }
    //
    //     // GET: api/Songs
    //     [HttpGet]
    //     public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
    //     {
    //         return await _context.Songs.ToListAsync();
    //     }
    //
    //     // GET: api/Songs/5
    //     [HttpGet("{id}")]
    //     public async Task<ActionResult<Song>> GetSong(int id)
    //     {
    //         var song = await _context.Songs.FindAsync(id);
    //
    //         if (song == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         return song;
    //     }
    //
    //     // PUT: api/Songs/5
    //     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //     [HttpPut("{id}")]
    //     public async Task<IActionResult> PutSong(int id, Song song)
    //     {
    //         if (id != song.Id)
    //         {
    //             return BadRequest();
    //         }
    //
    //         _context.Entry(song).State = EntityState.Modified;
    //
    //         try
    //         {
    //             await _context.SaveChangesAsync();
    //         }
    //         catch (DbUpdateConcurrencyException)
    //         {
    //             if (!SongExists(id))
    //             {
    //                 return NotFound();
    //             }
    //             else
    //             {
    //                 throw;
    //             }
    //         }
    //
    //         return NoContent();
    //     }
    //
    //     // POST: api/Songs
    //     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //     [HttpPost]
    //     public async Task<ActionResult<Song>> PostSong(Song song)
    //     {
    //         //Song already exists
    //         if (_context.Songs.FirstOrDefault(x => x.Name == song.Name) != null)
    //         {
    //             return Conflict("Song with same name already exists");
    //         }
    //         
    //         _context.Songs.Add(song);
    //         await _context.SaveChangesAsync();
    //
    //         return CreatedAtAction("GetSong", new { id = song.Id }, song);
    //     }
    //
    //     // DELETE: api/Songs/5
    //     [HttpDelete("{id}")]
    //     public async Task<IActionResult> DeleteSong(int id)
    //     {
    //         var song = await _context.Songs.FindAsync(id);
    //         if (song == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         _context.Songs.Remove(song);
    //         await _context.SaveChangesAsync();
    //
    //         return NoContent();
    //     }
    //
    //     private bool SongExists(int id)
    //     {
    //         return _context.Songs.Any(e => e.Id == id);
    //     }
    // }
}