namespace MindTrack.Application.DTOs.FocusRecords
{
    public class FocusRecordCreateDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Mood { get; set; } = "Neutral";
        public int UserId { get; set; }
    }
}
