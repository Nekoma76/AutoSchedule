namespace AutoSchedule.Models.ViewModels.Audience;
using System.ComponentModel.DataAnnotations;

public class AudienceViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Введите номер кабинета")]
    public string Number { get; set; }

    [Required(ErrorMessage = "Введите количество кабинетов")]
    public string SeatsCount { get; set; }
    
    [Required(ErrorMessage = "Введите предмет кабинета")]
    public string Lesson { get; set; }
    public long UserId { get; set; }
}
