using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Io
{
public interface IVcfWriter
{
    void WriteContacts(string fileName, Contact[] contacts);
}
}