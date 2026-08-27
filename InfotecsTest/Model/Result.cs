using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InfotecsTest.Model
{
    [Display(Name = "Результат мониторинга")]
    public record Result
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Длительность мониторинга")]
        public TimeSpan Duration { get; set; }
        [Required]
        [Display(Name = "Момент начала мониторинга")]
        public DateTime StartDate { get; set; }
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
        [JsonIgnore]
        public Report Report { get; set; }
        [Required]
        [JsonIgnore]
        [ForeignKey(nameof(Report))]
        public Guid Report_Id { get; set; }
    }
}
