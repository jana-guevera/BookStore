using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Display Order")]
        [Range(0, int.MaxValue, ErrorMessage = "{0} should be between {1} and {2}")]
        public int DisplayOrder { get; set; }
    }
}
