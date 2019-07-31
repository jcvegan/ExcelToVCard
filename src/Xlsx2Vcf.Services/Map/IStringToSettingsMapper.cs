using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Map
{
    public interface IStringToSettingsMapper
    {
        Settings ToSettings(string options);
    }
}