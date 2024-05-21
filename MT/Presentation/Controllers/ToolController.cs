using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Shared.Helpers;
using System.Text;

namespace Presentation.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Index(string url = "", bool exportCSV = false)
        {
            ScraperModel scraperModel = new ScraperModel();
            if (!string.IsNullOrEmpty(url))
            {
                scraperModel.Emails = Scraper.ExtractEmails(url);
                if (scraperModel.Emails.Count() > 0)
                {
                    scraperModel.IsFound = true;
                    ViewBag.NotFound = "yes";
                }

                else ViewBag.NotFound = "no";
                if (exportCSV && scraperModel.Emails.Any())
                    return ExportEmailsToCSV(scraperModel.Emails);
            }
            return View(scraperModel);
        }

        private IActionResult ExportEmailsToCSV(IEnumerable<string> emails)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Email");

            foreach (var email in emails)
            {
                csv.AppendLine(email);
            }

            byte[] buffer = Encoding.UTF8.GetBytes(csv.ToString());
            return File(buffer, "text/csv", $"emails-{DateTime.Now.Ticks}.csv");
        }
    }
}
