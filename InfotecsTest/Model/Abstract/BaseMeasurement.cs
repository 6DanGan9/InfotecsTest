using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InfotecsTest.Model.Abstract
{
    public abstract class BaseMeasurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [JsonIgnore]
        public BaseReport? Report { get; set; }
        [Required]
        [JsonIgnore]
        [ForeignKey(nameof(Report))]
        public Guid Report_Id { get; set; }
    }
}
