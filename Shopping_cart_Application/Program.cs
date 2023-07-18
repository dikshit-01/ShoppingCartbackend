using Buisness_Logic_Layer.Services;
using Data_Access_Layer.DataContext;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<ContextClass>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ContextClass>().AddDefaultTokenProviders();
//builder.Services.AddScoped<IGenericRepository<User>,GenericRepository<User>>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<UserServices, UserServices>();
builder.Services.AddScoped<ProductService, ProductService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Wei_API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new
                    SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                    (builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "YourSessionCookieName"; // Provide a unique name for your session cookie
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout duration
    options.Cookie.HttpOnly = true; // Ensure that the session cookie is only accessible through HTTP
                                    // Add any additional session options as needed
});
// Import the following namespace if needed:
// using System.Web.Http.Cors;




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}
app.UseCors();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();
app.UseHttpsRedirection();

app.MapControllers();


app.Run();
