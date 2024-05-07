using Talabat.Core.Entities.Basket;


namespace Talabat.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string basketId);
        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        public Task<bool> DeleteBasketAsync(string BasketId);
    }
}
