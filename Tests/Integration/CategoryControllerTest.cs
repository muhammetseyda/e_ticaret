using Api.Controllers;
using Entities_Genel.ViewModels;
using Entities_MongoDb.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services_MongoDb.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoriesServicesMD> _mockCategoryService;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoriesServicesMD>();
            _controller = new CategoryController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task GetAllCategories_ReturnsOkResult_WithCategories()
        {
            // Arrange
            var categories = new List<MDCategories>
            {
                new MDCategories { CategoryId = 1, Name = "Category 1", Image = "image1.jpg" },
                new MDCategories { CategoryId = 2, Name = "Category 2", Image = "image2.jpg" }
            };
            _mockCategoryService.Setup(repo => repo.GetAllCategoriesAsync()).ReturnsAsync(categories);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseData = Assert.IsAssignableFrom<IEnumerable<MDCategories>>(okResult.Value);
            Assert.Equal(2, responseData.Count());
        }


        [Fact]
        public async Task Create_ValidCategory_ReturnsCreatedResult_WithCategory()
        {
            // Arrange
            var category = new CategoryViewModel { Name = "New Category", Image = "newimage.jpg" };
            var createdCategory = new MDCategories { CategoryId = 1, Name = "New Category", Image = "newimage.jpg" };
            _mockCategoryService.Setup(repo => repo.GetEndCategoryId()).ReturnsAsync(1);
            _mockCategoryService.Setup(repo => repo.CreateCategoriesAsync(It.IsAny<MDCategories>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Create(category);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<MDCategories>(createdResult.Value);
            Assert.Equal("New Category", model.Name);
            Assert.Equal("newimage.jpg", model.Image);
        }

        // Add more test methods as needed
    }
}
