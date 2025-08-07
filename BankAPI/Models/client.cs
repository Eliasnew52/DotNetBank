using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class Client
    {
       public Guid Id { get; set; }

        [Required]
        public  required string Name { get; set; }

        public  required DateTime DateOfBirth { get; set; }

        public required string Sex { get; set; }

        public required decimal Income { get; set; }

        [Required]
        //This field will represent a unique id from the client, such as a national ID
        public required string IdentificationNumber { get; set; }

        // Navigation property for related accounts (in terms of Entity Framework)
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        
    }
}