
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Xlsx2Vcf.Services.Domain;

namespace Xlsx2Vcf.Services.Io
{
    public class XlsxContactReader : IXlsxContactReader {

        public Contact[] ReadContacts(string path)
        {
            var contactList = new List<Contact>();
            using (FileStream fStream = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
            {
                using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Open(fStream, false))
                {
                    WorkbookPart workbook = spreadsheet.WorkbookPart;
                    Sheet sheet = workbook.Workbook.Sheets.GetFirstChild<Sheet>();
                    Worksheet workSheet = (workbook.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                    IEnumerable<Row> rows = workSheet.GetFirstChild<SheetData>().Descendants<Row>().Skip(1);
                    foreach (Row row in rows)
                    {
                        var cells = row.Descendants<Cell>().ToArray();
                        var firstName = GetValue(spreadsheet, cells[1]);
                        var lastName = GetValue(spreadsheet, cells[2]);
                        var mail = GetValue(spreadsheet, cells[3]);
                        var phone = GetValue(spreadsheet, cells[4]);
                        contactList.Add(new Contact(firstName, lastName, phone, mail));
                    }
                }
            }    
            return contactList.ToArray();
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
            }return value;
        }
    }
}