using AngularHotel.Shared.Models.ResponseModels.CurrencyResponse;

namespace AngularHotel.Server.Services.CurrencyService
{
    public class CurrencyService : ICurrencyService
    {
        private readonly DataContext _context;
        public decimal EurRate { get; private set; }
        public decimal BamRate { get; private set; }

        public CurrencyService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

            InitializeExchangeRates().Wait();
        }
        private async Task InitializeExchangeRates()
        {
            EurRate = await _context.Currencies
                .Where(c => c.Name == "EUR")
                .Select(c => c.Price)
                .FirstOrDefaultAsync();

            BamRate = await _context.Currencies
                .Where(c => c.Name == "BAM")
                .Select(c => c.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<ServiceResponse<Currency>> CreateCurrency(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Currency> { Data = currency, Message = "Currency created successfully", Success = true };
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

            _context.Currencies.Remove(dbCurrency);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true, Message = "Currency deleted successfully", Success = true };
        }

        public async Task<ServiceResponse<List<Currency>>> GetAllCurrencies()
        {
            var currencies = await _context.Currencies.ToListAsync();

            return new ServiceResponse<List<Currency>> { Data = currencies };
        }

        public ServiceResponse<TotalPriceInBAMAndEurResponseModel> GetPriceInBAMAndEur(decimal totalPrice, Currency currency)
        {
            decimal totalPriceBam = (decimal)totalPrice * (currency.Price / BamRate);
            decimal totalPriceEur = (decimal)totalPrice * (currency.Price / EurRate);


            var response = new TotalPriceInBAMAndEurResponseModel
            {
                TotalPriceBam = totalPriceBam,
                TotalPriceEur = totalPriceEur
            };

            var serviceResponse = new ServiceResponse<TotalPriceInBAMAndEurResponseModel>
            {
                Data = response,
                Success = true,
                Message = "Total price in BAM and EUR calculated successfully."
            };

            return serviceResponse;
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

            return new ServiceResponse<Currency> { Data = dbCurrency, Message = "Currency updated successfully", Success = true };
        }

    
    }
}
