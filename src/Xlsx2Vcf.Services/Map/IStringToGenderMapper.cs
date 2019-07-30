using MixERP.Net.VCards.Types;

namespace Xlsx2Vcf.Services.Map
{
    public interface IStringToGenderMapper
    {
        Gender ToGender(string genderString);
    }
}