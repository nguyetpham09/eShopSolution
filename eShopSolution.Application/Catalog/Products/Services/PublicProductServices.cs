using eShopSolution.Application.Catalog.Products.Interface;
using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products.Services
{
    public class PublicProductServices : IPublicProductServices
    {
        private readonly EShopDbContext _context;

        public PublicProductServices(EShopDbContext context)
        {
            _context = context;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request)
        {
            //Select
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };

            //Filters
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //Paging 
            int totalRaw = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    Name = x.pt.Name,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();
            //select and projection
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecords = totalRaw,
                Items = data
            };
            return pageResult;
        }
    }
}
