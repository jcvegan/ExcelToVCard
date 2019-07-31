using MixERP.Net.VCards.Models;
using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Map
{
public interface IContactToAddressMapper
{
    Address[] ToAddress(Contact contact);
}
}