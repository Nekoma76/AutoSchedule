using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;

namespace AutoSchedule.DAL.Repositories;

public class AudienceRepository : IBaseRepository<Audience>
{
    private readonly ApplicationDbContext _context;

    public AudienceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Audience entity)
    {
        await _context.Audiences.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Audience entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Audience> GetAll()
    {
        return _context.Audiences;
    }

    public async Task<Audience> Update(Audience entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}
