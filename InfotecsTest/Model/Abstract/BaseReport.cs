using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InfotecsTest.Model.Abstract
{
    public abstract class BaseReport
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(127)]
        [Display(Name = "Имя файла")]
        public string? FileName { get; set; }
        [Display(Name = "Интегральные результаты")]
        public BaseResult? Result { get; set; }
        [InverseProperty(nameof(BaseMeasurement.Report))]
        public IList<BaseMeasurement> Measurements { get; set; } = [];
    }
}
