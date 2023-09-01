using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.jack.cunliffe.ecommerceproject.Utilities;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages
{
    internal class NavPOM
    {
        private IWebDriver _driver;

        public NavPOM(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators
        private IWebElement _myAccountButton => _driver.FindElement(By.CssSelector("#menu-item-46 > a"));
        private IWebElement _shopButton => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement _demoBanner
        {
            get
            {
                StaticHelpers.WaitForElement(_driver, By.CssSelector("body > p > a"), 5);
                return _driver.FindElement(By.CssSelector("body > p > a"));
            }
        }

        public void NavigateToMyAccount()
        {
            _myAccountButton.Click();
        }
        public void NavigateToShop()
        {
            _shopButton.Click();
        }
        public void DismissBanner()
        {
            _demoBanner.Click();
        }
    }
}
