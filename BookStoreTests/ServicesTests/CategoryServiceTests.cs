using Contracts.RepositoryContracts;
using Contracts.ServicesContracts;
using Domain.Entities;
using Domain.Validators;
using Moq;
using Services;
using Services.Exceptions;
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
            CategoryValidator validator = new CategoryValidator();

            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, validator);
        }

        #region Tests for adding category

        [Fact]
        public async void AddAsync_NullCategoryObject_NoArgumentError()
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
        public async void AddAsync_InvalidCategoryObject_ValidationError(string name, int order)
        {
            //Arrange
            Category category = new Category() { Name = name, DisplayOrder = order };

            //Act and Assert
            await Assert.ThrowsAsync<EntityValidationException>(async () => 
            { 
                await _categoryService.AddAsync(category); 
            });
        }

        [Fact]
        public async void AddAsync_ExistingCategory_UniqueValidationError()
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
        public async void AddAsync_ValidCategoryObject_CategoryObject(string name, int order)
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

        #region Tests for updating category

        [Fact]
        public async void UpdateAsync_NullCategory_NullArgumentException()
        {
            //Arrange
            Category category = null;

            //Act & Assert
            await Assert.ThrowsAsync<NullArgumentException>(async () =>
            {
                await _categoryService.UpdateAsync(category);
            });
        }

        [Theory]
        [InlineData("", 1000000)]
        [InlineData(null, 0)]
        [InlineData("sdfsdfasdasdadasdadadadad", 5)]
        [InlineData("Horror", 0)]
        public async void UpdateAsync_InvalidCategoryData_EntityValidationException(string name, int displayOrder)
        {
            //Arrange
            Category category = new Category() { Name = name, DisplayOrder = displayOrder };

            //Act & Assert
            await Assert.ThrowsAsync<EntityValidationException>(async () =>
            {
                await _categoryService.UpdateAsync(category);
            });

        }

        [Fact]
        public async void UpdateAsync_NonExistingCategory_ResourceNotFoundException()
        {
            // Arrange
            Category updatingCategory = new Category() { Id = 1, Name = "Scifi", DisplayOrder = 10};
            Category existingCategory = null;

            _categoryRepositoryMock.Setup(x => x.FindOneByIdAsync(It.IsAny<int>())).ReturnsAsync(existingCategory);

            //Act & Assert
            await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
            {
                await _categoryService.UpdateAsync(updatingCategory);
            });
        }

        [Fact]
        public async void UpdateAsync_CategoryWithSameName_UniqueValidationException()
        {
            // Arrange
            Category updatingCategory = new Category() { Id = 1, Name = "Scifi", DisplayOrder = 10 };
            Category existingCategory = new Category() { Id = 1, Name = "Horror", DisplayOrder = 10 };
            Category diffCategory = new Category() { Id = 2, Name = "Scifi", DisplayOrder = 4 };

            _categoryRepositoryMock.Setup(x => x.FindOneByIdAsync(updatingCategory.Id))
                .ReturnsAsync(existingCategory);

            _categoryRepositoryMock.Setup(x => x.FindOneByNameAsync(updatingCategory.Name))
                .ReturnsAsync(diffCategory);

            // Act and Assert
            await Assert.ThrowsAsync<UniqueValidationException>(async () =>
            {
                await _categoryService.UpdateAsync(updatingCategory);
            });
        }

        [Fact]
        public async void UpdateAsync_ValidCategory_CategoryObject()
        {
            // Arrange
            Category updatingCategory = new Category() { Id = 1, Name = "Scifi", DisplayOrder = 10 };
            Category existingCategory = new Category() { Id = 1, Name = "Scifi", DisplayOrder = 5 };
            Category updatedCategory = new Category() { Id = 1, Name = "Scifi", DisplayOrder = 10 };
            Category diffCategory = null;

            _categoryRepositoryMock.Setup(x => x.FindOneByIdAsync(updatingCategory.Id))
                .ReturnsAsync(existingCategory);

            _categoryRepositoryMock.Setup(x => x.FindOneByNameAsync(updatingCategory.Name))
                .ReturnsAsync(diffCategory);

            _categoryRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Category>())).ReturnsAsync(updatedCategory);

            // Act
            Category result = await _categoryService.UpdateAsync(updatingCategory);

            // Assert
            _categoryRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Category>()), Times.Once);
        }
        #endregion
    }
}
