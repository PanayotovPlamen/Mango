using Mango.Services.ShoppingCartAPI.Models.DTO;

namespace Mango.Services.ShoppingCartAPI.Services.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
