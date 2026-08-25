using SmallPOS.API.Data;
using SmallPOS.API.Repositories.Products;
using SmallPOS.API.Services.Products;

var builder = WebApplication.CreateBuilder(args);


// =========================
// SERVICES
// =========================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


// =========================
// CORS
// =========================

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// =========================
// DATABASE
// =========================

builder.Services.AddSingleton<SqlConnectionFactory>();


// =========================
// REPOSITORIES
// =========================

builder.Services.AddScoped<IProductRepository, ProductRepository>();


// =========================
// SERVICES
// =========================

builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();


// =========================
// HTTP PIPELINE
// =========================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// =========================
// CORS
// =========================

app.UseCors("ReactPolicy");


app.UseAuthorization();

app.MapControllers();

app.Run();