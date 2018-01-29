using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace PdfConvert.Controllers
{
    [Route("api/[controller]")]
    public class PdfController : Controller
    {
        private IConverter _converter;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PdfController(IConverter converter, IHostingEnvironment hostingEnvironment)
        {
            _converter = converter;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.CompletedTask;
            string html = "<!DOCTYPE html>" +
                            "<html lang = \"en\">" +
                                 "<head>" +
                                     "<meta charset = \"UTF-8\">" +
                                     " <title> Check </title>" +
                                      "<style>" +
                                                "@font-face {" +
                                                   "font-family: 'MICR';" +
                                                    "src: url(\"micrenc.woff\") format(\"woff\");" +
                                                    "font-weight: normal; " +
                                                    "font-style: normal; " +
                                                "}" +
                                    "</style> " +
                                "</head>" +
                                "<body>" +
                                    "<div style = \"text-align: center; margin-top:16px;\">" +
                                        "<p style=\"font-family: 'MICR'; font-size: 30px;\"> 235 384756  13425346 9877</p>" +
                                    "</div>" +
                                "</body>" +
                            "</html>";

            var pdf = new HtmlToPdfConverterService(_converter).Convert(html);
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "micr.pdf");
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                fs.Write(pdf, 0, pdf.Length);
            }
            return Ok();
        }

    }
}
