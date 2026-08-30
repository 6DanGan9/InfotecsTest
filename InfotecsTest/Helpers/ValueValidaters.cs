using InfotecsTest.Model;

namespace InfotecsTest.Helpers
{
    public static class ValueValidaters
    {
        private static readonly DateTimeOffset MinDate = new DateTime(2000, 1, 1);
        public static Value CheckDate(this Value value)
        {
            if (value.Date < MinDate || value.Date > DateTimeOffset.Now)
                throw new Exception("Дата не может быть позже текущей и раньше 01.01.2000");
            return value;
        }
        public static Value CheckExecutionTime(this Value value)
        {
            if (value.ExecutionTime < TimeSpan.Zero)
                throw new Exception("Время выполнения не может быть меньше 0");
            return value;
        }
        public static Value CheckData(this Value value)
        {
            if (value.Data < 0)
                throw new Exception("Значение показателя не может быть меньше 0");
            return value;
        }
    }
}
