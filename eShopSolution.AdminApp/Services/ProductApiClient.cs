﻿using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {

        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            :base(httpClientFactory, configuration, httpContextAccessor)
        {

        }

        public async Task<PageResult<ProductViewModel>> GetPaging(GetManageProductPagingRequest request)
        {
            return await GetAsync<PageResult<ProductViewModel>>($"/api/products/paging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}" +
                $"&languageId={request.LanguageId}");
        }
    }
}
