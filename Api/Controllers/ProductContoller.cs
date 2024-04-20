using Entities_Genel.ViewModels;
using Entities_MongoDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Services_MongoDb.Abstract;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductContoller : ControllerBase
    {
        private readonly IProductsServicesMD _productsServicesMD;

        public ProductContoller(IProductsServicesMD productsServicesMD)
        {
            _productsServicesMD = productsServicesMD;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var products = await _productsServicesMD.GetAllProductsAsync();
                if(products == null) 
                {
                    return NotFound(new {success = false, message="Ürün bulunamadı"});
                }
                return Ok(new { success = true, data= products });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductsViewModel model)
        {
            try
            {
                MDProducts product = new MDProducts();
                product.productId = await _productsServicesMD.GetEndProductId();
                product.title = model.Title;
                product.description = model.Description;
                product.category = model.Category;
                product.rating = model.Rating;
                product.price = model.Price;
                product.discountPercentage = model.DiscountPercentage;
                product.stock = model.Stock;
                product.brand = model.Brand;
                product.thumbnail = model.Thumbnail;
                product.images = model.Images;

                var result = await _productsServicesMD.CreateProductAsync(product);
                if (result)
                {
                    return Created("", new { success = true, message = "Ürün oluşturuldu.", data = product });
                }
                return BadRequest(new {success = false, message="Ürün oluşturulamadı."});
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { success = false, message = ex.Message });
            }
        }
    }
}
