using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ColinChang.Hystrix
{
    public static class ServiceCollectionExtension
    {
        // 注册"熔断安全"服务
        public static IServiceCollection RegisterAssemblyTypes(this IServiceCollection services, Assembly assembly, bool hystrixOnly = true)
        {
            foreach (var type in assembly.GetExportedTypes())
            {
                //要求业务实现类的第一个接口实现必须是其业务接口。可以实际情况自行约定规则。
                var interfaceType = type.GetInterfaces().FirstOrDefault();
                if (interfaceType == null)
                    continue;

                if (hystrixOnly)
                {
                    var hasHystrix = type.GetMethods()
                        .Any(m => m.GetCustomAttribute(typeof(HystrixAttribute)) != null);

                    if (!hasHystrix)
                        continue;
                }

                services.AddSingleton(interfaceType, type);
            }
            return services;
        }
    }
}
