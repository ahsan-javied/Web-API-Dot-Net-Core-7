using DAL.DBCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;
using Services.Utils;
using WebAPI.Helpers.FilterHandlers;
using WebAPI.Helpers.Middlewares;
using WebAPI.Helpers;
using Microsoft.AspNetCore.Hosting;

//Read Configuration from appSettings.json
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

//Initialize Logger
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();
try
{
    Log.Information("Application Starting.");
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(swagger =>
    {
        //This is to generate the Default UI of Swagger Documentation  
        swagger.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "MyBank v1",
            Title = "MyBank",
            Description = "MyBank"
        });
        // To Enable authorization using Swagger (JWT)  
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        });
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
    });
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddMemoryCache();
    builder.Services.AddControllers()
        .AddNewtonsoftJson(jsonOptions =>
        {
            jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
            jsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

        });

    builder.Services.AddDbContextPool<CoreDBContext>(
        options => options.UseSqlServer(
            config.GetConnectionString("DefaultConnection")
            )
        );
    builder.Services.AddMvcCore(options =>
    {
        options.Filters.Add(typeof(ValidateModelStateAttribute));
    });

    builder.Services.AddServicesDependencies();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    //Add Custom Middlewares
    app.UseMiddleware<RequestHandelerMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The Application failed to start.");
}
finally
{
    Log.CloseAndFlush();
}
