using apbd_4_s22085.DAL;
using apbd_4_s22085.DTO;
using Microsoft.AspNetCore.Mvc;

namespace apbd_4_s22085.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AnimalsController : ControllerBase
{

    private readonly ILogger<AnimalsController> _logger;
    private readonly IAnimalRepository _animalRepository;

    public AnimalsController(ILogger<AnimalsController> logger, IAnimalRepository animalRepository)
    {
        _logger = logger;
        _animalRepository = animalRepository;
    }

    [HttpGet(Name = "GetAnimals")]
    public async Task<IActionResult> Get(string? orderBy = "name")
    {
        _logger.LogInformation("GetAnimals called with orderBy = {orderBy}", orderBy);
        if (orderBy != null 
            && orderBy != "name" 
            && orderBy != "category"
            && orderBy != "area"
            && orderBy != "description")
        {
            return BadRequest("Invalid orderBy parameter");
        }
        var animals = await _animalRepository.GetAnimalsAsync(orderBy);
        return Ok(animals);
    }
    
    [HttpPost(Name="AddAnimal")]
    public async Task<IActionResult> AddAnimal(AddAnimal animal)
    {
        _logger.LogInformation("AddAnimal called with animal = {animal}", animal);
        var result = await _animalRepository.AddAnimalAsync(animal);
        if (result)
        {
            return Created("GetAnimals", "Animal added");
        }
        return BadRequest("Could not add animal");
    }
    
    [HttpPut("{id}", Name = "UpdateAnimal")]
    public async Task<IActionResult> UpdateAnimal(int id, Animal animal)
    {
        _logger.LogInformation("UpdateAnimal called with id = {id} and animal = {animal}", id, animal);
        var result = await _animalRepository.UpdateAnimalAsync(id, animal);
        if (result)
        {
            return Ok("Animal updated");
        }
        return BadRequest("Could not update animal");
    }
    
    [HttpDelete("{id}", Name="DeleteAnimal")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        _logger.LogInformation("DeleteAnimal called with id = {id}", id);
        var result = await _animalRepository.DeleteAnimalAsync(id);
        if (result)
        {
            return Ok("Animal deleted");
        }
        return BadRequest("Could not delete animal");
    }
}
