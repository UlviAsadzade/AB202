using PustokAB202.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace PustokAB202.ViewModels
{
    public class RegisterUserVM
    {
        //custom columns
        [Required]
        [MaxLength(25,ErrorMessage = "Name length cant be longer than 25!")]
        [MinLength(3, ErrorMessage = "Name length cant be short than 3!")]
        public string Name { get; set; } 
        
        [Required]
        [MaxLength(50,ErrorMessage = "Surname length cant be longer than 50!")]
        [MinLength(3, ErrorMessage = "Surname length cant be short than 3!")]
        public string Surname { get; set; }

        public Gender Gender { get; set; }

        //package columns
        [Required]
        [DataType(DataType.EmailAddress)]
        [MinLength(6, ErrorMessage = "Email length cant be short than 6!")]
        [MaxLength(50, ErrorMessage = "Email length cant be longer than 50!")]


        public string Email { get; set; }

        [Required]

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password length cant be short than 8!")]
        [MaxLength(50, ErrorMessage = "Password length cant be longer than 50!")]


        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password length cant be short than 8!")]
        [MaxLength(50, ErrorMessage = "Password length cant be longer than 50!")]
        [Compare(nameof(Password))]

        public string ConfirmPassword { get; set; }





    }
}
