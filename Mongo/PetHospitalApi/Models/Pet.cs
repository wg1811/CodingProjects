using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
// explain
using MongoDB.Bson.Serialization.Attributes;

namespace PetHospitalApi.Models
{
    public class Pet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Species { get; set; }

        [Required]
        public string? Breed { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Color { get; set; } = "generic";

        // pet's owner
        [Required]
        public Owner? Owner { get; set; }

        //medical history
        [Required]
        public List<MedicalHistoryEntry> MedicalHistory { get; set; } =
            new List<MedicalHistoryEntry>();

        [Required]
        public List<VaccinationRecord> Vaccinations { get; set; } = new List<VaccinationRecord>();
    }
}
