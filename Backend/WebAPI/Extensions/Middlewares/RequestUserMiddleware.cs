using Business.Abstract;
using Core.Business;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Extensions.Middlewares
{
    public class RequestUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRequestUserService _requestUserService;

        public RequestUserMiddleware(RequestDelegate next)
        {
            _next = next;
            _requestUserService = ServiceTool.ServiceProvider.GetService<IRequestUserService>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var claims = context.User.Claims.ToList();

            if (claims.Count > 0)
            {
                var id = claims.Find(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var email = claims.Find(c => c.Type == ClaimTypes.Email).Value;
                var status = claims.Find(c => c.Type == CustomClaimTypes.Status).Value;
                var roles = context.User.ClaimRoles();

                _requestUserService.SetRequestUser(new RequestUser
                {
                    Id = int.Parse(id),
                    Email = email,
                    Status = status,
                });
            }
            else
            {
                _requestUserService.SetRequestUser(null);
            }

            await _next.Invoke(context);
        }
    }
}