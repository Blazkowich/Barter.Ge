using Barter.Ge.BLL.AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Barter.Ge.BLL.CustomExceptions;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Barter.Ge.Api.Bootstrapping.Middleware;
using Serilog;
using Barter.Ge.Api.Bootstrapping.Extensions;
using Barter.Ge.Api.AutoMapper;
using Barter.Ge.BLL.DependencyInjection;
using Barter.Ge.DAL.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Barter.Ge.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureLogger();
        builder.Host.UseSerilog();

        builder.Services.ConfigureExceptionHandlingMiddleware(new Dictionary<Type, HttpStatusCode>
        {
            [typeof(BadRequestException)] = HttpStatusCode.BadRequest,
            [typeof(ValidationException)] = HttpStatusCode.BadRequest,
            [typeof(NotFoundException)] = HttpStatusCode.NotFound,
            [typeof(ForbiddenException)] = HttpStatusCode.Forbidden,
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAutoMapper(typeof(AutomapperProfile));
        builder.Services.AddAutoMapper(typeof(AutomapperProfileBLL));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDALRepositories(builder.Configuration);
        builder.Services.AddBLLServices();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.AllowTrailingCommas = true;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Barter API",
                Description = "API for managing exchange or gift items.",
                Contact = new OpenApiContact
                {
                    Name = "Otar Iluridze",
                    Email = "otar_iluridze@gmail.com",
                    Url = new Uri("https://api.barter.ge"),
                },
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        var app = builder.Build();

        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "[ANALYTICS] API endpoint call info";
            options.EnrichDiagnosticContext = (IDiagnosticContext diagnosticContext, HttpContext httpContext) =>
            {
                diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                diagnosticContext.Set("ResponseStatusCode", httpContext.Response.StatusCode);
            };
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameStore API v1");
            });

            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async context =>
                {
                    var exceptionHandler = context
                        .RequestServices
                        .GetRequiredService<GlobalExceptionHandler>();

                    await exceptionHandler
                        .TryHandleAsync(
                        context,
                        context.Features.Get<IExceptionHandlerFeature>().Error,
                        CancellationToken.None);
                },
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}
