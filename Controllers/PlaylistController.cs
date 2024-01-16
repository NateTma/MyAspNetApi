using System;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApi.Services;
using MyAspNetApi.Models;
using MyAspNetApi.Data;

namespace MyAspNetApi.Controllers;

[Controller] //makes the class a controller
[Route("api/[controller]")] //path to access different endpoint functions for this controller class
public class PlaylistController: Controller{

    private readonly MongoDBService _mongoDBService;

    public PlaylistController(MongoDBService mongoDBService){
        _mongoDBService = mongoDBService;
    }

    //defining endpoints

    [HttpGet] //read
    public async Task<List<Playlist>> Get() {
        return await _mongoDBService.GetAsync(string databaseName);
    }

    [HttpPost] //create
    public async Task<IActionResult> Post([FromBody] Playlist playlist) {
        await _mongoDBService.CreateAsync(string databaseName, playlist);
        return CreatedAtAction(nameof(Get), new { id = playlist.Id }, playlist);
    }

    [HttpPut("{id}")] //update, finds document using id
    public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) {
        await _mongoDBService.AddToPlaylistAsync(id, movieId);
        return NoContent();
    }

    [HttpDelete("{id}")] //delete
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }

}