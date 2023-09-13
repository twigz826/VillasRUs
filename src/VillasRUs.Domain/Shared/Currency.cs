namespace VillasRUs.Domain.Shared
{
    public record Currency
    {
        public static readonly Currency USD = new("USD");
        public static readonly Currency EUR = new("EUR");
        internal static readonly Currency None = new("");

        private Currency(string code)
        {
            Code = code;
        }

        public string Code { get; init; }

        public static Currency FromCode(string code)
        {
            return All.First(c => c.Code == code) ?? throw new ApplicationException("The currency code is invalid");
        }

        public static readonly IReadOnlyCollection<Currency> All = new[]
        {
            USD,
            EUR
        };
    }
}
