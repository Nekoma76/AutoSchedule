using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;

namespace AutoSchedule.DAL.Repositories
{
    public class TeacherRepository : IBaseRepository<Teacher>
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Teacher entity)
        {
            await _context.Teachers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Teacher entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Teacher> GetAll()
        {
            return _context.Teachers;
        }

        public async Task<Teacher> Update(Teacher entity)
        {
            _context.Teachers.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
