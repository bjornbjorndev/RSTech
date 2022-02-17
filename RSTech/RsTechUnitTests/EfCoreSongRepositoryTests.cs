using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RSTech.Data;
using RSTech.Models;

namespace RsTechUnitTests;

public class Tests
{
    private async Task<EfCoreSongRepository> CreateSongRepositoryAsync()
    {
        var options = new DbContextOptionsBuilder<SongContext>()
            .UseInMemoryDatabase(databaseName: "RSTechInMemory").Options;
        SongContext context = new SongContext(options);
        context.Database.EnsureDeleted();
        return  new EfCoreSongRepository(context);
    }

    [Test]
    public async Task CreatRepoSuccessTest()
    {
        //Arrange
        var repository = await CreateSongRepositoryAsync();
        
        // Act
        await repository.Add(new Song()
        {
            Id = 1,
            Name = "test"
        });
        
        //Assert
        var songs = await repository.GetAll();
        Assert.AreEqual(1, songs.Count);
    }
    
    [Test]
    public async Task FindByNameSuccessTest()
    {
        //Arrange
        var repository = await CreateSongRepositoryAsync();
        
        // Act
        await repository.Add(new Song()
        {
            Id = 2,
            Name = "test2"
        });
        
        //Assert
        var song = repository.GetSongByName("test2");
        Assert.NotNull(song);
    }
    
    [Test]
    public async Task FindByGenreSuccessTest()
    {
        //Arrange
        var repository = await CreateSongRepositoryAsync();
        
        // Act
        await repository.Add(new Song()
        {
            Id = 3,
            Name = "test",
            Genre = "Rock"
        });
        
        //Assert
        var songs = repository.GetSongByGenre("Rock");
        Assert.AreEqual(songs.Count,1);
    }
    
    [Test]
    public async Task DeleteRepoSuccessTest()
    {
        //Arrange
        var repository = await CreateSongRepositoryAsync();
        
        // Act
        await repository.Add(new Song()
        {
            Id = 3,
            Name = "test",
            Genre = "Rock"
        });
        var songs = await repository.GetAll();
        foreach (var song in songs)
        {
            await repository.Delete(song.Id);
        }
        
        
        //Assert
        songs = await repository.GetAll();
        Assert.AreEqual(0, songs.Count);
    }
    
    [Test]
    public async Task GetRepoSuccessTest()
    {
        //Arrange
        var repository = await CreateSongRepositoryAsync();
        
        // Act
        await repository.Add(new Song()
        {
            Id = 1,
            Name = "test"
        });
        var song = await repository.Get(1);
        
        //Assert
        Assert.NotNull(song);
        Assert.AreEqual("test", song.Name);
    }
    
    [Test]
    public async Task GeAllRepoSuccessTest()
    {
        //Arrange
        var repository = await CreateSongRepositoryAsync();
        
        // Act
        await repository.Add(new Song()
        {
            Id = 4,
            Name = "test"
        });
        await repository.Add(new Song()
        {
            Id = 5,
            Name = "test2"
        });
        var songs = await repository.GetAll();
        
        //Assert
        Assert.AreEqual(2, songs.Count);
    }
    
    [Test]
    public async Task UpdateRepoSuccessTest()
    {
        //Arrange
        var repository = await CreateSongRepositoryAsync();
        
        // Act
        var song = new Song()
        {
            Id = 1,
            Name = "test"
        };
        await repository.Add(song);
        song.Name = "Barry";
        await repository.Update(song);
        var songFromDb = await repository.Get(1);
        
        //Assert
        Assert.NotNull(songFromDb);
        Assert.AreEqual("Barry", songFromDb.Name);
    }


    // [Test]
    // public void TestAddingSong()
    // {
    //     var options = new DbContextOptionsBuilder<SongContext>()
    //         .UseInMemoryDatabase(databaseName: "RSTechInMemory").Options;
    //
    //     // 1. Arrange
    //     var song = new Song()
    //     {
    //         Name = "RL1"
    //     };
    //     
    //     // Set up a context (connection to the "DB") for writing
    //     using (var context = new SongContext(options))
    //     {
    //         // 2. Act 
    //         context.Songs.Add(song);
    //         context.SaveChanges();
    //         
    //         // 3. Assert
    //         var result = context.Songs.FirstOrDefault(x => x.Name == song.Name);
    //         Assert.IsNotNull(result);
    //     }
    // }
}