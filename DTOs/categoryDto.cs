using System.ComponentModel.DataAnnotations;

namespace asp_net_ecommerce_web_api
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;
    }

    public class CategoryUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}


//DTOs are used for data transfer to add a security layer
