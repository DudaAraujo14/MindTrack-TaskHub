using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindTrack.Infrastructure.Persistence;
using MindTrack.Domain.Entities;
using AutoMapper;
using MindTrack.Application.DTOs.Users;
using MindTrack.Application.DTOs.Tasks;


namespace MindTrack.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        // ✅ GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("Usuário não encontrado.");
            return Ok(_mapper.Map<UserReadDto>(user));
        }

        // ✅ GET: api/users/by-name/{name}
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetByName(string name)
        {
            var users = await _context.Users
                .Where(u => EF.Functions.Like(u.Name.ToLower(), $"%{name.ToLower()}%"))
                .ToListAsync();

            if (!users.Any())
                return NotFound($"Nenhum usuário encontrado contendo o nome '{name}'.");

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        // ✅ GET: api/users/{id}/tasks
        [HttpGet("{id}/tasks")]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetUserTasks(int id)
        {
            var user = await _context.Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            var tasks = _mapper.Map<IEnumerable<TaskReadDto>>(user.Tasks);
            return Ok(tasks);
        }

        // ✅ POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> Create(UserCreateDto dto)
        {
            var user = _mapper.Map<User>(dto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<UserReadDto>(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, result);
        }

        // ✅ PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest("ID do usuário não corresponde.");

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("Usuário não encontrado.");

            _mapper.Map(dto, user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("Usuário não encontrado.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
