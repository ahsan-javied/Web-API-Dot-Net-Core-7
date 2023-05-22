using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.Common;
using Models.DTO.User;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Helpers.Middlewares
{
    public class RequestHandelerMiddleware
    {
        private readonly IConfiguration config;
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestHandelerMiddleware> _logger;


        public RequestHandelerMiddleware(RequestDelegate next, ILogger<RequestHandelerMiddleware> logger, IConfiguration iconfiguration)
        {
            _next = next;
            _logger = logger;
            config = iconfiguration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"]
                    .FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                    AttachUserToContext(context, token);

                //Continue down the Middleware pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                //For unhandeled exceptions
                // log the error
                _logger.LogError(ex, "Exception during executing {Context}", context.Request.Path.Value);
                await HandleExceptionAsync(context, ex);
            }
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(config["JWT:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // extracting user information from claims 
                var userObj = new AuthenticatedUserDTO()
                {
                    UserId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value),
                    Username = Convert.ToString(jwtToken.Claims.First(x => x.Type == "email").Value),
                    FirstName = Convert.ToString(jwtToken.Claims.First(x => x.Type == "unique_name").Value),
                    LastName = Convert.ToString(jwtToken.Claims.First(x => x.Type == "given_name").Value),
                };

                // attach user to context on successful jwt validation
                context.Items["User"] = userObj;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errror on token parsing: {ex}");
                throw;
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error."
            }.ToString());
        }
    }
}
