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
            if (category == null) throw new NoArgumentError("Empty Category");

            // Validate the category data and throw error 
            ValidationHelper.ModelValidation(category);

            // Check if there is an existing category with the same name and throw error
            Category existingCategory = await _categoryRepository.FindByName(category.Name);
            if (existingCategory != null)
            {
                throw new UniqueValidationError("Category Already Exists");
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
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _categoryRepository.FindOneAsync(x => x.Id == id);
        }   

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
