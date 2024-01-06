using apbd_4_s22085.DTO;

namespace apbd_4_s22085.DAL;

public interface IAnimalRepository
{
    Task<IEnumerable<Animal>> GetAnimalsAsync(string orderBy);
    
    
    Task<bool> AddAnimalAsync(AddAnimal animal);
    
    Task<bool> UpdateAnimalAsync(int id, Animal animal);
    
    Task<bool> DeleteAnimalAsync(int id);
}