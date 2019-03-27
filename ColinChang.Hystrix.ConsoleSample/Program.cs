using System;
using System.Resources;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using ColinChang.Hystrix.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ColinChang.Hystrix.ConsoleSample
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            //DI
            var services = new ServiceCollection();
            services.AddSingleton<ITestService, TestService>();
            _serviceProvider = services.BuildAspectInjectorProvider();

            Test();
            Console.ReadKey();
        }

        static async void Test()
        {
            var p = _serviceProvider.GetService<ITestService>();

            //Fallback
            Console.WriteLine(p.FallbackTest(1, 2));

            //Retry
            Console.WriteLine(await p.RetryTestAsync("Colin"));

            //CircuitBreaker
            while (true)
            {
                Console.WriteLine(p.FallbackTest(1, 2));
                await Task.Delay(500);
            }

            //Timeout
            await p.TimeoutTestAsync(1);
        }
    }
}