using InfotecsTest.Data;
using InfotecsTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfotecsTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ResultsController> _logger;

        public ResultsController(
            AppDbContext context,
            ILogger<ResultsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Дополнительный метод для получения данных
        [HttpGet("measurements")]
        public async Task<ActionResult<IEnumerable<Value>>> GetResults(
            [FromQuery] string? fileName,
            [FromQuery] DateTimeOffset? startFrom,
            [FromQuery] DateTimeOffset? startTo,
            [FromQuery] long? minExecutionTime,
            [FromQuery] long? maxExecutionTime,
            [FromQuery] double? maxValue,
            [FromQuery] double? minValue,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 100)
        {
            var query = _context.Results.AsQueryable();

            if (!string.IsNullOrWhiteSpace(fileName))
                query = query.Where(m => m.Report!.FileName == fileName);
            if (startFrom.HasValue)
                query = query.Where(m => m.StartDate >= startFrom.Value);
            if (startTo.HasValue)
                query = query.Where(m => m.StartDate <= startTo.Value);
            if (minExecutionTime.HasValue)
                query = query.Where(m => m.AverageExicutionTime >= TimeSpan.FromSeconds(minExecutionTime.Value));
            if (maxExecutionTime.HasValue)
                query = query.Where(m => m.AverageExicutionTime <= TimeSpan.FromSeconds(maxExecutionTime.Value));
            if (minValue.HasValue)
                query = query.Where(m => m.AverageValue >= minValue.Value);
            if (maxValue.HasValue)
                query = query.Where(m => m.AverageValue <= maxValue.Value);

            var total = await query.CountAsync();
            var data = await query
                .OrderBy(m => m.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", total.ToString());

            return Ok(data);
        }
    }
}
