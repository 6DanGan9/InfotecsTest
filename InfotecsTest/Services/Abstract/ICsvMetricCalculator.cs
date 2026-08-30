using InfotecsTest.Model.Abstract;

namespace InfotecsTest.Services.Abstract
{
    public interface ICsvMetricCalculator<V, R> where V : BaseMeasurement where R : BaseResult
    {
        public void AddValue(V value);
        public R GetResult();
    }
}
