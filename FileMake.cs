
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Telegram_Bot
{
    public class FileMake
    {
        public void MijozRoyxatPdf(string text, string pdfsFolder) 
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                      .Text("Mijozlar royxati PDF!")
                      .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                      .PaddingVertical(1, Unit.Centimetre)
                      .Column(x =>
                      {
                          x.Spacing(20);

                          x.Item().Text($"{text}");
                      });

                    page.Footer()
                      .AlignCenter()
                      .Text(x =>
                      {
                          x.Span("Page ");
                          x.CurrentPageNumber();
                      });
                });
            })
           .GeneratePdf(Path.Combine(pdfsFolder, "MijozlarRoyhati.pdf"));


        }
        public void HaridRoyxatPdf(string text, string pdfsFolder)
        {
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                      .Text("Haridlar royxati PDF!")
                      .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                      .PaddingVertical(1, Unit.Centimetre)
                      .Column(x =>
                      {
                          x.Spacing(20);

                          x.Item().Text($"{text}");
                      });

                    page.Footer()
                      .AlignCenter()
                      .Text(x =>
                      {
                          x.Span("Page ");
                          x.CurrentPageNumber();
                      });
                });
            })
           .GeneratePdf(Path.Combine(pdfsFolder, "MijozlarRoyhati.pdf"));


        }
        public static void WriteToExcel(string excelFilePath, string path)
        {
            string file = File.ReadAllText(path);

            string[] text = file.Split("|");



            try
            {


                // Fayl yaratish va ma'lumotlarni yozish
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    worksheet.Cells["A1"].Value = "Id";
                    worksheet.Cells["B1"].Value = "User";
                    worksheet.Cells["C1"].Value = "Contact";
                    int j = 2;
                    for (int i = 1; i < text.Length; i = i + 2, j++)
                    {
                        worksheet.Cells[$"A{j}"].Value = text[i];
                        worksheet.Cells[$"B{j}"].Value = text[i + 1];
                        worksheet.Cells[$"C{j}"].Value = text[i + 2];
                    }


                    // Faylni saqlash
                    var fileInfo = new System.IO.FileInfo(excelFilePath);
                    package.SaveAs(fileInfo);
                }

                Console.WriteLine("Fayl muvaffaqiyatli yaratildi: " + excelFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Xatolik yuz berdi: " + ex.Message);
            }
        }
    }
}
