using System.ComponentModel.DataAnnotations;

namespace BankApi.Dtos
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required DateTime DateOfBirth { get; set; }
        [Required]
        public required string Sex { get; set; }
        [Required]
        public required decimal Income { get; set; }
        [Required]
        public required string IdentificationNumber { get; set; }
    }
}