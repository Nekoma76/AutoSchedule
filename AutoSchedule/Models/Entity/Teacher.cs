namespace AutoSchedule.Models.Entity
{
    public class Teacher
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Lesson { get; set; }
        public long? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
