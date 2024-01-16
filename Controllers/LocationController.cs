using System;
using Microsoft.AspNetCore.Mvc;
using MyAspNetApi.Services;
using MyAspNetApi.Models;

namespace MyAspNetApi.Controllers;

[Controller] //makes the class a controller
[Route("api/[controller]")] //path to access different endpoint functions for this controller class
public class LocationController: Controller {

    private readonly MongoDBWService _mongoDBWService;

    public LocationController(MongoDBWService mongoDBWService){
        _mongoDBWService =  mongoDBWService;
    }

    //defining endpoints

    [HttpGet] //read
    public async Task<List<Locations>> Get() {
        return await _mongoDBWService.GetAsync();
    }

    [HttpPost] //create
    public async Task<IActionResult> Post([FromBody] Locations location) {
        await _mongoDBWService.CreateAsync(location);
        return CreatedAtAction(nameof(Get), new { id = location.Id }, location);
    }

    [HttpPut ("{id}")] //update
    public async Task<IActionResult> AddToLocation(string id, [FromBody] string Lname) {
        await _mongoDBWService.AddToLocationAsync(id, Lname);
        return NoContent();
    }

    [HttpDelete ("{id}")] //delete
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBWService.DeleteAsync(id);
        return NoContent();
    }
    
}