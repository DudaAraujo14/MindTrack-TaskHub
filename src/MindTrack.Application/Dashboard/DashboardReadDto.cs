namespace MindTrack.Application.DTOs.Dashboard
{
    public class DashboardReadDto
    {
        public string Usuario { get; set; } = string.Empty;
        public int TarefasConcluidas { get; set; }
        public int TempoFocadoMinutos { get; set; }
        public string HumorAtual { get; set; } = string.Empty;
        public int Pontuacao { get; set; }
    }
}
