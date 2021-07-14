using eShopSolution.Application.Catalog.Products.Dtos;
using eShopSolution.Application.Catalog.Products.Dtos.Public;
using eShopSolution.Application.Catalog.Products.Interface;
using eShopSolution.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.Catalog.Products.Services
{
    public class PublicProductServices : IPublicProductServices
    {
        public PageResult<ProductViewModel> GetAllByCategoryId(GetProductPagingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
