using System.Data.SqlClient;
using System.Security;
using apbd_4_s22085.DTO;

namespace apbd_4_s22085.DAL;

public class SqlServerAnimalRepository : IAnimalRepository
{
    private readonly string _connectionString;
    
    public SqlServerAnimalRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentException("Connection string not found");
    }

    public async Task<IEnumerable<Animal>> GetAnimalsAsync(string orderBy)
    {
        
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandText = $"SELECT * FROM Animal ORDER BY {orderBy} ASC";

            await connection.OpenAsync();
            var reader = command.ExecuteReader();
            var animals = new List<Animal>();
            while (reader.Read())
            {
                var animal = new Animal();
                animal.IdAnimal = (int) reader["IdAnimal"];
                animal.Name = reader["Name"].ToString();
                animal.Description = reader["Description"].ToString();
                animal.Category = reader["Category"].ToString();
                animal.Area = reader["Area"].ToString();
                animals.Add(animal);
            }

            return animals;
        }
    }

    public async Task<bool> AddAnimalAsync(AddAnimal animal)
    {
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandText = "INSERT INTO Animal VALUES (@name, @description, @category, @area)";
            command.Parameters.AddWithValue("name", animal.Name);
            command.Parameters.AddWithValue("description", animal.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("category", animal.Category);
            command.Parameters.AddWithValue("area", animal.Area);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }

    public async Task<bool> UpdateAnimalAsync(int id, Animal animal)
    {
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE IdAnimal = @id";
            command.Parameters.AddWithValue("id", id);
            // id is not being updated
            command.Parameters.AddWithValue("name", animal.Name);
            command.Parameters.AddWithValue("description", animal.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("category", animal.Category);
            command.Parameters.AddWithValue("area", animal.Area);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }

    public async Task<bool> DeleteAnimalAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        using (var command = new SqlCommand())
        {
            command.Connection = connection;
            command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @id";
            command.Parameters.AddWithValue("id", id);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}