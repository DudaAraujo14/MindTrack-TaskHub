using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindTrack.Infrastructure.Persistence;

namespace MindTrack.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetDashboard(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            // 🧾 Tarefas concluídas
            var tarefasConcluidas = await _context.Tasks
                .CountAsync(t => t.UserId == userId && (int)t.Status == 2); // Status.Done

            // ⏱️ Tempo total focado (somatório das sessões)
            var registrosFoco = await _context.FocusRecords
                .Where(f => f.UserId == userId)
                .ToListAsync();

            var tempoTotalFocado = registrosFoco.Sum(f => (int)(f.End - f.Start).TotalMinutes);

            // 😄 Último humor
            var ultimoHumor = registrosFoco.OrderByDescending(f => f.Id)
                                           .Select(f => f.Mood)
                                           .FirstOrDefault() ?? "Não informado";

            // 📊 Resultado
            var resultado = new
            {
                usuario = user.Name,
                tarefasConcluidas = tarefasConcluidas,
                tempoFocadoMinutos = tempoTotalFocado,
                humorAtual = ultimoHumor,
                pontuacao = (tarefasConcluidas * 10) + (tempoTotalFocado / 10)
            };

            return Ok(resultado);
        }
    }
}
