using Newtonsoft.Json;
using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Map
{
    public class StringToSettingsMapper : IStringToSettingsMapper
    {
        public Settings ToSettings(string options)
        {
            return JsonConvert.DeserializeObject<Settings>(options);
        }
    }
}