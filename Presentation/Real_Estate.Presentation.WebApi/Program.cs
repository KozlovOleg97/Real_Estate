using Microsoft.AspNetCore.Identity;
using Real_Estate.Core.Application;
using Real_Estate.Infrastructure.Identity.Entities;
using Real_Estate.Infrastructure.Identity.Seeds;
using Real_Estate.Infrastructure.Identity.Services;
using Real_Estate.Infrastructure.Persistence;
using Real_Estate.Infrastructure.Shared.Services;
using Real_Estate.Presentation.WebApi.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerExtension();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	try
	{
		var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
		var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

		await DefaultRoles.SeedAsync(userManager, roleManager);
		await DefaultDeveloperUser.SeedAsync(userManager, roleManager);
		await DefaultAdminUser.SeedAsync(userManager, roleManager);
	}
	catch (Exception exception)
	{

	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwaggerExtension();


app.MapControllers();

app.Run();
