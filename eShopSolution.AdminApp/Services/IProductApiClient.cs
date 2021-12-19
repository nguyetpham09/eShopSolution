using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<PageResult<ProductViewModel>> GetPaging(GetManageProductPagingRequest request);
        Task<bool> CreateProduct(ProductCreateRequest request);
    }
}
