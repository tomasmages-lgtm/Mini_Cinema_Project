using System.ComponentModel.DataAnnotations;

namespace Mini_Cinema.ViewModel.Identity
{
    public class RegisterVM
    {
        public string Name { set; get; }

        public string Address { set; get; }
        public string UserName { set; get; }
        [EmailAddress,DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [DataType(DataType.Password),Compare(nameof(Password))]

        public string ConfirmPassword { set; get; }
    }
}
