namespace AutoSchedule.Models.Entity;

public class Audience
{
    public long Id { get; set; }
    public string Number { get; set; }
    public string SeatsCount { get; set; }
    public string Lesson { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
