using ApiOAuthPractica.Data;
using ApiOAuthPractica.Repositories;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient(builder.Configuration.GetSection("KeyVault"));
});

SecretClient secretClient = 
    builder.Services.BuildServiceProvider().GetService<SecretClient>();
KeyVaultSecret secret = await secretClient.GetSecretAsync("SqlAzure");

//string connectionString = secret.Value;
string connectionString = builder.Configuration.GetConnectionString("SqlAzure");

builder.Services.AddTransient<RepositoryDoctores>();
builder.Services.AddDbContext<DoctoresContext>
    (options => options.UseSqlServer(connectionString));




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
