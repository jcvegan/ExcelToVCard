﻿using MixERP.Net.VCards;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Types;
using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Map {
public class ContactToVCardMapper : IContactToVCardMapper {
    private readonly IStringToGenderMapper _stringToGender;

    public ContactToVCardMapper(IStringToGenderMapper stringToGender)
    {
        _stringToGender = stringToGender;
    }
    public VCard ToCard(Contact contact)
    {
        var card = new VCard();
        card.FirstName = contact.FirstName;
        card.LastName = contact.LastName;
        card.FormattedName = $"{contact.FirstName} {contact.LastName}";

        card.Telephones = new Telephone[]
        {
            new Telephone() {
                Number = contact.Phone, Type =  TelephoneType.Cell
            }
        };
        card.Emails = new Email[]
        {
            new Email(){ EmailAddress = contact.Mail, Type = EmailType.Smtp},
        };
        card.Organization = "INGENIA";
        card.Gender = _stringToGender.ToGender(contact.Gender);
        card.BirthDay = contact.BirthDate;
        return card;
    }
}
}