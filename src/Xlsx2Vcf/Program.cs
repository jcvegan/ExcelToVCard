using System;
using Microsoft.Extensions.DependencyInjection;
using Xlsx2Vcf.Services.Extensions;
using Xlsx2Vcf.Services.Io;
using Xlsx2Vcf.Services.Map;

namespace Xlsx2Vcf
{
    class Program
    {
        static void Main(string source, string target, string configuration)
        {
            var serviceProvider = BuildServices();
            var settings = serviceProvider.GetRequiredService<IStringToSettingsMapper>().ToSettings(configuration);
            var contacts = serviceProvider.GetRequiredService<IXlsxContactReader>().ReadContacts(source, settings);
            serviceProvider.GetRequiredService<IVcfWriter>().WriteContacts(target, contacts);
        }

        private static IServiceProvider BuildServices()
        {
            var services = new ServiceCollection();
            services.AddFileSupport();
            return services.BuildServiceProvider();
        }
    }
}
