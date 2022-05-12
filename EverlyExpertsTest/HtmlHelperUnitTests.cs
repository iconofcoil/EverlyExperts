using EverlyExperts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Threading.Tasks;

namespace EverlyExpertsTest
{
    [TestClass]
    public class HtmlHelperUnitTests
    {
        [TestMethod]
        public async Task TestConvertShortUrlToUrlMethod()
        {
            // Arrange
            string shortUrl = "https://cutt.ly/yHi3UyE";

            // Act
            string resultUrl = await HtmlHelper.ConvertShortUrlToUrl(shortUrl);

            // Assert
            Assert.AreEqual(resultUrl, "https://stackoverflow.com/questions/70592774/azure-function-to-signalr-serverless-to-web-app");
        }

        [TestMethod]
        public async Task TestParseHtmlHeadings1to3Method()
        {
            // Arrange
            string url = "https://web.ics.purdue.edu/~gchopra/class/public/pages/webdesign/05_simple.html";

            StringBuilder sb = new StringBuilder();

            sb.Append("A very simple webpage. This is an \"h1\" level header.");
            sb.Append(" ");
            sb.Append("This is a level h2 header.");
            sb.Append(" ");
            sb.Append("How about a nice ordered list!");
            sb.Append(" ");
            sb.Append("Unordered list");
            sb.Append(" ");
            sb.Append("Nested Lists!");

            // Act
            string resultUrl = await HtmlHelper.ParseHtmlHeadings1to3(url);

            // Assert
            Assert.AreEqual(resultUrl, sb.ToString());
        }
    }
}