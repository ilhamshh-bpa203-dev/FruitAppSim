using System.ComponentModel.DataAnnotations;

namespace FruitSimulation.ViewModels
{
    public class RegisterVM
    {
        [MaxLength(25)]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(25)]
        [MinLength(3)]
        public string Surame { get; set; }
        [MaxLength(35)]
        [MinLength(4)]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
