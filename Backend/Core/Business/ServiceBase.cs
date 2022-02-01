using Core.Business.Translate;
using Core.Entities.Concrete;
using Core.Utilities.Constants;
using Core.Utilities.IoC;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Business
{
    public class ServiceBase
    {
        public ServiceBase()
        {
            HttpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            RequestUserService = ServiceTool.ServiceProvider.GetService<IRequestUserService>();
            TranslateContext = ServiceTool.ServiceProvider.GetService<ITranslateContext>();

            UserTokenHelper = ServiceTool.ServiceProvider.GetService<ITokenHelper<User>>();

            Configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            CoreMessages = ServiceTool.ServiceProvider.GetService<CoreMessages>();
        }

        public IHttpContextAccessor HttpContextAccessor { get; }
        public IRequestUserService RequestUserService { get; }
        public ITranslateContext TranslateContext { get; }

        public ITokenHelper<User> UserTokenHelper { get; }

        public IConfiguration Configuration { get; }

        public CoreMessages CoreMessages { get; }
    }
}