using Microsoft.EntityFrameworkCore;
using MindTrack.Application.Interfaces;
using MindTrack.Domain.Entities;
using MindTrack.Infrastructure.Persistence;

namespace MindTrack.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;
        public UserRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _ctx.Users.AsNoTracking().ToListAsync();

        public async Task<User?> GetByIdAsync(int id) =>
            await _ctx.Users
                      .Include(u => u.Tasks)
                      .Include(u => u.FocusRecords)
                      .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> AddAsync(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _ctx.Entry(user).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.Users.FindAsync(id);
            if (entity is null) return;
            _ctx.Users.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
