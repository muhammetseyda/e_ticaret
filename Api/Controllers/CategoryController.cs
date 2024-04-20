using Entities_Genel.ViewModels;
using Entities_MongoDb.Models;
using Microsoft.AspNetCore.Mvc;
using Services_MongoDb.Abstract;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesServicesMD _categoriesServicesMD;

        public CategoryController(ICategoriesServicesMD categoriesServicesMD)
        {
            _categoriesServicesMD = categoriesServicesMD;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var categories = await _categoriesServicesMD.GetAllCategoriesAsync();
                return Ok(new { success = true, data=categories });
            }
            catch (Exception ex)
            {
                return BadRequest(new {success =false,message = ex.Message});
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CategoryViewModel category)
        {
            try
            {
                MDCategories mdcategory = new MDCategories();
                mdcategory.Name = category.Name;
                mdcategory.Image = category.Image;

                mdcategory.CategoryId = await _categoriesServicesMD.GetEndCategoryId();
                var result = await _categoriesServicesMD.CreateCategoriesAsync(mdcategory);
                if (result)
                {
                    return Created("", new { success = true, message = "Kategori oluşturuldu." , category=mdcategory });

                }
                else
                {
                    return BadRequest(new {success=false,message="Kategori oluşturulamadı."});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500,new {success = false, message = ex.Message});
            }
        }

        [HttpGet("getonecategory")]
        public async Task<ActionResult> GetOneCategory(int id)
        {
            try
            {
                var category = await _categoriesServicesMD.GetCategoryById(id);
                if(category == null)
                {
                    return NotFound(new {success = false, message="Kategori bulunamadı."});
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCategoryByIdAsync(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest(new {success = false, message = "Categori bulunamadı."});
                }
                await _categoriesServicesMD.DeleteCategoriesByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

    }
}
