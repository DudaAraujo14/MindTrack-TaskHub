namespace MindTrack.Application.DTOs.FocusRecords
{
    public class FocusRecordReadDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int DurationMinutes { get; set; }
        public string Mood { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
