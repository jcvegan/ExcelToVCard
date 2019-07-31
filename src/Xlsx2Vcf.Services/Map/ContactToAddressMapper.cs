using MixERP.Net.VCards.Models;
using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Map
{
public class ContactToAddressMapper : IContactToAddressMapper
{
    public Address[] ToAddress(Contact contact)
    {
        if (string.IsNullOrEmpty(contact.Address))
            return null;
        return new Address[]
        {
            new Address()
            {
                Country = "Perú",
                Street = contact.Address
            },
        };
    }
}
}