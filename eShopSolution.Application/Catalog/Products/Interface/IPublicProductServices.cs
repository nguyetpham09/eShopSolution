using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products.Interface
{
    public interface IPublicProductServices
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);
    }
}
