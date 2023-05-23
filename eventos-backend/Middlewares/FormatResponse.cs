using eventos_backend.DTOs.Responses;
using eventos_backend.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace eventos_backend.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class FormatResponse
    {
        private readonly RequestDelegate _next;

        public FormatResponse(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            ResponseDTO responseDTO = new ResponseDTO();

            using (var responseBody = new MemoryStream())
            {
                var body = httpContext.Response.Body;
                using (var updatedBody = new MemoryStream())
                {
                    httpContext.Response.Body = updatedBody;
                    try
                    {
                        await _next(httpContext);
                        httpContext.Response.Body = body;
                        updatedBody.Seek(0, SeekOrigin.Begin);
                        var newContent = new StreamReader(updatedBody).ReadToEnd();
                        responseDTO.Result = true;
                        if (httpContext.Response.ContentType!= null && httpContext.Response.ContentType.Contains("application/json"))
                        {
                            responseDTO.Data = JsonConvert.DeserializeObject(newContent);
                        }
                        else
                        {
                            responseDTO.Data = newContent;
                            httpContext.Response.ContentType = "application/json";
                        }
                        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(responseDTO));

                    }
                    catch (Exception ex)
                    {
                        httpContext.Response.Body = body;
                        var response = httpContext.Response;
                        response.ContentType = "application/json";

                        responseDTO.Result = false;
                        switch (ex)
                        {
                            case AppException e:
                                responseDTO.Errors = e.Errors;
                                response.StatusCode = e.StatusCode;
                                break;
                            default:
                                responseDTO.Errors.Add("Error General");
                                response.StatusCode = 500;
                                break;
                        }

                        var result = JsonConvert.SerializeObject(responseDTO);
                        await response.WriteAsync(result);
                    }
                }
            }





        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class FormatResponseExtensions
    {
        public static IApplicationBuilder UseFormatResponse(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FormatResponse>();
        }
    }
}
