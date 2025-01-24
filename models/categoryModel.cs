namespace asp_net_ecommerce_web_api.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } = string.Empty; // Default value
        public DateTime CreatedAt { get; set; }
    }
}
