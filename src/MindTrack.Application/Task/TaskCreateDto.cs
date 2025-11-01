using MindTrack.Domain.Enums;

namespace MindTrack.Application.DTOs.Tasks
{
    public class TaskCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; } = Priority.Medium;
        public TaskState Status { get; set; } = TaskState.Pending;
        public int UserId { get; set; }
    }
}
