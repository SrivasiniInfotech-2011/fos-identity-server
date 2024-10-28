using AMS.API.Authenticator.Configurations;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

//Adding the Certificate
var environment = builder.Environment.ContentRootPath ?? builder.Environment.WebRootPath;
X509Certificate2 cert = new X509Certificate2(Path.Combine(environment, "damienbodserver.pfx"), "");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddIdentityServer()
        .AddSigningCredential(cert)
        .AddInMemoryIdentityResources(Config.GetIdentityResources())
        .AddInMemoryApiResources(Config.GetApiResources())
        .AddInMemoryApiScopes(Config.GetApiScopes())
        .AddInMemoryClients(Config.GetClients())
        .AddUserStore(builder.Configuration); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
