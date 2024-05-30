using Mango.Services.ShoppingCartAPI.Models.DTO;

namespace Mango.Services.ShoppingCartAPI.Services.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
