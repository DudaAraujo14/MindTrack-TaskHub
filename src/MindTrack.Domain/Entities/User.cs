using System.ComponentModel.DataAnnotations;

namespace MindTrack.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
        public string Email { get; set; } = string.Empty;

        // 🔹 Relação 1:N — um usuário pode ter várias tarefas
        public List<TaskItem> Tasks { get; set; } = new();

        // 🔹 Relação 1:N — um usuário pode ter vários registros de foco
        public List<FocusRecord> FocusRecords { get; set; } = new();
    }
}
