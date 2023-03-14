using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Radzen;
using FileInfo = System.IO.FileInfo;

namespace MatchMerge.Data
{
    public class ExportExcelService
    {
        public static string fileName;

        private readonly IConfiguration _configuration;
        public IJSRuntime JSRuntime;
        public NotificationService NotificationService;

        public ExportExcelService(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }


        public string get()
        {
            var PathDocument = _configuration.GetValue<string>("DownloadDocumentsPath:path");
            return PathDocument;
        }

        public static string GetFileName()
        {
            return fileName;
        }


        public void ExportErrorExcel(FileInfo file, List<int> ErrorList)
        {
            fileName =  file.Name;
            file = new FileInfo(Path.Combine(get(), fileName));

            var fileInfo = file;


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            using (var excelPackage = new ExcelPackage(fileInfo))
            {
                var worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                for (var e = 0; e < ErrorList.Count - 1; e = e + 2)
                {
                    worksheet.Cells[ErrorList[e], ErrorList[e + 1]].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[ErrorList[e], ErrorList[e + 1]].Style.Fill.BackgroundColor
                        .SetColor(ColorTranslator.FromHtml("#FFA7A0"));
                }
                excelPackage.Save();
            }
        }
    }
}