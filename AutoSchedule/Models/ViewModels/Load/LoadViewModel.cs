namespace AutoSchedule.Models.ViewModels.Load;

public class LoadViewModel
{
    public long Id { get; set; }
    public string ClassName { get; set; }
    public int Shift { get; set; }
    public int Hours { get; set; }
    public string Lesson { get; set; }
    public string TeacherName { get; set; }
    public long? UserId { get; set; }
}
