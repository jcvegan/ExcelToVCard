using System;
using Microsoft.Extensions.DependencyInjection;
using Xlsx2Vcf.Services.Extensions;
using Xlsx2Vcf.Services.Io;

namespace Xlsx2Vcf
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var serviceProvider = BuildServices();
            var contacts = serviceProvider.GetRequiredService<IXlsxContactReader>().ReadContacts(args[0]);
            serviceProvider.GetRequiredService<IVcfWriter>().WriteContacts("Sociedad 49.vcf",contacts);
            Console.ReadLine();
        }

        private static IServiceProvider BuildServices()
        {
            var services = new ServiceCollection();
            services.AddFileSupport();
            return services.BuildServiceProvider();
        }
    }
}
