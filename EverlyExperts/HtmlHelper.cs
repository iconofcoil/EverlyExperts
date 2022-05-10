using HtmlAgilityPack;
using System.Text;

namespace EverlyExperts
{
    public static class HtmlHelper
    {
        public static async Task<string> ConvertShortUrlToUrl(string shortUrl)
        {
            var longUrl = string.Empty;

            try
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.AllowAutoRedirect = false;
                    using (var client = new HttpClient(handler))
                    {
                        var response = await client.GetAsync(shortUrl);

                        longUrl = response?.Headers.Location?.ToString();
                    }
                }
            }
            catch
            {
                longUrl = string.Empty;
            }

            return longUrl;
        }

        public static async Task<string> ParseHtmlHeadings1to3(string url)
        {
            string html = string.Empty;
            
            try
            {
                using (var handler = new HttpClientHandler())
                using (var client = new HttpClient(handler))
                {
                    html = await client.GetStringAsync(url);
                }
            }
            catch
            {
                return string.Empty;
            }

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
