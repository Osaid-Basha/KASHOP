using KASHOP.BLL.Services.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using Stripe;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]

    public class ReportsController : ControllerBase
    {
        private readonly ReportServices reportServices;

        public ReportsController(ReportServices reportServices)
        {
            this.reportServices = reportServices;
        }

        [HttpGet("PdfProduct")]
        public IActionResult GetPdfProductReports()
        {
            var document = reportServices.CreateDocument();
            var pdf = document.GeneratePdf();
            return File(pdf, "application/pdf", "hello-word.pdf");
        }
    }
}
