using System.Collections.Generic;
using System.IO;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Serializer;
using Xlsx2Vcf.Services.Domain;
using Xlsx2Vcf.Services.Map;

namespace Xlsx2Vcf.Services.Io 
{
    public class VcfWriter : IVcfWriter 
    {
        private readonly IContactToVCardMapper _mapper;

        public VcfWriter(IContactToVCardMapper mapper)
        {
            _mapper = mapper;
        }

        public void WriteContacts(string fileName, Contact[] contacts)
        {
            List<VCard> cards = new List<VCard>();
            foreach (Contact contact in contacts)
            {
                cards.Add(_mapper.ToCard(contact));
            }

            var content = VCardCollectionSerializer.Serialize(cards);
            File.WriteAllText(fileName,content);
        }
    }
}