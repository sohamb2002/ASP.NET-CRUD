using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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

// Add controllers to the service container
builder.Services.AddControllers();

var app = builder.Build();

// Enable CORS
app.UseCors();

// Uncomment the following line if you want HTTPS redirection
app.UseHttpsRedirection(); // Ensure this is commented out for HTTP testing

// Map controllers
app.MapControllers();

app.Run();
