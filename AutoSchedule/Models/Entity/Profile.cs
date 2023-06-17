namespace AutoSchedule.Models.Entity
{
    public class Profile
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public long PhoneNumber { get; set; }
        public string Address { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
