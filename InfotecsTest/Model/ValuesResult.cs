using InfotecsTest.Model.Abstract;
using System.ComponentModel.DataAnnotations;

namespace InfotecsTest.Model
{
    [Display(Name = "Результат мониторинга")]
    public record ValuesResult : BaseResult
    {
        [Required]
        [Display(Name = "Длительность мониторинга")]
        public TimeSpan Duration { get; set; }
        [Required]
        [Display(Name = "Момент начала мониторинга")]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        [Display(Name = "Средняя длительность")]
        public TimeSpan AverageExicutionTime { get; set; }
        [Required]
        [Display(Name = "Среднее значение")]
        public double AverageValue { get; set; }
        [Required]
        [Display(Name = "Медианное значение")]
        public double MedianValie { get; set; }
        [Required]
        [Display(Name = "Максимальное значение")]
        public double MaxValue { get; set; }
        [Required]
        [Display(Name = "Минимальное значение ")]
        public double MinValue { get; set; }

        public ValuesResult(TimeSpan duration, DateTimeOffset startDate, TimeSpan averageExicutionTime,
                            double averageValue, double medianValie, double maxValue, double minValue)
        {
            Duration = duration;
            StartDate = startDate;
            AverageExicutionTime = averageExicutionTime;
            AverageValue = averageValue;
            MedianValie = medianValie;
            MaxValue = maxValue;
            MinValue = minValue;
        }
    }
}
