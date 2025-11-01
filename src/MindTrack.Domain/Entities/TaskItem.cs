using System.ComponentModel.DataAnnotations;
using MindTrack.Domain.Enums;

namespace MindTrack.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter até 100 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "A descrição deve ter até 255 caracteres.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "A prioridade deve ser informada.")]
        public Priority Priority { get; set; } = Priority.Medium;

        [Required(ErrorMessage = "O status da tarefa deve ser informado.")]
        public TaskState Status { get; set; } = TaskState.Pending;

        [Required(ErrorMessage = "O campo UserId é obrigatório.")]
        public int UserId { get; set; }

        // 🔹 Relacionamento reverso (muitas tarefas → 1 usuário)
        public User? User { get; set; }
    }
}
