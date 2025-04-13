using Microsoft.Azure.WebJobs;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using DinkToPdf;
using DinkToPdf.Contracts;


namespace FormApp3.Server.Models
{
    public class PdfService
    {
        //Stores the PDF covnverter dependency
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
            LoadLibwkhtmltox();
        }

        private void LoadLibwkhtmltox()
        {
            // var path = Path.Combine(Directory.GetCurrentDirectory(), "\\bin\\Debug\\net8.0\\wkhtmltox.dll");
            var path = "C:\\Facultate\\FormApp2\\FormApp2.Server\\bin\\Debug\\net8.0\\libwkhtmltox.dll";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The wkhtmltox.dll file was not found.", path);
            }
            var handle = LoadLibrary(path);
            if (handle == IntPtr.Zero)
            {
                throw new Exception("Failed to load libwkhtmltox.dll.");
            }
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        //Returns PDF as byte array
        public byte[] GeneratePdf(string htmlCode)
        {
            var globalSettings = new GlobalSettings
            {
                PaperSize = DinkToPdf.PaperKind.A4,
                Orientation = Orientation.Portrait,
            };

            //Content specific information
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlCode
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return _converter.Convert(pdf); //Converts HTML to PDF
        }
    }
}
