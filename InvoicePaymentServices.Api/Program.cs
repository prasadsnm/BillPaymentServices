using InvoicePaymentServices.Core.Interfaces.Repositories;
using InvoicePaymentServices.Core.Interfaces.Services;
using InvoicePaymentServices.Core.Services;
using InvoicePaymentServices.Infra.DBContext;
using InvoicePaymentServices.Infra.Repositories;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// database connection
var connectionString = builder.Configuration.GetConnectionString("InvoicePayment");
builder.Services.AddDbContext<InvoicePaymentDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(opt =>
{
    // Not sure if we need to set default version here. so ignore it for now.
    // opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(2, 1);
    // opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddCors();

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
