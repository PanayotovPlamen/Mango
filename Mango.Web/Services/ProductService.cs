using Mango.Web.Models.DTO;
using Mango.Web.Services.IService;
using Mango.Web.Utility;
using Mango.Web.Utility.Enums;

namespace Mango.Web.Services
{
	public class ProductService : IProductService
	{
		private readonly IBaseService _baseService;
		public ProductService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> CreateProductsAsync(ProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Data = productDto,
				Url = SD.ProductAPIBase + "/api/product",
				ContentType = ContentType.MultipartFormData
			});
		}

		public async Task<ResponseDto?> DeleteProductsAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.DELETE,
				Url = SD.ProductAPIBase + "/api/product/" + id
			});
		}

		public async Task<ResponseDto?> GetAllProductsAsync()
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.ProductAPIBase + "/api/product"
			});
		}



		public async Task<ResponseDto?> GetProductByIdAsync(int id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.ProductAPIBase + "/api/product/" + id
			});
		}

		public async Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = ApiType.PUT,
				Data = productDto,
				Url = SD.ProductAPIBase + "/api/product",
				ContentType = ContentType.MultipartFormData
			});
		}

	}
}
