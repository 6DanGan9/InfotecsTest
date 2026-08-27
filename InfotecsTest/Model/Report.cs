using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InfotecsTest.Model
{
    [Display(Name = "Отчёт")]
    public class Report
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Имя файла")]
        public string FileName { get; set; }
        [Display(Name = "Интегральные результаты")]
        public Result Result { get; set; }
        [Required]
        [JsonIgnore]
        [ForeignKey(nameof(Result))]
        public Guid Resutl_Id { get; set; }
        [Display(Name = "Измерения")]
        [InverseProperty(nameof(Value.Report))]
        public IList<Value> Values { get; set; }
    }
}
