using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BaseBusiness.Model
{
    [Table("categories")]
    public class CategoryModel
    {
        [Key]
        public long Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public long? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public CategoryModel Parent { get; set; }

        public ICollection<CategoryModel> Children { get; set; }

        public byte Status { get; set; } = 1;
    }
}
