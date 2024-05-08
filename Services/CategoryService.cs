using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.RepositoryContracts;
using Contracts.ServicesContracts;
using Domain.Entities;
using Domain.Validators;
using Services.Exceptions;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryValidator _categoryValidator;

        public CategoryService(ICategoryRepository categoryRepository, CategoryValidator categoryValidator)
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
        }

        public async Task<Category> AddAsync(Category category)
        {
            // Check if category is null and throw error
            if (category == null) throw new NullArgumentException("Empty Category");

            // Validate the category data and throw error 
            IDictionary<string, string[]> errors = _categoryValidator.ValidateModel(category);
            if (errors != null) throw new EntityValidationException("Category entity validation failed", errors);

            // Check if there is an existing category with the same name and throw error
            Category existingCategory = await _categoryRepository.FindOneByNameAsync(category.Name);
            if (existingCategory != null)
            {
                throw new UniqueValidationException("Category Already Exists");
            }
            
            // create category and return the category
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> RemoveAsync(int id)
        {
            Category category = new Category() { Id = id };
            return await _categoryRepository.RemoveAsync(category);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            // Check if category is null and throw error
            if (category == null) throw new NullArgumentException("Category is null");

            // Return null if category doesnt exist 
            Category existingCategory = await _categoryRepository.FindOneByIdAsync(category.Id);
            if (existingCategory == null) return null;

            existingCategory.Name = category.Name;
            existingCategory.DisplayOrder = category.DisplayOrder;

            // If cateory name has not been changed
            if (string.Equals(category.Name, existingCategory.Name))
            {
                return await _categoryRepository.UpdateAsync(existingCategory);
            }

            // If category with same name exist throw uniqu validation exception
            Category categoryWithSameName = await _categoryRepository.FindOneByNameAsync(category.Name);
            if (categoryWithSameName != null) throw new UniqueValidationException("Category already exist");
            
            // Save the updated category to db
            return await _categoryRepository.UpdateAsync(existingCategory);
        }

        public async Task<Category> FindOneByIdAsync(int id)
        {
            return await _categoryRepository.FindOneByIdAsync(id);
        }   

        public async Task<Category> FindOneByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NullArgumentException("Empty category name");
            }

            return await _categoryRepository.FindOneByNameAsync(name);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
