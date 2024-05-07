using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(20, ErrorMessage = "{0} should be less than 20 characters long")]
        [Remote(action: "IsCategoryExist", controller: "Category", areaName:"Admin", 
            AdditionalFields = "Id")]
        public string Name { get; set; }

        
        [DisplayName("Display Order")]
        [Required(ErrorMessage = "{0} is required")]    
        [Range(1, 100, ErrorMessage = "{0} should be between {1} and {2}")]
        public int? DisplayOrder { get; set; }
    }
}
