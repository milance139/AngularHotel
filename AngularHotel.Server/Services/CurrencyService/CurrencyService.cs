
namespace AngularHotel.Server.Services.CurrencyService
{
    public class CurrencyService : ICurrencyService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrencyService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Currency>> CreateCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Currency> { Data = currency };
        }

        public async Task<ServiceResponse<bool>> DeleteCurrency(int currencyId)
        {
            var dbCurrency = await _context.Currencies.FindAsync(currencyId);
            if (dbCurrency == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Currency not found",
                    Success = false
                };
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Currency>>> GetAllCurrencies()
        {
            var currencies = await _context.Currencies.ToListAsync();

            return new ServiceResponse<List<Currency>> { Data = currencies };
        }

        public async Task<ServiceResponse<Currency>> UpdateCurrency(Currency currency)
        {
            var dbCurrency = await _context.Currencies.FindAsync(currency.Id);

            if (dbCurrency == null)
            {
                return new ServiceResponse<Currency>
                {
                    Message = "Currency not found",
                    Success = false
                };
            }

            dbCurrency.Price = currency.Price;
            dbCurrency.Name = currency.Name;

            await _context.SaveChangesAsync();

            return new ServiceResponse<Currency> { Data = dbCurrency };
        }
    }
}
