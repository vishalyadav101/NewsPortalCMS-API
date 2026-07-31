// ============================================================
// USING STATEMENTS
// ============================================================

// JWT Authentication ke liye
// JWT secret key ko bytes me convert karne ke liye
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
// ASP.NET Core Identity ke liye
using Microsoft.AspNetCore.Identity;
// Entity Framework Core ke liye
using Microsoft.EntityFrameworkCore;
// JWT token validation aur security key ke liye
using Microsoft.IdentityModel.Tokens;
// Swagger me JWT Authorize button configure karne ke liye
using Microsoft.OpenApi.Models;
// Application layer
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Application.Mappings;
using NewsPortalCMS.Application.Services;
using NewsPortalCMS.Application.Validators;
// Domain layer
using NewsPortalCMS.Domain.Entities;
// Infrastructure layer
using NewsPortalCMS.Infrastructure.Data;
using NewsPortalCMS.Infrastructure.Repositories;
using NewsPortalCMS.Infrastructure.Seed;
using NewsPortalCMS.Infrastructure.Services;
using NewsPortalCMS.Interfaces;
using NewsPortalCMS.Repositories;
using NewsPortalCMS.Services;
using NewsPortalCMS.Services.Interfaces;



// ============================================================
// 1. CREATE WEB APPLICATION BUILDER
// ============================================================

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MenuProfile).Assembly);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();



// ============================================================
// 2. DATABASE CONFIGURATION
// ============================================================

// appsettings.json se DefaultConnection read karke
// SQL Server ke saath ApplicationDbContext register karta hai.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


// ============================================================
// 3. ASP.NET CORE IDENTITY CONFIGURATION
// ============================================================

// ApplicationUser hamara custom user hai.
// IdentityRole<int> roles ko integer Id ke saath manage karega.
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
{
    // Password me kam se kam ek number required hai.
    options.Password.RequireDigit = true;

    // Password me kam se kam ek uppercase letter required hai.
    options.Password.RequireUppercase = true;

    // Password me kam se kam ek lowercase letter required hai.
    options.Password.RequireLowercase = true;

    // Special character ko abhi mandatory nahi rakha hai.
    options.Password.RequireNonAlphanumeric = false;

    // Minimum password length.
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


// ============================================================
// 4. JWT AUTHENTICATION CONFIGURATION
// ============================================================

builder.Services.AddAuthentication(options =>
{
    // API authentication ke liye JWT Bearer scheme use hogi.
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Valid token ko authentication properties me save karta hai.
    options.SaveToken = true;

    // Local development ke liye false rakha hai.
    // Production me HTTPS use karna mandatory rahega.
    options.RequireHttpsMetadata = false;

    // Incoming JWT token validate karne ke rules.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Check karega token expected issuer ne generate kiya hai.
        ValidateIssuer = true,

        // Check karega token expected audience ke liye hai.
        ValidateAudience = true,

        // Expired token reject karega.
        ValidateLifetime = true,

        // Token ki signature validate karega.
        ValidateIssuerSigningKey = true,

        // appsettings.json se issuer.
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        // appsettings.json se audience.
        ValidAudience = builder.Configuration["Jwt:Audience"],

        // JWT signature verify karne ke liye secret key.
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!
            )
        ),

        // Token expire hote hi invalid ho jayega.
        // Default extra grace period nahi milega.
        ClockSkew = TimeSpan.Zero
    };
});



// ============================================================
// 5. APPLICATION SERVICES / DEPENDENCY INJECTION
// ============================================================

// Authentication Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtTokenService>();

// Category Repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Category Service
builder.Services.AddScoped<ICategoryService, CategoryService>();

// News Repository
builder.Services.AddScoped<INewsRepository, NewsRepository>();
// SubCategory Repository
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

// SubCategory Service
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();


// Tag
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddScoped<INewsTagRepository, NewsTagRepository>();
builder.Services.AddScoped<INewsTagService, NewsTagService>();

//Menu
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService,MenuService>();

//MenuItem
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();


// News Services
builder.Services.AddScoped<INewsService, NewsService>();


//Media
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<IMediaService, MediaService>();

//Static Page
builder.Services.AddScoped<IStaticPageService, StaticPageService>();
builder.Services.AddScoped<IStaticPageRepository, StaticPageRepository>();

//Comment
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

//Advertisement
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<IAdvertisementRepository,AdvertisementRepository>();

//User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//Permission
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();

builder.Services.AddScoped<IPermissionService, PermissionService>();

//RolePermission
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
// ============================================================
// 6. CONTROLLERS
// ============================================================

// API Controllers ko register karta hai.
builder.Services.AddControllers();


// ============================================================
// 7. SWAGGER CONFIGURATION
// ============================================================

// Swagger/OpenAPI endpoint discovery ke liye.
builder.Services.AddEndpointsApiExplorer();

// Swagger configure kar rahe hain.
// Isme JWT Bearer authentication support bhi add hai.
builder.Services.AddSwaggerGen(options =>
{
    // Swagger ko batata hai ki API Bearer JWT use karti hai.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        // HTTP Bearer Authentication.
        Type = SecuritySchemeType.Http,

        Scheme = "bearer",

        BearerFormat = "JWT",

        In = ParameterLocation.Header,

        Description = "Enter your JWT token"
    });

    // Swagger requests me Bearer authentication use karne ki
    // security requirement define karta hai.
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            Array.Empty<string>()
        }
    });
});


// ============================================================
// 8. BUILD APPLICATION
// ============================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// YAHI LINE MISSING THI.
// Is line ke baad hi 'app' object available hota hai.
var app = builder.Build();


// ============================================================
// 9. IDENTITY DATA SEEDING
// ============================================================

// Application start hote waqt ek temporary DI scope banate hain.
using (var scope = app.Services.CreateScope())
{
    // Identity roles manage karne ke liye RoleManager.
    var roleManager =
        scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole<int>>>();

    // Application users manage karne ke liye UserManager.
    var userManager =
        scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

    // Initial roles create karega:
    // SuperAdmin, Admin, Editor, Reporter
    await IdentitySeeder.SeedRolesAsync(roleManager);

    // Existing "vivek" user ko SuperAdmin role assign karega.
    await IdentitySeeder.AssignSuperAdminAsync(userManager);
}


// ============================================================
// 10. HTTP REQUEST PIPELINE
// ============================================================

// Swagger sirf Development environment me enable hai.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// HTTP request ko HTTPS par redirect karta hai.
app.UseHttpsRedirection();


// ============================================================
// 11. AUTHENTICATION
// ============================================================
app.UseCors("AllowAngularApp");
// Request me JWT token hai to user ko authenticate karega.
//
// IMPORTANT:
// Authentication hamesha Authorization se pehle aayega.
app.UseAuthentication();


// ============================================================
// 12. AUTHORIZATION
// ============================================================

// [Authorize] aur role-based permissions check karega.
app.UseAuthorization();


// ============================================================
// 13. MAP CONTROLLERS
// ============================================================

// Controller routes jaise:
// /api/Auth/login
// /api/Auth/register
// ko application se map karta hai.

app.UseStaticFiles();
app.MapControllers();


// ============================================================
// 14. RUN APPLICATION
// ============================================================

app.Run();