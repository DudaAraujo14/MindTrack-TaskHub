# 🧠 MindTrack API — Sistema de Produtividade e Foco

Este projeto é uma **API RESTful desenvolvida em .NET 8** que compõe o backend do sistema **MindTrack TaskHub**, responsável por gerenciar **usuários, tarefas e registros de foco**.  
O objetivo é fornecer uma base sólida e escalável para controle de produtividade, com integração ao frontend React e banco de dados SQLite (ou Oracle, se desejado).

---

## 📘 Sumário

## 📘 Sumário

1. [🎯 Objetivo e Escopo](#-objetivo-e-escopo)
2. [🧩 Estrutura da Arquitetura](#-estrutura-da-arquitetura)
3. [⚙️ Tecnologias Utilizadas](#️-tecnologias-utilizadas)
4. [🗂️ Estrutura do Projeto](#️-estrutura-do-projeto)
5. [🧠 Principais Entidades](#-principais-entidades)
6. [🔧 Configuração e Execução](#-configuração-e-execução)
7. [📡 Endpoints da API](#-endpoints-da-api)
8. [💬 Tratamento de Erros e Validações](#-tratamento-de-erros-e-validações)
9. [👥 Autoria](#-autoria)


---

## 🎯 Objetivo e Escopo

A **MindTrack API** fornece serviços para:

- Gerenciar **usuários** e suas informações básicas.  
- Controlar **tarefas (tasks)** com título, prioridade e status.  
- Registrar **sessões de foco (focus records)** associadas a cada usuário.  
- Expor endpoints REST padronizados e documentados via Swagger.  
- Servir como backend do sistema **MindTrack TaskHub**.

---

## 🧩 Estrutura da Arquitetura

O projeto segue o padrão de **Clean Architecture** com separação em camadas:

```
MindTrackAPI/
 ├─ src/
 │   ├─ MindTrack.Domain/         → Entidades e Enums
 │   ├─ MindTrack.Application/    → DTOs, Mapeamentos e Interfaces
 │   ├─ MindTrack.Infrastructure/ → Persistência e Repositórios
 │   └─ MindTrack.Presentation/   → Controllers e Configurações da API
```

---

## ⚙️ Tecnologias Utilizadas

| Categoria | Tecnologia |
|------------|-------------|
| Linguagem | C# (.NET 8) |
| Framework Web | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Banco de Dados | SQLite (ou Oracle) |
| Mapeamento | AutoMapper |
| Documentação | Swagger / Swashbuckle |
| Validação | DataAnnotations |
| Tratamento de Erros | Middleware personalizado |

---

## 🗂️ Estrutura do Projeto

```
src/
 ├─ MindTrack.Domain/
 │   ├─ Entities/
 │   │   ├─ User.cs
 │   │   ├─ TaskItem.cs
 │   │   └─ FocusRecord.cs
 │   └─ Enums/
 │       ├─ TaskState.cs
 │       └─ Priority.cs
 │
 ├─ MindTrack.Application/
 │   ├─ DTOs/
 │   │   ├─ Users/
 │   │   ├─ Tasks/
 │   │   └─ FocusRecords/
 │   ├─ Interfaces/
 │   └─ Mappings/
 │       └─ MappingProfile.cs
 │
 ├─ MindTrack.Infrastructure/
 │   ├─ Persistence/
 │   │   └─ AppDbContext.cs
 │   ├─ Repositories/
 │   └─ ServiceCollectionExtensions.cs
 │
 └─ MindTrack.Presentation/
     ├─ Controllers/
     │   ├─ UsersController.cs
     │   ├─ TasksController.cs
     │   ├─ FocusRecordsController.cs
     │   └─ DashboardController.cs
     ├─ Program.cs
     ├─ appsettings.json
     └─ MindTrack.Presentation.csproj

```

---

## 🧠 Principais Entidades

### 👤 User
```csharp
public class User {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public ICollection<FocusRecord> FocusRecords { get; set; } = new List<FocusRecord>();
}
```

### ✅ TaskItem
```csharp
public class TaskItem {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Medium;
    public TaskState Status { get; set; } = TaskState.Pending;
    public int UserId { get; set; }
    public User? User { get; set; }
}
```

### ⏱️ FocusRecord
```csharp
public class FocusRecord {
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}
```

---

## 🔧 Configuração e Execução

### 1️⃣ Clonar o repositório
```bash
git clone https://github.com/DudaAraujo14/MindTrack-TaskHub.git
cd MindTrackAPI/src/MindTrack.Presentation
```

### 2️⃣ Restaurar pacotes
```bash
dotnet restore
```

### 3️⃣ Aplicar as migrações do banco
```bash
dotnet ef database update -p ../MindTrack.Infrastructure -s .
```

### 4️⃣ Rodar a API
```bash
dotnet run
```

A aplicação iniciará em:
```
http://localhost:5062/swagger
```

---

## 📡 Endpoints da API

| Recurso | Método | Endpoint | Descrição |
|----------|---------|-----------|------------|
| **Users** | GET | `/api/users` | Lista todos os usuários |
|  | POST | `/api/users` | Cria um novo usuário |
|  | PUT | `/api/users/{id}` | Atualiza um usuário |
|  | DELETE | `/api/users/{id}` | Remove um usuário |
| **Tasks** | GET | `/api/tasks` | Lista todas as tarefas |
|  | GET | `/api/tasks/by-user/{id}` | Lista tarefas por usuário |
|  | POST | `/api/tasks` | Cria uma tarefa |
|  | PUT | `/api/tasks/{id}` | Atualiza uma tarefa |
|  | DELETE | `/api/tasks/{id}` | Exclui uma tarefa |
| **FocusRecords** | GET | `/api/focusrecords` | Lista registros de foco |
|  | POST | `/api/focusrecords` | Cria um registro de foco |

---

## 💬 Tratamento de Erros e Validações

A API possui validações automáticas e middleware para erros padronizados.

Exemplo de resposta:
```json
{
  "status": 400,
  "erro": "BadRequest",
  "mensagem": "Um ou mais campos estão inválidos.",
  "detalhes": [
    { "campo": "titulo", "mensagens": ["O título é obrigatório."] }
  ]
}
```

---

## 👥 Autoria

**Maria Eduarda Araujo Penas**  
FIAP — Checkpoint 2 (Back-End)  
📅 2025  
💜 Projeto: *MindTrack TaskHub — Sistema de Produtividade e Foco*
