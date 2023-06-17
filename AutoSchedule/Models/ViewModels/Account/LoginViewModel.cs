using System.ComponentModel.DataAnnotations;

namespace AutoSchedule.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        public long Id {get; set; }
        
        [Required(ErrorMessage = "Введите логин")]
        [MaxLength(20, ErrorMessage = "Логин должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должно иметь длину больше 3 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
