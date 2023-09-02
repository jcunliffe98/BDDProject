using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities
{
    internal static class StaticHelpers
    {

        public static IWebElement WaitForElement(IWebDriver driver, By Locator, int TimeInSeconds = 5)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeInSeconds));
            wait.Until(elementToBeFound => elementToBeFound.FindElement(Locator).Enabled);
            return driver.FindElement(Locator);
        }

        public static void TakeScreenshot(IWebDriver driver, IWebElement element, string fileName)
        {
            var ssdriver = element as ITakesScreenshot;
            var screenshot = ssdriver.GetScreenshot();
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            screenshot.SaveAsFile("[" + timestamp + "] " + fileName);
            TestContext.AddTestAttachment(fileName);
        }
    }
}
