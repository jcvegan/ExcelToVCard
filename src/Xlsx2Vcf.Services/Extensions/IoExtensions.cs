using Microsoft.Extensions.DependencyInjection;
using Xlsx2Vcf.Services.Io;
using Xlsx2Vcf.Services.Map;

namespace Xlsx2Vcf.Services.Extensions
{
public static class IoExtensions
{
    public static IServiceCollection AddFileSupport(this IServiceCollection services)
    {
        services.AddScoped<IXlsxContactReader, XlsxContactReader>();
        services.AddScoped<IVcfWriter, VcfWriter>();
        services.AddScoped<IContactToVCardMapper, ContactToVCardMapper>();
        services.AddScoped<IStringToGenderMapper, StringToGenderMapper>();
        return services;
    }
}
}