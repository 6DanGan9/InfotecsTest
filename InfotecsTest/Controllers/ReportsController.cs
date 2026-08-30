using InfotecsTest.Data;
using InfotecsTest.Model;
using InfotecsTest.Services.Values.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InfotecsTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValuesCsvParser _csvParser;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(
            AppDbContext context,
            IValuesCsvParser csvParser,
            ILogger<ReportsController> logger)
        {
            _context = context;
            _csvParser = csvParser;
            _logger = logger;
        }

        [HttpPost("upload")]
        [RequestSizeLimit(10 * 1024 * 1024)] // 10 MB
        public async Task<ActionResult> UploadCsv(IFormFile file)
        {
            var stopwatch = Stopwatch.StartNew();

            // Проверка файла
            if (file == null || file.Length == 0)
                return BadRequest("Файл отсутствует или пуст");

            // Проверка расширения
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (extension != ".csv")
            {
                return BadRequest("Файл не является CSV");
            }

            ValuesReport report;

            try
            {
                // Парсим CSV
                using var stream = file.OpenReadStream();
                report = await _csvParser.ParseAsync(stream, file.FileName);
                if (report.Result == null)
                    throw new Exception("Ошибка получения результата");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            // Сохраняем в базу данных
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                // Используем BulkInsert для производительности (если много записей)
                if (report.Measurements.Count() > 1000)
                {
                    await _context.Values.AddRangeAsync(report.Measurements.OfType<Value>());
                }
                else
                {
                    // По-одной для маленьких файлов
                    foreach (var record in report.Measurements)
                    {
                        _context.Values.Add((Value)record);
                    }
                }
                _context.ValuesReports.Add(report);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Ошибка при загрузке в бд");
                return StatusCode(500, $"Database error: {ex.Message}");
            }
        }

        // Дополнительный метод для получения данных
        [HttpGet("measurements")]
        public async Task<ActionResult<IEnumerable<Value>>> GetMeasurements(
            [FromQuery] DateTimeOffset? from,
            [FromQuery] DateTimeOffset? to,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 100)
        {
            var query = _context.Values.AsQueryable();

            if (from.HasValue)
                query = query.Where(m => m.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(m => m.Date <= to.Value);

            var total = await query.CountAsync();
            var data = await query
                .OrderBy(m => m.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", total.ToString());

            return Ok(data);
        }

        // Дополнительный метод для получения данных
        [HttpGet("lastMeasurements")]
        public async Task<ActionResult<IEnumerable<Value>>> GetLastMeasurements(
            [FromQuery] string fileName)
        {
            var query = _context.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(fileName))
                query = query.Where(m => m.Report!.FileName == fileName);

            var total = await query.CountAsync();
            var data = await query
                .OrderBy(m => m.Date)
                .Skip(total > 10? total - 10 : 0)
                .Take(10)
                .ToListAsync();

            return Ok(data);
        }
    }
}
