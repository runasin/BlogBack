using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using BlogFull.Entity.Context.ApplicationDbContext;
using BlogFull.Repository.UsersRepository;
using BlogFull.Repository.BlogsRepository;
using BlogFull.Repository.BlogsCommentRepository;
using BlogFull.Service.Token;
using BlogFull.Service.Photo;
using BlogFull.Entity.Models.Settings;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

#region Users
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Blogs
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
#endregion Blogs

#region BlogComments
builder.Services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
#endregion BlogComments

#region Token
builder.Services.AddScoped<ITokenService, TokenService>();
#endregion

#region Photo
builder.Services.AddScoped<IPhotoService, PhotoService>();
#endregion Photo

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.Configure<CloudinaryOptions>(configuration.GetSection("CloudinaryOptions"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog API", Version = "v1" });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
