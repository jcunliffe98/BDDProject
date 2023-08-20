using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities
{
    internal class InstanceHelpers
    {
        private IWebDriver driver;

        public InstanceHelpers(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitForElement(By Locator, int TimeInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeInSeconds));
            wait.Until(waitForElement => waitForElement.FindElement(Locator).Enabled);
        }
    }
}
