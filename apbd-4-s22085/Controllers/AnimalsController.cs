using apbd_4_s22085.DTO;
using Microsoft.AspNetCore.Mvc;

namespace apbd_4_s22085.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AnimalsController : ControllerBase
{

    private readonly ILogger<AnimalsController> _logger;

    public AnimalsController(ILogger<AnimalsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAnimals")]
    public async Task<IActionResult> Get(string? orderBy = "name")
    {
        return Ok(orderBy);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddAnimal(Animal animal)
    {
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnimal(int id, Animal animal)
    {
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        return Ok();
    }
}
