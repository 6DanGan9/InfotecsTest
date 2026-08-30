using InfotecsTest.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfotecsTest.Model
{
    [Table("Values")]
    [Display(Name = "Результат мониторинга")]
    public class Value : BaseMeasurement
    {
        [Required]
        [Display(Name = "Время начала")]
        public DateTimeOffset Date { get; set; }
        [Required]
        [Display(Name = "Длительность")]
        public TimeSpan ExecutionTime { get; set; }
        [Required]
        [Display(Name = "Значение")]
        public double Data { get; set; }

        public Value(DateTimeOffset date, TimeSpan executionTime, double data)
        {
            Date = date;
            ExecutionTime = executionTime;
            Data = data;
        }
    }
}
