using MixERP.Net.VCards;
using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Map
{
    public interface IContactToVCardMapper
    {
        VCard ToCard(Contact contact);
    }
}