using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindTrack.Infrastructure.Persistence;
using MindTrack.Domain.Entities;
using AutoMapper;
using MindTrack.Application.DTOs.FocusRecords;

namespace MindTrack.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FocusRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FocusRecordsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/focusrecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FocusRecordReadDto>>> GetAll()
        {
            var records = await _context.FocusRecords.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<FocusRecordReadDto>>(records));
        }

        // GET: api/focusrecords/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<FocusRecordReadDto>>> GetByUser(int userId)
        {
            var records = await _context.FocusRecords
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<FocusRecordReadDto>>(records));
        }

        // POST: api/focusrecords
        [HttpPost]
        public async Task<ActionResult<FocusRecordReadDto>> Create(FocusRecordCreateDto dto)
        {
            var record = _mapper.Map<FocusRecord>(dto);
            _context.FocusRecords.Add(record);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<FocusRecordReadDto>(record);
            return CreatedAtAction(nameof(GetAll), new { id = record.Id }, result);
        }

        // DELETE: api/focusrecords/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.FocusRecords.FindAsync(id);
            if (record == null) return NotFound();

            _context.FocusRecords.Remove(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
