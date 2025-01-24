using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Specify URLs for both HTTP and HTTPS
builder.WebHost.UseUrls("http://localhost:5246", "https://localhost:7288");

// Enable CORS (if testing from a different origin)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Enable CORS
app.UseCors();

// Uncomment the following line if you want HTTPS redirection
 app.UseHttpsRedirection(); // Ensure this is commented out for HTTP testing

// Map routes
app.MapGet("/hello", () => {
    return Results.Content("<h1>helloworld</h1>", "text/html");
});

List<Category> categories = new List<Category>()
{
    new Category { CategoryId = Guid.NewGuid(), Name = "Category 1", Description = "This is Category 1", CreatedAt = DateTime.Now },
    new Category { CategoryId = Guid.NewGuid(), Name = "Category 2", Description = "This is Category 2", CreatedAt = DateTime.Now },
    // Add more categories as needed for your application.
};

// Read categories
app.MapGet("/api/categories", ([FromQuery] string? SearchValue) =>
{
if (!string.IsNullOrEmpty(SearchValue) && categories != null)
{
    var searchedCategories = categories
        .Where(category => category?.Name?.Contains(SearchValue, StringComparison.OrdinalIgnoreCase) == true)
        .ToList();

    return Results.Ok(searchedCategories);
}


    Console.WriteLine("Received GET request for categories");
    return Results.Ok(categories);
});


// POST route for /hello
app.MapPost("/api/categories", ([FromBody] Category categoryData) => 
{

    var newCategory=new Category{
        CategoryId = Guid.NewGuid(),
        Name =categoryData.Name,
        Description =categoryData.Description,
        CreatedAt = DateTime.Now
    };

    categories.Add(newCategory);

    
return Results.Created($"/api/categories/{newCategory.CategoryId}",newCategory);
});
app.MapDelete("/api/categories/{categoryId}", (Guid categoryId) =>
{
    // Try to find the category with the given GUID
    var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
    
    if (foundCategory == null)
    {
        // Return 404 Not Found if the category doesn't exist
        return Results.NotFound();
    }

    // Remove the category only if it exists
    categories.Remove(foundCategory);

    // Return 204 No Content upon successful deletion
    return Results.NoContent();
});
app.MapPut("/api/categories",()=>{
    var foundCategory=categories.FirstOrDefault(category=>category.CategoryId==Guid.Parse("3b102d57-02e3-406b-b76f-ab725772d5a3"));
    if(foundCategory==null)
    {
        return Results.NotFound();
}
    foundCategory.Name="Cosmetics";
return Results.NoContent();
});
app.Run();

public record Category
{
    public Guid CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    
    // Add more properties as needed for your product entity.
};
