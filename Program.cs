var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Configure CORS to allow calls from local React/Vite dev servers
var reactCorsPolicy = "_reactDevCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: reactCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Enable CORS for browser-based frontends (React/Vite)
app.UseCors(reactCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();