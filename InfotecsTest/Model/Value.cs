using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InfotecsTest.Model
{
    [Display(Name = "Результат мониторинга")]
    public class Value
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Время начала")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Длительность")]
        public TimeSpan ExecutionTime { get; set; }
        [Required]
        [Display(Name = "Значение")]
        public double Data { get; set; }
        [JsonIgnore]
        public Report Report { get; set; }
        [Required]
        [JsonIgnore]
        [ForeignKey(nameof(Report))]
        public Guid Report_Id { get; set; }
    }
}
