using System.IO;
using MatchMerge.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace MatchMerge.Pages
{
    public class ExcelDownloadModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        private string fileName = ExportExcelService.GetFileName();

        //string fileName = "segmentation_" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xlsx";
        public ExcelDownloadModel(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;

        }

        public IActionResult OnGet()
        {
            if(fileName != null)
            {
                var filePath = Path.Combine(_config.GetValue<string>("DownloadDocumentsPath:path"), fileName);
                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                return File(fileBytes, "application/force-download", fileName);
            }

            return null;
        }
    }
}