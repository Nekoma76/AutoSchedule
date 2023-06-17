using AutoSchedule.Models.Enum;

namespace AutoSchedule.Models.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Profile Profile { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Audience> Audiences { get; set; }
        public List<Load> Loads { get; set; }
    }
}
