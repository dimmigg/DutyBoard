using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DutyBoard_Utility.Export
{
    public static class FileExport
    {
        public static void WriteToExcel(IEnumerable<DutyBoard_Models.Models.Export> report, string path)
        {
            try
            {
                using (var excel = new ExcelPackage())
                {
                    var worksheet = excel.Workbook.Worksheets.Add("Лист1");
                    worksheet.Cells["A1"].LoadFromCollection(report, true);

                    FormattingExcel(worksheet);

                    excel.SaveAs(new System.IO.FileInfo(path));
                }
            }
            catch (OutOfMemoryException)
            {
                throw new OutOfMemoryException("Недостаточно памяти для выполнения операции");
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void FormattingExcel(ExcelWorksheet worksheet)
        {
            var headerCells = worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column];
            headerCells.AutoFilter = true;
            headerCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerCells.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(146, 208, 80));

            var allCells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
            allCells.AutoFitColumns();
            worksheet.Cells[2, 2, worksheet.Dimension.End.Row, 3].Style.Numberformat.Format = "dd.MM.yyyy hh:mm";
            foreach (var cell in worksheet.Cells)
            {
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }

            for (var i = 2; i < worksheet.Dimension.End.Row; i++)
            {
                if (worksheet.Cells[i, 1].Value.ToString() != "Воскресенье" &&
                    worksheet.Cells[i, 1].Value.ToString() != "Суббота") continue;
                worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 242, 204));
            }
        }
    }
}
