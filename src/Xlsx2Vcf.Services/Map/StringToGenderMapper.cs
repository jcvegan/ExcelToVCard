using MixERP.Net.VCards.Types;

namespace Xlsx2Vcf.Services.Map
{
public class StringToGenderMapper : IStringToGenderMapper
{
    public Gender ToGender(string genderString)
    {
        if (string.IsNullOrEmpty(genderString))
            return Gender.Unknown;
        var gender = genderString.ToLower();
        if (gender == "h")
            return Gender.Male;
        if (gender == "m")
            return Gender.Female;
        return Gender.NotApplicable;
    }
}
}