using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilter;
using Repositories.Context;
using WebApiProject.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;//Ýçerik pazarlýðýna açýk olduðumuzu belirttik.
    config.ReturnHttpNotAcceptable = true;//Yapýlan istek formatýnýn desteklenmediðini istemciye gönderdik.406 kodu ile
})
.AddXmlDataContractSerializerFormatters()    
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.AddScoped<ValidationFilterAttribute>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureManagerRepository();
builder.Services.ConfigureServiceManager();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerProvider>();
app.ConfigureExceptionHandler(logger);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
