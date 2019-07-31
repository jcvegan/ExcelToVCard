
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Io
{
    public class XlsxContactReader : IXlsxContactReader {

        public Contact[] ReadContacts(string path, Settings settings)
        {
            var contactList = new List<Contact>();
            using (FileStream fStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Open(fStream, false))
                {
                    WorkbookPart workbook = spreadsheet.WorkbookPart;
                    Sheet sheet = workbook.Workbook.Sheets.GetFirstChild<Sheet>();
                    Worksheet workSheet = (workbook.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                    IEnumerable<Row> rows = workSheet.GetFirstChild<SheetData>().Descendants<Row>().Skip(2);
                    foreach (Row row in rows)
                    {
                        var cells = row.Descendants<Cell>().ToArray();
                        var firstName = settings.FirstName.HasValue ? GetValue(spreadsheet, cells[settings.FirstName.Value]) : string.Empty;
                        var lastName = settings.LastName.HasValue ? GetValue(spreadsheet, cells[settings.LastName.Value]) : string.Empty;
                        var birthDateString = settings.BirthDate.HasValue ? GetValue(spreadsheet, cells[settings.BirthDate.Value]) : string.Empty;
                        DateTime? birthDate = null;
                        if (!string.IsNullOrEmpty(birthDateString))
                        {
                            birthDate = DateTime.FromOADate(double.Parse(birthDateString));
                            ;
                        }
                        var gender = settings.Gender.HasValue ? GetValue(spreadsheet, cells[settings.Gender.Value]) : string.Empty;
                        var phone = settings.Phone.HasValue ? ScapeString(GetValue(spreadsheet, cells[settings.Phone.Value])) : string.Empty;
                        var mail = settings.Mail.HasValue ? GetValue(spreadsheet, cells[settings.Mail.Value]) : string.Empty;
                        var address = settings.Address.HasValue ? GetValue(spreadsheet, cells[settings.Address.Value]) : string.Empty;

                        contactList.Add(new Contact(firstName, lastName, phone, mail,gender,birthDate, address));
                    }
                }
            }    
            return contactList.Where(c => !string.IsNullOrEmpty(c.FirstName)).ToArray();
        }

        private static string ScapeString(string toScape)
        {
            string scaped = "";
            scaped = toScape.Trim();
            scaped = scaped.Replace(" ", "");
            return scaped;
        }
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = string.Empty;
            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText.Trim();
                }
                
            }
            return value;
        }
    }
}