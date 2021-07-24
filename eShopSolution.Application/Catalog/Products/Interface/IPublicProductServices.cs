using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products.Interface
{
    public interface IPublicProductServices
    {
        Task<List<ProductViewModel>> GetAll();
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
    }
}
