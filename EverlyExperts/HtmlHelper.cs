using HtmlAgilityPack;
using System.Text;

namespace EverlyExperts
{
    static class HtmlHelper
    {
        static async Task<string> ConvertShortUrlToUrl(string shortUrl)
        {
            var html = string.Empty;

            try
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.AllowAutoRedirect = false;
                    using (var client = new HttpClient(handler))
                    {
                        var response = await client.GetAsync("https://cutt.ly/4G67enp");

                        var longUrl = response?.Headers.Location?.ToString();
                        html = await client.GetStringAsync("https://www.w3schools.com/html/html_headings.asp");
                    }
                }
            }
            catch
            {
                html = string.Empty;
            }

            return html;
        }

        static string ParseHtmlHeadings1to3(string html)
        {
            StringBuilder sb = new StringBuilder();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//*[self::h1 or self::h2 or self::h3]");

            if (nodes != null && nodes.Count > 0)
            {
                foreach (var node in nodes)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }

                    sb.Append(node.InnerText.Trim());
                }
            }

            return sb.ToString().Trim();
        }
    }
}
