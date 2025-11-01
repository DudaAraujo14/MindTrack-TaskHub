using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindTrack.Infrastructure.Persistence;
using MindTrack.Domain.Entities;
using AutoMapper;
using MindTrack.Application.DTOs.Tasks;

namespace MindTrack.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TasksController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET: api/tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TaskReadDto>>(tasks));
        }

        // ✅ GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskReadDto>> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound("Tarefa não encontrada.");
            return Ok(_mapper.Map<TaskReadDto>(task));
        }

        // ✅ GET: api/tasks/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetByUser(int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<TaskReadDto>>(tasks));
        }

        // ✅ POST: api/tasks
        [HttpPost]
        public async Task<ActionResult<TaskReadDto>> Create(TaskCreateDto dto)
        {
            var task = _mapper.Map<TaskItem>(dto);
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<TaskReadDto>(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, result);
        }

        // ✅ PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest("ID da tarefa não corresponde.");

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound("Tarefa não encontrada.");

            _mapper.Map(dto, task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound("Tarefa não encontrada.");

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
