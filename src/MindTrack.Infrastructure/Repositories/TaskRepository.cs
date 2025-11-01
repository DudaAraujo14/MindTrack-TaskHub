using Microsoft.EntityFrameworkCore;
using MindTrack.Application.Interfaces;
using MindTrack.Domain.Entities;
using MindTrack.Infrastructure.Persistence;

namespace MindTrack.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _ctx;
        public TaskRepository(AppDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
            await _ctx.Tasks.AsNoTracking().ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(int id) =>
            await _ctx.Tasks.FindAsync(id);

        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            _ctx.Tasks.Add(task);
            await _ctx.SaveChangesAsync();
            return task;
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _ctx.Entry(task).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _ctx.Tasks.FindAsync(id);
            if (e is null) return;
            _ctx.Tasks.Remove(e);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetByUserAsync(int userId) =>
            await _ctx.Tasks.AsNoTracking()
                    .Where(t => t.UserId == userId).ToListAsync();
    }
}
