using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;

namespace AutoSchedule.DAL.Repositories;

public class LoadRepository : IBaseRepository<Load>
{
    private readonly ApplicationDbContext _context;

    public LoadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Load entity)
    {
        await _context.Loads.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Load entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Load> GetAll()
    {
        return _context.Loads;
    }

    public async Task<Load> Update(Load entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
