using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.jack.cunliffe.ecommerceproject.POMPages
{
    internal class NavPOM
    {
        private IWebDriver _driver;

        public NavPOM(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement _myAccountButton => _driver.FindElement(By.CssSelector("#menu-item-46 > a"));
        private IWebElement _logOutButton => _driver.FindElement(By.CssSelector("#post-7 > div > div > nav > ul > li.woocommerce-MyAccount-navigation-link.woocommerce-MyAccount-navigation-link--customer-logout > a"));

        public void NavigateToMyAccount()
        {
            _myAccountButton.Click();
        }

        public void LogOut()
        {
            _logOutButton.Click();
        }
    }
}
