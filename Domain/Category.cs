using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="The category name is required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The category display order is required")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
