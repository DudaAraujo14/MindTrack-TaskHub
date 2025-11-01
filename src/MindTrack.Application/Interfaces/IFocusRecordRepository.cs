using MindTrack.Domain.Entities;

namespace MindTrack.Application.Interfaces
{
    public interface IFocusRecordRepository
    {
        Task<IEnumerable<FocusRecord>> GetAllAsync();
        Task<FocusRecord> AddAsync(FocusRecord record);
        Task<IEnumerable<FocusRecord>> GetByUserAsync(int userId);
    }
}
