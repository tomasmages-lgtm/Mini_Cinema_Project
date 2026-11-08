using System.ComponentModel.DataAnnotations;

namespace Mini_Cinema.ViewModel.Identity
{
    public class LoginVM
    {
        public string UserNameOrEmail { set; get; }
        [DataType(DataType.Password)]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}
