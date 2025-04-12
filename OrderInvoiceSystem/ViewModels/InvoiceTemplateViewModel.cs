using System.ComponentModel.DataAnnotations;
using OrderInvoiceSystem.Models;

namespace OrderInvoiceSystem.ViewModels
{
    public class InvoiceTemplateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Template name is required.")]
        [MaxLength(200)]
        public string TemplateName { get; set; }

        [Required(ErrorMessage = "HTML content is required.")]
        public string HtmlContent { get; set; }

        [Required(ErrorMessage = "Template type is required.")]
        public TemplateType TemplateType { get; set; }
    }
}