using System;

namespace MindTrack.Domain.Entities
{
    public class FocusRecord
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }  // início da sessão
        public DateTime End   { get; set; }  // fim da sessão
        public string Mood    { get; set; } = "Neutral";

        public int DurationMinutes => (int)(End - Start).TotalMinutes;

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
