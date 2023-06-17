using System.ComponentModel.DataAnnotations;
using AutoSchedule.Models.Entity;

namespace AutoSchedule.Models.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите учебное заведение")]
        [MaxLength(100, ErrorMessage = "Учебное заведение должно иметь длину меньше 100 символов")]
        [MinLength(3, ErrorMessage = "Учебное заведение должно иметь длину больше 3 символов")]
        public string Organization { get; set; }
        
        [Required(ErrorMessage = "Введите номер телефона")]
        public long PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите адресс")]
        public string Address { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
    }
}
