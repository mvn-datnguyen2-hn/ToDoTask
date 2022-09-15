using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDo.DTOs;
using ToDo.Models;
using ToDo.Security;
using ToDo.Services.PasswordHash;
using ToDo.Services.ToDo;
using ToDo.Services.TokenGenerator;
using ToDo.Services.Users;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoDbContext>(v =>
    v.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddSingleton<BcryptPasswordHasher>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddSingleton<AccessTokenGenerator>();
builder.Services.Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication"));
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(c =>
{
    //AuthenticationConfiguration authenticationConfiguration;
    var authenticationConfiguration = builder.Configuration.GetSection("Authentication").Get<AuthenticationConfiguration>();
    c.TokenValidationParameters = new TokenValidationParameters()
    {
        NameClaimType = ClaimTypes.NameIdentifier,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
        ValidIssuer = authenticationConfiguration.Issuer,
        ValidAudience = authenticationConfiguration.Audience,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
