using System.ComponentModel.DataAnnotations;

namespace FruitSimulation.ViewModels
{
    public class LoginVM
    {
        public string UsernameOrEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
