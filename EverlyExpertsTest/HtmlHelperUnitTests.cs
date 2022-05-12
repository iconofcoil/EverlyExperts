using EverlyExperts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}