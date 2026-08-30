using InfotecsTest.Model;
using InfotecsTest.Services.Values.Abstract;

namespace InfotecsTest.Services.Values
{
    public class ValuesCsvMetricCalculator : IValuesCsvMetricCalculator
    {
        private List<double> _values;

        private DateTimeOffset _minDate = DateTimeOffset.MaxValue;
        private DateTimeOffset _maxDate = DateTimeOffset.MinValue;
        private TimeSpan _summTime = TimeSpan.Zero;
        private double _summValue = 0;
        private double _minValue = double.MaxValue;
        private double _maxValue = double.MinValue;

        public ValuesCsvMetricCalculator()
        {
            _values = new();
        }

        public ValuesCsvMetricCalculator(int count)
        {
            _values = new(count);
        }
        public void AddValue(Value value)
        {
            _values.Add(value.Data);

            _summTime += value.ExecutionTime;
            _summValue += value.Data;

            if (value.Date < _minDate) _minDate = value.Date;
            if (value.Date > _maxDate) _maxDate = value.Date;
            if (value.Data < _minValue) _minValue = value.Data;
            if (value.Data > _maxValue) _maxValue = value.Data;
        }

        public ValuesResult GetResult()
        {
            var duration = _maxDate - _minDate;
            var averageExicutionTime = _summTime / _values.Count;
            var averageValue = _summValue / _values.Count;

            _values.Sort();
            int mid = _values.Count / 2;

            double median = _values.Count % 2 != 0 ?
                            _values[mid] :
                            (_values[mid - 1] + _values[mid]) / 2.0;

            return new ValuesResult(duration, _minDate, averageExicutionTime, averageValue, median, _maxValue, _minValue);
        }
    }
}
