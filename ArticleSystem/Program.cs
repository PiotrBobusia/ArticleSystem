using ArticleSystem.CQRS.Commands.AddNewUser;
using ArticleSystem.Database;
using ArticleSystem.Entity;
using ArticleSystem.MapperProfile;
using ArticleSystem.Middleware;
using ArticleSystem.Models;
using ArticleSystem.Repository;
using ArticleSystem.Requirements;
using ArticleSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ArticleDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbLegion"));
});

builder.Services.AddMediatR(typeof(AddNewUserCommand));
builder.Services.AddAutoMapper(typeof(MappingProfile));

JwtConfiguration jwtConfiguration = new JwtConfiguration();
builder.Configuration.GetSection("JwtBearer").Bind(jwtConfiguration);
builder.Services.AddSingleton(jwtConfiguration);

builder.Services.AddAuthentication( opt =>
{
    opt.DefaultAuthenticateScheme = "Bearer";
    opt.DefaultScheme = "Bearer";
    opt.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtConfiguration.JwtIssuer,
        ValidAudience = jwtConfiguration.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.JwtKey))
    };
});

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();

//Authorization Handlers
builder.Services.AddScoped<IAuthorizationHandler, GetArticleUserRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AddNewArticleUserRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ModifyArticleUserRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, DeleteArticleUserRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AddTagUserRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, DeleteTagUserRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, DeleteAllTagUserRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, DeleteCommentUserRequirementHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();