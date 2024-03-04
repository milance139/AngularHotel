using AngularHotel.Shared.Models.ResponseModels.CurrencyResponse;

namespace AngularHotel.Server.Services.CurrencyService
{
    public interface ICurrencyService
    {
        Task<ServiceResponse<Currency>> CreateCurrency(Currency currency);
        Task<ServiceResponse<Currency>> UpdateCurrency(Currency currency);
        Task<ServiceResponse<List<Currency>>> GetAllCurrencies();
        Task<ServiceResponse<bool>> DeleteCurrency(int currencyId);
        ServiceResponse<TotalPriceInBAMAndEurResponseModel> GetPriceInBAMAndEur(decimal totalPrice, Currency currency);
    }
}
