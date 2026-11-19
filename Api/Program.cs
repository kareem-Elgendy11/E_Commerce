using Application.Common;
using Application.Dependency;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using System.Security.Cryptography.Xml;

//using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//add sevice of swagger and add some documentation
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "E_Commerce API",
        Description = "My First ASP.NET Core Web API",
        //TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "Example Contact",
            Email = "kareemelgendy50@gmail.com"
        },
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token in Here"
    });
    option.AddSecurityRequirement(document =>
    {
        var x = new OpenApiSecuritySchemeReference("Bearer",document);
        return new OpenApiSecurityRequirement
        {
            {x,new List<string>() }
        };
    });
});
//توحيد الناتج
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var hasJsonError = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Any(e => e.Exception is System.Text.Json.JsonException);

        if (hasJsonError)
        {
            var response = ApiResponse<string>.Faild("Invalid request data, please check and try again.");
            return new BadRequestObjectResult(response);
        }

        var errors = context.ModelState
            .Where(e => e.Value!.Errors.Count > 0)
            .SelectMany(e => e.Value!.Errors.Select(x => x.ErrorMessage))
            .ToList();

        var normalResponse = ApiResponse<string>.Faild(string.Join(" | ", errors));
        return new BadRequestObjectResult(normalResponse);
    };
});
/////////////////
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.User.RequireUniqueEmail = true;
    //option.ac
    option.SignIn.RequireConfirmedEmail = true;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    option.Lockout.MaxFailedAccessAttempts = 3;
    option.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<E_COmmerceDbContext>()
    .AddDefaultTokenProviders();
//addRateLimit
builder.Services.AddRateLimiter(option =>
{
    option.AddTokenBucketLimiter("retelimit", option =>
    {
        option.QueueLimit = 0;
        option.TokensPerPeriod = 2;
        option.TokenLimit = 1;
        option.AutoReplenishment = true;
        option.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
    });
});
//add Time To Token 
builder.Services.Configure<DataProtectionTokenProviderOptions>(option =>
{
    option.TokenLifespan = TimeSpan.FromMinutes(15);
});
builder.Services.Configure<SecurityStampValidatorOptions>(option =>
{
    option.ValidationInterval = TimeSpan.Zero;
});

//add jwtidentity Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = true;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidAudience = builder.Configuration["jwt:Audience"],
        ValidateAudience = true,
        ValidateIssuer=true,
        ValidIssuer= builder.Configuration["jwt:issuer"],
        IssuerSigningKey=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]!)),
        ClockSkew=TimeSpan.Zero
    };
});

//add service for Dbcontext
builder.Services.AddDbContext<E_COmmerceDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
//addd ependency Injection
builder.Services.AddDependencyServiceinfra();
builder.Services.AddDependencyService();
//builder.Services.AddScoped<IRefreshTokenRepo, RefreshRepo>();

//builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request ppeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope=app.Services.CreateScope();
var rolemanager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
foreach (var role in new[] { "user", "admin" })
{
    var roleexist = await rolemanager.RoleExistsAsync(role);
    if (!roleexist)
    {
        await rolemanager.CreateAsync(new IdentityRole(role));
    }
}



app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
