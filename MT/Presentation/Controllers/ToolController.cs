using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Presentation.Models;
using Shared.Helpers;
using System.Text;

namespace Presentation.Controllers
{
    public class ToolController : Controller
    {
        [Authorize]
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
            }
            return View(scraperModel);
        }
        public IActionResult Export(ExportModel model)
        {
            return ExportEmailsToCSV(model.EmailCSV.Split(","));
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
    public class ExportModel
    {
        public string EmailCSV { get; set; }
    }
}
