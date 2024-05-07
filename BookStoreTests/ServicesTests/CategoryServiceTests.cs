using Contracts.RepositoryContracts;
using Contracts.ServicesContracts;
using Domain.Entities;
using Domain.ErrorHandling;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTests.ServicesTests
{
    /// <summary>
    /// Represents tests for category service
    /// </summary>
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly ICategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object);
        }

        #region Tests for adding category

        [Fact]
        public async void AddCategory_NullCategoryObject_NoArgumentError()
        {
            //Arrange
            Category category = null;

            //Act and Assert
            await Assert.ThrowsAsync<NullArgumentException>(async () => { 
                await _categoryService.AddAsync(category); 
            });
        }

        [Theory]
        [InlineData("", 1000000)]
        [InlineData(null, 0)]
        [InlineData("sdfsdfasdasdadasdadadadad", 5)]
        [InlineData("Horror", 0)]
        public async void AddCategory_InvalidCategoryObject_ValidationError(string name, int order)
        {
            //Arrange
            Category category = new Category() { Name = name, DisplayOrder = order };

            //Act and Assert
            await Assert.ThrowsAsync<ModelValidationException>(async () => 
            { 
                await _categoryService.AddAsync(category); 
            });
        }

        [Fact]
        public async void AddCategory_ExistingCategory_UniqueValidationError()
        {
            // Arrange
            Category category = new Category() { Name = "Peter Parker", DisplayOrder = 1 };
            _categoryRepositoryMock.Setup(x => x.FindOneByNameAsync(It.IsAny<string>())).ReturnsAsync(category);

            // Act and Assert
            await Assert.ThrowsAsync<UniqueValidationException>(async () =>
            {
                await _categoryService.AddAsync(category);
            });
        }

        [Theory]
        [InlineData("Peter Parker", 1)]
        public async void AddCategory_ValidCategoryObject_CategoryObject(string name, int order)
        {
            // Arrange
            Category category = new Category() { Name = name, DisplayOrder = order };
            Category existingCategory = null;

            _categoryRepositoryMock.Setup(x => x.FindOneByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(existingCategory);

            // Act
            Category result = await _categoryService.AddAsync(category);

            // Assert
            _categoryRepositoryMock.Verify(x => x.AddAsync(category), Times.Once);
        }

        #endregion
    }
}
