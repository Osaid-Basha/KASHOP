using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
namespace KASHOP.BLL.Services.Class
{
    public class ReportServices
    {
        private readonly IProductRepository productRepository;

        public ReportServices(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public QuestPDF.Infrastructure.IDocument CreateDocument()
        {
            // Fix: Removed the `.GeneratePdf("hello.pdf")` call, as it returns void and is not part of the `IDocument` interface.  
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("KAshop - Products!")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);
                            foreach (var item in productRepository.GetAllProductWithImage())
                            {
                                x.Item().Text($"Id:{item.Id} ..Name {item.Name}");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });
        }
    }
}
