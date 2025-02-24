using ConnectDbLearning.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace ConnectDbLearning.Modules
{
    public class Hotel
    {
        //[Key] etc. etc.  You can do stuff to the SQL
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public double Salary { get; set; }
        public DateOnly Birthdate { get; set; }
        public DateOnly Hiredate { get; set; }
    }
}
