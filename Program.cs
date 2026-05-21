using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TrackExences.Data;
using TrackExences.Dtos.Expenses;
using TrackExences.Dtos.User;
using TrackExences.Middlewares;
using TrackExences.Repositories.ExpenseRepo;
using TrackExences.Repositories.UserRepo;
using TrackExences.Services;
using TrackExences.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();



var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = jwtSettings["Issuer"],
            ValidAudience            = jwtSettings["Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(secretKey),
            ClockSkew                = TimeSpan.Zero // Remove default 5-min tolerance
        };
    });
builder.Services.AddAuthorization();




builder.Services.AddControllers();

string stringConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(stringConnection));

builder.Services.AddScoped<IValidator<CreateUser>, UserValidator>();

builder.Services.AddScoped<IValidator<CreateExpense>, ExpenseValidator>();

builder.Services.AddScoped<IValidator<UpdateExpense>, UpdateExpenseValidator>();

builder.Services.AddScoped<HasherService>();

builder.Services.AddScoped<UserMapperService>();

builder.Services.AddScoped<ExpenseMapperService>();

builder.Services.AddScoped<IExpenseRepo, ExpensesRepository>();

builder.Services.AddScoped<IUserRepo, UserRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

// Use CORS — must be before UseAuthorization
app.UseCors("AllowAngular");

app.UseMiddleware<ExceptionsMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.MapFallbackToController("Handle", "Fallback");

app.UseHttpsRedirection();

app.Run();
