namespace AutoSchedule.Models.Entity;

public class Load
{
    public long Id { get; set; }
    public string ClassName { get; set; }
    public int Shift { get; set; }
    public int Hours { get; set; }
    public string Lesson { get; set; }
    public string TeacherName { get; set; }
    public long? UserId { get; set; }
    public User User { get; set; }
}
