using BookManagerAPI.Repository.Impl;
using BookManagerAPI.Repository.Interfaces;
using BookManagerAPI.Service.Impl;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Web.Contracts.Book;
using BookManagerAPI.Web.Contracts.User;
using BookManagerAPI.Web.MappingProfiles;
using BookManagerAPI.Web.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;
using System.Data;
using System.Data.SqlClient;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<UserRequestModel>, AddUserRequestValidator>();
builder.Services.AddScoped<IValidator<BookRequestModel>, AddBookRequestValidator>();

builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(BookMappingProfile));

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration.GetValue<string>("BlobConfiguration:StorageConnectionString"));
});

builder.Services.AddTransient<IDbConnection, SqlConnection>();
builder.Services.AddTransient<IAzureBlobRepository, AzureBlobRepository>();
builder.Services.AddTransient<ISQLDBRepository, SQLDBRepository>();
builder.Services.AddTransient<IBookService,BookService>();
builder.Services.AddTransient<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
