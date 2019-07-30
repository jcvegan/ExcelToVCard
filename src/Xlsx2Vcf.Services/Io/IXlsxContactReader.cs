using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Io
{
public interface IXlsxContactReader
{
    Contact[] ReadContacts(string path);
}
}