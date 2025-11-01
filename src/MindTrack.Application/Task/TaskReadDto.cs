using MindTrack.Domain.Enums;

namespace MindTrack.Application.DTOs.Tasks
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public TaskState Status { get; set; }
        public int UserId { get; set; }
    }
}
