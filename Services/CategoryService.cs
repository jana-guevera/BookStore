using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.RepositoryContracts;
using Contracts.ServicesContracts;
using Domain.Entities;
using Domain.ErrorHandling;
using Services.Helpers;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> AddAsync(Category category)
        {
            // Check if category is null and throw error
            if (category == null) throw new NullArgumentException("Empty Category");

            // Validate the category data and throw error 
            ValidationHelper.ModelValidation(category);

            // Check if there is an existing category with the same name and throw error
            Category existingCategory = await _categoryRepository.FindOneAsync(x => x.Name == category.Name);
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
            if (category == null) throw new NullArgumentException("Category is null");

            Category existingCategory = await _categoryRepository.FindOneAsync(x => x.Id == category.Id);

            if (existingCategory == null) return null;

            existingCategory.Name = category.Name;
            existingCategory.DisplayOrder = category.DisplayOrder;

            return await _categoryRepository.UpdateAsync(existingCategory);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _categoryRepository.FindOneAsync(x => x.Id == id);
        }   

        public async Task<Category> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NullArgumentException("Empty category name");
            }

            return await _categoryRepository.FindOneAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
