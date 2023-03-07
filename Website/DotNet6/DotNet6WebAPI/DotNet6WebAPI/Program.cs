using DotNet6WebAPI.Common;
using DotNet6WebAPI.Dapper;
using DotNet6WebAPI.Dapper.Mapping;
using DotNet6WebAPI.Dapper.Repositories;
using DotNet6WebAPI.Domain.Entities;
using Hellang.Middleware.ProblemDetails;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<Student>();

//dapper mapping
SetDapperMapper.Set();

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Dotnet 6 demo Api",
        Version = "v1",
        Description = "description."
    });

    // Set the comments path for the Swagger JSON and UI.
    // 设置注释
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
//add ProblemDetails
//为异常消息统一规范格式
//统一处理异常，为不同的异常设置不同的信息
builder.Services.AddProblemDetails(opts =>
{
    opts.IncludeExceptionDetails = (ctx, ex) =>
    {
        return false;
    };
    opts.Map<CustomException>((ex) =>
    {
        var pd = StatusCodeProblemDetails.Create(StatusCodes.Status403Forbidden);
        pd.Detail = ex.Message;
        return pd;
    });
    opts.Map<Exception>((ex) =>
    {
        var pd = StatusCodeProblemDetails.Create(StatusCodes.Status500InternalServerError);
        pd.Detail = ex.Message;
        pd.Extensions.Add("key1", "value1");
        pd.Extensions.Add("key2", 2);
        return pd;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
