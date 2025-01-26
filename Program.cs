using asp_net_ecommerce_web_api;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

// This sets up a new web application using the minimal API style in ASP.NET Core.
var builder = WebApplication.CreateBuilder(args);

// =====================================================================
// **Specify URLs for both HTTP and HTTPS**
// ---------------------------------------------------------------------
// - The application will listen for requests on the specified URLs.
// - HTTP: http://localhost:5246 (unencrypted communication)
// - HTTPS: https://localhost:7288 (encrypted communication with SSL/TLS)
// - HTTPS ensures data sent between the client and server is secure.
// =====================================================================
builder.WebHost.UseUrls("http://localhost:5246", "https://localhost:7288");

// =====================================================================
// **Enable Cross-Origin Resource Sharing (CORS)**
// ---------------------------------------------------------------------
// - CORS allows a server to handle requests from a different origin.
// - Example: A frontend running on "http://localhost:3000" may need to
//   call APIs on "http://localhost:5246".
// - Without CORS enabled, browsers block such cross-origin requests
//   for security reasons.
// - Here, we allow requests from any origin, method, and header.
// - This is useful for development but should be restricted in production
//   for security.
// =====================================================================
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin() // Allow requests from any domain.
               .AllowAnyMethod() // Allow any HTTP method (GET, POST, etc.).
               .AllowAnyHeader(); // Allow any headers in the request.
    });
});

// =====================================================================
// **Add Controllers to the Service Container**
// ---------------------------------------------------------------------
// - Controllers handle HTTP requests (like GET, POST, PUT, DELETE).
// - This line tells the application to look for controller classes
//   (e.g., `CategoryController`) and register them so they can process
//   incoming API requests.
// - Without this, the app won’t recognize controller-based routes.
// =====================================================================
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================================================================
// **Build the Web Application**
// ---------------------------------------------------------------------
// - This compiles the configuration and prepares the application to run.
// =====================================================================
var app = builder.Build();

// =====================================================================
// **Enable CORS Middleware**
// ---------------------------------------------------------------------
// - The middleware ensures the CORS policy is applied to every HTTP request.
// - Without this, cross-origin requests may still be blocked even if CORS
//   is configured above.
// =====================================================================
app.UseCors();

// =====================================================================
// **Redirect HTTP to HTTPS (Optional)**
// ---------------------------------------------------------------------
// - Redirects all HTTP requests to the secure HTTPS endpoint.
// - This ensures encrypted communication with clients.
// - Uncomment this line during production to enforce HTTPS usage.
// - For development, you can leave this commented out to simplify testing.
// =====================================================================
app.UseHttpsRedirection();

// =====================================================================
// **Map Controllers**
// ---------------------------------------------------------------------
// - Maps all controller routes to their respective endpoints.
// - For example:
//   - If a `CategoryController` has an endpoint for `/api/categories`,
//     this line ensures it is accessible when the application runs.
// - Without this line, your API endpoints won't be reachable.
// =====================================================================
app.MapControllers();

// =====================================================================
// **Run the Application**
// ---------------------------------------------------------------------
// - This starts the web server and begins listening for incoming HTTP
//   requests on the specified URLs.
// - The app will continue running until manually stopped.
// =====================================================================
app.Run();
