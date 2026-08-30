using CsvHelper;
using CsvHelper.Configuration;
using InfotecsTest.Model;
using System.Globalization;
using InfotecsTest.Helpers;
using InfotecsTest.Services.Values.Abstract;

namespace InfotecsTest.Services.Values
{
    public class ValuesCsvParser : IValuesCsvParser
    {
        private readonly ILogger<ValuesCsvParser> _logger;
        private readonly IValuesCsvMetricCalculator _calculator;

        public ValuesCsvParser(ILogger<ValuesCsvParser> logger, IValuesCsvMetricCalculator calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }

        public async Task<ValuesReport> ParseAsync(Stream fileStream, string fileName)
        {
            IEnumerable<Value> validValues;
            var error = false;
            var report = new ValuesReport(fileName);

            if (string.IsNullOrEmpty(report.FileName))
                throw new Exception("Недопустимое имя файла");

            try
            {
                using var reader = new StreamReader(fileStream);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ";",
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null,
                    BadDataFound = context =>
                    {
                        error = true;
                    }
                });

                if (error)
                    throw new Exception("Содержимое файла повреждено");

                validValues = await Task.Run(() => ProcessCsv(csv, report));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при парсинге CSV файла");
                throw;
            }

            report.Result = _calculator.GetResult();

            return report;
        }

        /// <summary>
        /// Обработка csv файла.
        /// </summary>
        private IEnumerable<Value> ProcessCsv(CsvReader reader, ValuesReport report)
        {
            var lineNumber = 0;
            var validValues = new List<Value>();

            // Читаем записи
            var records = reader.GetRecords<dynamic>();

            foreach (var record in records)
            {
                lineNumber++;
                if (lineNumber > 10_000)
                    throw new Exception("Кол-во записей превышает 10_000");
                try
                {
                    // Получаем значения как строки
                    var rawDate = record.Date?.ToString();
                    var rawExecutionTime = record.ExecutionTime?.ToString();
                    var rawValue = record.Value?.ToString();

                    Value validValue = ProcessLine(rawDate, rawExecutionTime, rawValue);

                    validValue.Report = report;
                    report.Measurements.Add(validValue);

                    _calculator.AddValue(validValue);
                    validValues.Add(validValue);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка в строке{lineNumber}: {ex.Message}");
                }
            }

            return validValues;
        }

        /// <summary>
        /// Обработка строки файла.
        /// </summary>
        private static Value ProcessLine(string rawDate, string rawExTime, string rawData)
        {
            // Проверяем пустые поля
            if (string.IsNullOrEmpty(rawDate) ||
                string.IsNullOrEmpty(rawExTime) ||
                string.IsNullOrEmpty(rawData))
            {
                throw new Exception("Пустое поле");
            }

            // Парсим дату (формат: ГГГГ-ММ-ДДTчч-мм-сс.ммммZ)
            if (!DateTimeOffset.TryParseExact(
                rawDate,
                "yyyy-MM-ddTHH:mm:ss.ffffZ",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal,
                out var date))
            {
                throw new Exception("Ошибка парсинга даты");
            }

            // Парсим время выполнения
            if (!uint.TryParse(rawExTime, out var exTime))
                throw new Exception("Ошибка парсинга длительности");

            // Парсим значение
            if (!double.TryParse(rawData, out var data))
                throw new Exception("Ошибка парсинга значения");

            var value = new Value(date.ToUniversalTime(), TimeSpan.FromSeconds(exTime), data);

            // Проверяем корректность данных
            value.CheckDate()
                    .CheckExecutionTime()
                    .CheckData();

            return value;
        }
    }
}
