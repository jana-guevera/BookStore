using BookStore.Dtos.CategoryDtos;
using Contracts.ServicesContracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService; 
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (Category category in categories)
            {
                categoryDtos.Add(new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    DisplayOrder = category.DisplayOrder,
                });
            }

            return View(categoryDtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto) {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            Category category = new Category() { 
                Name = categoryDto.Name, 
                DisplayOrder = (int) categoryDto.DisplayOrder 
            };

            await _categoryService.AddAsync(category);
            TempData["success"] = "Category added successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _categoryService.FindOneByIdAsync(id);

            if (category == null)
            {
                throw new ResourceNotFoundException("Category not found");
            }

            CategoryDto categoryDto = new CategoryDto() {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
            };

            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            Category category = new Category()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                DisplayOrder = (int) categoryDto.DisplayOrder
            };

            Category updatedCategory = await _categoryService.UpdateAsync(category);
			TempData["success"] = "Category updated successfully";
			return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int Id)
        {
            await _categoryService.RemoveAsync(Id);
			TempData["success"] = "Category removed successfully";
			return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles remote validation for category name field
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsCategoryExist(string name, int? Id)
        {
            Category category = await _categoryService.FindOneByNameAsync(name);

            if (category == null) return Json(true);

            if (Id == null)
            {
                return Json($"Category name is already in use");
            }else if ((int)Id == category.Id)
            {
                return Json(true);
            }


            return Json($"Category name is already in use");
        }
    }
}
