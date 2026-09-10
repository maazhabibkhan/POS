using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SmallPOS.API.Data;
using SmallPOS.API.Repositories.Authentication;
using SmallPOS.API.Repositories.Products;
using SmallPOS.API.Services.Authentication;
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
// AUTHENTICATION
// =========================

var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("JWT key is missing.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();


// =========================
// REPOSITORIES
// =========================

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


// =========================
// SERVICES
// =========================

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();


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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
