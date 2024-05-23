using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Shared.Helpers
{
    public class Scraper
    {
        private static async Task<string> GetHtmlAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error fetching the URL: " + e.Message);
                    return string.Empty;
                }
            }
        }

        public static List<string> ExtractEmails(string url)
        {
            try
            {
                string html = GetHtmlAsync(url).GetAwaiter().GetResult();
                List<string> emails = new List<string>();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                // Regular expression to match email addresses
                string emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
                Regex regex = new Regex(emailPattern);

                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()[not(ancestor::script) and not(ancestor::style)]"))
                {
                    MatchCollection matches = regex.Matches(node.InnerText);
                    foreach (Match match in matches)
                    {
                        if (!emails.Contains(match.Value))
                        {
                            emails.Add(match.Value);
                        }
                    }
                }

                return emails;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
