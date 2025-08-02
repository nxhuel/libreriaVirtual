using libreriaVirtual.Configurations;
using libreriaVirtual.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection DataBase

// var connectionString = builder.Configuration.GetConnectionString("Connection");
// builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add integration folder Configuration
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddIdentityConfiguration();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        policy => policy.AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader());
});

// Integration services
// builder.Services.AddScoped<IRoleService, RoleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
