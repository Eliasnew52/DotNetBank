using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Sex { get; set; }

        public decimal Income { get; set; }

        [Required]
        //This field will represent a unique id from the client, such as a national ID
        public string IdentificationNumber { get; set; }

        // Navigation property for related accounts (in terms of Entity Framework)
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        
    }
}