// See https://aka.ms/new-console-template for more information

using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RSTech.Models;

var jsonSongs = new WebClient().DownloadString("https://www.teamrockstars.nl/sites/default/files/songs.json");
var songs = JsonConvert.DeserializeObject<Song[]>(jsonSongs);
var jsonArtists = new WebClient().DownloadString("https://www.teamrockstars.nl/sites/default/files/artists.json");
var artists = JsonConvert.DeserializeObject<Artist[]>(jsonArtists);

// using (var context = new SongContext(new DbContextOptions<SongContext>()))
// {
//     if (songs != null)
//     {
//         context.Songs.AddRange(songs);
//         await context.SaveChangesAsync();
//     }
// }

using (var context = new ArtistContext(new DbContextOptions<ArtistContext>()))
{
    if (artists != null)
    {
        context.Artists.AddRange(artists);
        await context.SaveChangesAsync();
    }
}


Console.WriteLine("Hello, World!");