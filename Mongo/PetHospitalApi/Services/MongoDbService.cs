using MongoDB.Driver;
using PetHospitalApi.Models;

namespace PetHospitalApi.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Pet> _pets;

        public MongoDbService(IConfiguration config)
        {
            var connectionString = config.GetSection("MongoDB:ConnectionString").Value;
            var databaseName = config.GetSection("MongoDB:DatabaseName").Value;

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            // Use the collection for Pet documents
            _pets = database.GetCollection<Pet>("pethospitalcollection");
            //_collection = database.GetCollection<BsonDocument>("pethospitalcollection");
        }

        public async Task<List<Pet>> GetAllPetsAsync()
        {
            return await _pets.Find(_ => true).ToListAsync();
        }

        public async Task<Pet> GetPetByIdAsync(string id)
        {
            return await _pets.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddPetAsync(Pet pet)
        {
            await _pets.InsertOneAsync(pet);
        }

        public async Task UpdatePetAsync(string id, Pet updatedPet)
        {
            await _pets.ReplaceOneAsync(p => p.Id == id, updatedPet);
        }

        public async Task DeletePetAsync(string id)
        {
            await _pets.DeleteOneAsync(p => p.Id == id);
        }
    }
}
