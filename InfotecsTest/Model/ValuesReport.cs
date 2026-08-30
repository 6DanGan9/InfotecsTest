using InfotecsTest.Model.Abstract;
using System.ComponentModel.DataAnnotations;

namespace InfotecsTest.Model
{
    [Display(Name = "Отчёт")]
    public class ValuesReport : BaseReport
    {
        public ValuesReport(string fileName)
        {
            FileName = fileName;
        }
    }
}
