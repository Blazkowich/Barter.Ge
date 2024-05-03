using Barter.Application;
using Barter.Application.CustomExceptions;
using Barter.Application.MappingProfiles;
using Barter.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using Barter.Bootstrapping.Extensions;
using Barter.Bootstrapping.Middleware;

namespace Barter.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.ConfigureLogger();
            builder.Host.UseSerilog();

            builder.Services.ConfigureExceptionHandlingMiddleware(new Dictionary<Type, HttpStatusCode>
            {
                [typeof(BadRequestException)] = HttpStatusCode.BadRequest,
                [typeof(ValidationException)] = HttpStatusCode.BadRequest,
                [typeof(NotFoundException)] = HttpStatusCode.NotFound,
                [typeof(ForbiddenException)] = HttpStatusCode.Forbidden,
            });

            builder.Services.AddAutoMapper(typeof(AutomapperProfile));

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddDALRepositories(builder.Configuration);
            builder.Services.AddBLLServices();

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
}
