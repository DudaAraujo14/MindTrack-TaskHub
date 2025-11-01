using Microsoft.EntityFrameworkCore;
using MindTrack.Application.Interfaces;
using MindTrack.Domain.Entities;
using MindTrack.Infrastructure.Persistence;

namespace MindTrack.Infrastructure.Repositories
{
    public class FocusRecordRepository : IFocusRecordRepository
    {
        private readonly AppDbContext _context;
        public FocusRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FocusRecord>> GetAllAsync()
        {
            return await _context.FocusRecords.AsNoTracking().ToListAsync();
        }

        public async Task<FocusRecord> AddAsync(FocusRecord record)
        {
            if (record.Start == default)
                record.Start = DateTime.Now;

            _context.FocusRecords.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<IEnumerable<FocusRecord>> GetByUserAsync(int userId)
        {
            return await _context.FocusRecords
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
    }
}
