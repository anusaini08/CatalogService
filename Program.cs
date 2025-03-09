using Amazon.S3;
using Amazon.CloudWatch;
using CatalogService.DataContext;
using CatalogService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Add Database Context
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version())
    ));

// AWS Services Configuration
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAWSService<IAmazonCloudWatch>();

// Register Services

// Add S3 or Local Storage Service based on config
var useS3 = builder.Configuration.GetValue<bool>("Storage:UseS3");
if (useS3)
    builder.Services.AddScoped<IStorageService, S3StorageService>();
else
    builder.Services.AddScoped<IStorageService, LocalStorageService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IImageService, ImageService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply pending migrations automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    dbContext.Database.Migrate(); // Apply migrations
}

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
