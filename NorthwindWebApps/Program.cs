using Northwind.Services;
using Microsoft.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore;
using Northwind.Services.EntityFrameworkCore.Context;
using Northwind.Services.BloggingModels;
using Northwind.Services.EntityFrameworkCore.Blogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<NorthwindContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<string[]>();

builder.Services.AddTransient<IProductManagementService, ProductManagementService>();
builder.Services.AddTransient<IProductCategoryManagementService, ProductCategoryManagementService>();
builder.Services.AddTransient<IProductCategoryPicturesManagementService, ProductCategoryPicturesManagementService>();
builder.Services.AddTransient<IEmployeeManagementService, EmployeeManagementService>();
builder.Services.AddTransient<ISupplierManagementService, SupplierManagementService>();
builder.Services.AddTransient<IBloggingService, BloggingService>();
builder.Services.AddTransient<ICommentingService, CommentingService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.Run();
