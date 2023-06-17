using AutoSchedule.DAL.Interfaces;
using AutoSchedule.Models.Entity;

namespace AutoSchedule.DAL.Repositories;

public class ProfileRepository : IBaseRepository<Profile>
{
    private readonly ApplicationDbContext _context;

    public ProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Profile entity)
    {
        await _context.Profiles.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Profile entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Profile> GetAll()
    {
        return _context.Profiles;
    }

    public async Task<Profile> Update(Profile entity)
    {
        _context.Profiles.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    } 
}
