﻿using eShopSolution.Application.Catalog.Products.Interface;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductServices _publicProductServices;
        private readonly IManageProductServices _manageProductServices;
        public ProductsController(IPublicProductServices publicProductServices, IManageProductServices manageProductServices)
        {
            _publicProductServices = publicProductServices;
            _manageProductServices = manageProductServices;
        }

        [HttpGet("product-paging/{languageId}")]
        public async Task<IActionResult> Get([FromQuery]string languageId, GetPublicProductPagingRequest request)
        {
            var products = await _publicProductServices.GetAllByCategoryId(languageId, request);
            return Ok(products);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductServices.GetById(productId, languageId);
            if (product == null) return BadRequest("Cannot find product");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productId = await _manageProductServices.Create(request);

            if (productId == 0)
            {
                return BadRequest("Cannot create product");
            }

            var product = _manageProductServices.GetById(productId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update ([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var affectedResult = await _manageProductServices.Update(request);

            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageProductServices.Delete(productId);
            if (affectedResult == 0) return BadRequest();
            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice (int productId, decimal newPrice)
        {
            var isSuccessful = await _manageProductServices.UpdatePrice(productId, newPrice);
            if (isSuccessful) return Ok();
            return BadRequest();
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById (int imageId)
        {
            var image = await _manageProductServices.GetImageById(imageId);
            if (image == null) return BadRequest($"Cannot find image with id {imageId}");
            return Ok(image);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> Create(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageId = await _manageProductServices.AddImage(productId, request);

            if (imageId == 0)
            {
                return BadRequest("Cannot create image");
            }

            var image = _manageProductServices.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = image.Id }, image);
        }
        
        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var affectedResult = await _manageProductServices.UpdateImage(imageId, request);

            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
        
        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var affectedResult = await _manageProductServices.RemoveImage(imageId);
            if (affectedResult == 0) return BadRequest();
            return Ok();
        }
    }
}