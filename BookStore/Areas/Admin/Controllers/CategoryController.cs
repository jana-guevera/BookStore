using Contracts.ServicesContracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService; 

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category) {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await _categoryService.AddAsync(category);
            TempData["success"] = "Category added successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _categoryService.FindOneByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            Category updatedCategory = await _categoryService.UpdateAsync(category);

            if(updatedCategory == null) { return NotFound(); }
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
