using FormApp3.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormApp3.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly PdfService _pdfService;

        public PdfController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost]
        [Route("generate")]
        public async Task<IActionResult> GeneratePdf([FromBody] StudentForm formData)
        {
            if (formData == null || string.IsNullOrWhiteSpace(formData.Nume) ||
                string.IsNullOrWhiteSpace(formData.Prenume) || string.IsNullOrWhiteSpace(formData.Facultate)
                || string.IsNullOrWhiteSpace(formData.Motivare))
            {
                return BadRequest("Invalid request.");
            }

            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "formapp3.client", "src", "StudentFormTemplate.html");
            string htmlTemplate = System.IO.File.ReadAllText(templatePath);

            var htmlContent = htmlTemplate.Replace("{{Nume}}", formData.Nume)
                                          .Replace("{{Prenume}}", formData.Prenume)
                                          .Replace("{{Facultate}}", formData.Facultate)
                                          .Replace("{{Motivare}}", formData.Motivare)
                                          .Replace("{{Data}}", DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));

            var pdfBytes = await Task.Run(() => _pdfService.GeneratePdf(htmlContent)); // Run CPU-bound work on a background thread

            return File(pdfBytes, "application/pdf", $"StudentApplication_{formData.Nume}_{formData.Prenume}.pdf");
        }
    }
}
