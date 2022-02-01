using Business.Abstract;
using Business.Constants;
using Business.CrossCuttingConcerns.Translate;
using Business.CrossCuttingConcerns.Translate.GoogleTranslate;
using Business.Helpers;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers
{
    public class BusinessModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<BusinessMessages>();
            serviceCollection.AddTransient<BusinessService>();
            serviceCollection.AddSingleton<IWordTranslateService, WordTranslateHelper>();
            serviceCollection.AddSingleton<FileReaderHelper>();
        }
    }
}