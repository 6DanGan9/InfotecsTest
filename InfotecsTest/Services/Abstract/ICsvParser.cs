using InfotecsTest.Model.Abstract;

namespace InfotecsTest.Services.Abstract
{
    public interface ICsvParser<T> where T : BaseReport
    {
        Task<T> ParseAsync(Stream fileStream, string fileName);
    }
}
