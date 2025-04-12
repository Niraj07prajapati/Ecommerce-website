using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderInvoiceSystem.Data;
using OrderInvoiceSystem.ViewModels;
using OrderInvoiceSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace OrderInvoiceSystem.Controllers
{
    [Authorize]
    public class TemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TemplateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var templates = await _context.Templates.AsNoTracking().ToListAsync();
            return View(templates);
        }

        public IActionResult Create()
        {
            return View(new InvoiceTemplateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var template = new Template
                {
                    TemplateName = model.TemplateName,
                    HtmlContent = model.HtmlContent,
                    TemplateType = model.TemplateType,
                    CreatedDate = DateTime.Now,
                    CreatedBy = User.Identity?.Name ?? "Admin"
                };

                _context.Templates.Add(template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var template = await _context.Templates
                .AsNoTracking().FirstOrDefaultAsync(temp => temp.Id == id);

            if (template == null)
            {
                return NotFound();
            }

            var viewModel = new InvoiceTemplateViewModel
            {
                Id = template.Id,
                TemplateName = template.TemplateName,
                HtmlContent = template.HtmlContent,
                TemplateType = template.TemplateType
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InvoiceTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var template = await _context.Templates.FindAsync(model.Id);
                if (template == null)
                {
                    return NotFound();
                }

                template.TemplateName = model.TemplateName;
                template.HtmlContent = model.HtmlContent;
                template.TemplateType = model.TemplateType;
                template.UpdatedDate = DateTime.Now;
                template.UpdatedBy = User.Identity?.Name ?? "Admin";

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }

            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
