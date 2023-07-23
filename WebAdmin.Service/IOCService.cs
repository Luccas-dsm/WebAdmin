using Microsoft.Extensions.DependencyInjection;
using WebAdmin.Service.Interfaces;
using WebAdmin.Service.Services;

namespace WebAdmin.Service
{
    public class IOCService
    {
        private static readonly ServiceProvider _serviceProvider;

        static IOCService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IProdutoService, ProdutoService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
