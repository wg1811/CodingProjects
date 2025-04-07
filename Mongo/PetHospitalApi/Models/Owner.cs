using System.ComponentModel.DataAnnotations;

namespace PetHospitalApi.Models
{
    public class Owner
    {
        [Required]
        public string? Name { get; set; }
        public ContactInfo? Contact { get; set; }
    }
}
