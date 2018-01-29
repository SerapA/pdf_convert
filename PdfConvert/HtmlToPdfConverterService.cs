using System;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace PdfConvert
{
    public class HtmlToPdfConverterService
    {
        private IConverter _converter;

        public HtmlToPdfConverterService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] Convert(string html){
            HtmlToPdfDocument doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.Letter,
                    Margins = new MarginSettings() { Top = 10 , Bottom=0, Left=10, Right=10},
                    
                },
                Objects = {
                        new ObjectSettings() {
                                 HtmlContent = html
                            }
                        }
            };

            return _converter.Convert(doc);
        }

      
    }
}
