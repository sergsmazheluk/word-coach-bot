namespace WordCoachBot.Domain
{
    public class Word
    {
        public Guid Id { get; set; }

        // слово на английском
        public string Text { get; set; } = string.Empty;
        // перевод
        public string Translation { get; set; } = string.Empty;
        // пример использования
        public string? Example { get; set; }
    }
}
