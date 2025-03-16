using DEMO_Product.Infrastructure;
using DEMO_Product.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddDependencyInjection();

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

using (var scope = app.Services.CreateScope())
{
    var productContextSeed = scope.ServiceProvider.GetRequiredService<ProductContextSeed>();
    await productContextSeed.InitializeAsync();
    await productContextSeed.TrySeedAsync();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
